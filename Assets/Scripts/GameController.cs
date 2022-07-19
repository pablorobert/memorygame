using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
    public int[] cardIndexes;
    public CardModel[] cards;

    public CardModel lastCard;

    public int jogadas;

    public bool processando;

    public Text tentativas;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        lastCard = null;
        jogadas = 0;
        processando = false;

        cards = FindObjectsOfType<CardModel>();

        if (cards.Length == 0) return;

        int numberOfIndexes = cards.Length / 2;
        cardIndexes = new int[numberOfIndexes];
        for (int i = 0; i < numberOfIndexes; i++) {
            cardIndexes[i] = -1;
        }

        int position = 0;
        while (true) {
        //for (int i = 0; i < numberOfIndexes; i++) {
unique:
            int newCard = Random.Range(0, 51);
            //print ("novo " + newCard);
            for (int j = 0; j < numberOfIndexes; j++) {
                if (newCard == cardIndexes[j]) {
                    print (newCard + " repetido");
                    goto unique;
                }
            }
            //print ("salva novo card");
            cardIndexes[position] = newCard;
            position++;
            if (position == numberOfIndexes) break;
        }
        //return;
        //associa de 2 em 2
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
    
    public void AlterarTentativas()
    {
        jogadas++;
        tentativas.text = "Tentativas: " + jogadas;

    }

    /// <summary>
    /// Reset is called when the user hits the Reset button in the Inspector's
    /// context menu or when adding the component the first time.
    /// </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }


}
