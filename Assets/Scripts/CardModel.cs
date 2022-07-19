using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex; // e.g. faces[cardIndex];
    public bool isFaceUp;

    private GameController gameController;

    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    public void Toggle()
    {
        isFaceUp = !isFaceUp;
        ToggleFace(isFaceUp);
    }


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFaceUp = false; //durante debug
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        //Toggle();
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        if (isFaceUp == true || gameController.processando == true) {
            return;
        } else if (isFaceUp == false && gameController.lastCard == null) {
            //é a primeira carta
            //isFaceUp = true;
            gameController.lastCard = this;
            Toggle();
        } else if (gameController.lastCard != null && gameController.lastCard.cardIndex == this.cardIndex) {
            print ("é a mesma");
            gameController.lastCard = null;
            Toggle();
            gameController.AlterarTentativas();
        } else if (gameController.lastCard != null &&  gameController.lastCard.cardIndex != this.cardIndex) {
            print ("diferente");
            Toggle();
            gameController.AlterarTentativas();
            StartCoroutine(ToggleAgain());
        }
        
    }

    IEnumerator ToggleAgain() {
        gameController.processando = true;
        yield return new WaitForSeconds(1f);

        gameController.lastCard.Toggle();
        gameController.lastCard = null;
        
        Toggle();

        gameController.processando = false;
    }
}
