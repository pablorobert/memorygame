using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int[] cardIndexes;
    public CardModel[] cards;

    public CardModel LastCard { get; set; }

    public int Tries { get; private set; }

    public bool Processing { get; set; }

    [Header("UI")]
    [SerializeField]
    private Text triesText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        LastCard = null;
        Tries = 0;
        Processing = false;

        cards = FindObjectsOfType<CardModel>();

        if (cards.Length == 0) return;

        int numberOfIndexes = cards.Length / 2;
        cardIndexes = new int[numberOfIndexes];
        for (int i = 0; i < numberOfIndexes; i++) {
            cardIndexes[i] = -1;
        }

        int position = 0;
        while (true) {
unique:
            int newCard = Random.Range(0, 51);
            for (int j = 0; j < numberOfIndexes; j++) {
                if (newCard == cardIndexes[j]) {
                    //print (newCard + " repetido");
                    goto unique;
                }
            }
            cardIndexes[position] = newCard;
            position++;
            if (position == numberOfIndexes) break;
        }
        for (int i = 0; i < numberOfIndexes; i++) {
            cards[i].cardIndex = cardIndexes[i];
            cards[i + numberOfIndexes].cardIndex = cardIndexes[i];
        }
       //swaps
        int numberOfChanges = Random.Range(20, 30);
        for (int i = 0; i < numberOfChanges; i++) {
            int pos1 = Random.Range(0, cards.Length);
            int pos2 = Random.Range(0, cards.Length);

            int temp = cards[pos1].cardIndex;
            cards[pos1].cardIndex = cards[pos2].cardIndex;
            cards[pos2].cardIndex = temp;
        }
    }
    
    public void UpdateTries()
    {
        Tries++;
        triesText.text = "Tries: " + Tries;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
