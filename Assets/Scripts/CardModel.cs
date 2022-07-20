using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour
{
    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex; // e.g. faces[cardIndex];
    private bool isFaceUp;

    private SpriteRenderer spriteRenderer;
    private CardFlipper flipper;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flipper = GetComponent<CardFlipper>();

        isFaceUp = false; //while debugging
    }

    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            flipper.FlipCard(spriteRenderer.sprite, faces[cardIndex]);
        }
        else
        {
            flipper.FlipCard(spriteRenderer.sprite, cardBack);
        }
    }

    public void Toggle()
    {
        isFaceUp = !isFaceUp;
        ToggleFace(isFaceUp);
    }

    private void OnMouseDown()
    {
        if (isFaceUp == true || GameController.Instance.Processing == true) {
            return;
        } else if (isFaceUp == false && GameController.Instance.LastCard == null) {
            //isFaceUp = true;
            GameController.Instance.LastCard = this;
            Toggle();
        } else if (GameController.Instance.LastCard != null && GameController.Instance.LastCard.cardIndex == this.cardIndex) {
            GameController.Instance.LastCard = null;
            Toggle();
            GameController.Instance.UpdateTries();
        } else if (GameController.Instance.LastCard != null && GameController.Instance.LastCard.cardIndex != this.cardIndex) {
            Toggle();
            GameController.Instance.UpdateTries();
            StartCoroutine(ToggleAgain());
        }
    }

    private IEnumerator ToggleAgain() {
        GameController.Instance.Processing = true;
        yield return new WaitForSeconds(1f);

        GameController.Instance.LastCard.Toggle();
        GameController.Instance.LastCard = null;
        
        Toggle();

        GameController.Instance.Processing = false;
    }
}
