using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashScript : MonoBehaviour {

    private List<Card> trashDeck = new List<Card>();
    private bool selected = false;

	// Use this for initialization
	void Start () {
        loadCard();
	}
	
	// Update is called once per frame
	void Update () {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();

        if (gameScript.getCardSelObjName() != null)
        {
            if (gameScript.getCardSelObjName().CompareTo(gameObject.name) == 0)
            {
                selected = true;
            }
            else selected = false;
        }
        else selected = false;

        if (selected) { this.GetComponent<Image>().color = Color.yellow; }
        else { this.GetComponent<Image>().color = Color.white; }
    }

    public void clickTrash()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();

        // If there is a card selected and it is not itself, then that card must be put in trash
        if (gameScript.getCardSelected() != null && !selected)
        {
            string trashObjName = "P" + gameScript.getPlayer() + " Trash";
            string cardObjName = "P" + gameScript.getPlayer() + " Card";

            // If the origin is the deck of the player active and the trash if of the active player, then any card can be trashed
            // And active player changes
            if (gameObject.name.CompareTo(trashObjName) == 0 && gameScript.getCardSelObjName().CompareTo(cardObjName) == 0)
            {
                trashCard();
                if (gameScript.getPlayer() == 1)
                    { gameScript.setPlayer(2); }
                else { gameScript.setPlayer(1); }
                
            }
            // If not it must test if the card select is the same suit and +1 or -1 number of the card in trash
            else if(gameScript.getCardSelected().isOneNextTo(trashDeck[trashDeck.Count - 1]))
            {
                trashCard();
            }
        }
        // If the card in the trash is the one selected, then it must unselected
        else if (selected)
        {
            gameScript.setCardSelected(null);
            gameScript.setCardSelObjName(null);
        }
        // Else it means no card is selected and we want to select the card in the trash
        // (but only if it is the trash of the active player, and trash can't be selected if there's a card on table)
        else if(gameObject.name.Contains(gameScript.getPlayer().ToString()) && gameScript.noCardsOnTable() && trashDeck.Count != 0)
        {
            gameScript.setCardSelected(this.trashDeck[trashDeck.Count - 1]);
            gameScript.setCardSelObjName(gameObject.name);
        }

    }

    void trashCard()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();
        
        trashDeck.Add(gameScript.getCardSelected());

        if (gameScript.getCardSelObjName().Contains("Card"))
        {
            CardScript cardScript = GameObject.Find(gameScript.getCardSelObjName()).GetComponent<CardScript>();
            if (gameScript.getCardSelObjName().Contains("1"))
            {
                DeckScript deckScript = GameObject.Find("P1 Deck").GetComponent<DeckScript>();
                deckScript.removeCard();
            }
            else if (gameScript.getCardSelObjName().Contains("2"))
            {
                DeckScript deckScript = GameObject.Find("P2 Deck").GetComponent<DeckScript>();
                deckScript.removeCard();
            }
            cardScript.setEnabled(false);
            cardScript.selectCard(); //To unselect it
        }

        else if (gameScript.getCardSelObjName().Contains("Trash"))
        {
            TrashScript trashScript = GameObject.Find(gameScript.getCardSelObjName()).GetComponent<TrashScript>();
            trashScript.removeCard();
            gameScript.setCardSelected(null);
            gameScript.setCardSelObjName(null);
        }

        else if (gameScript.getCardSelObjName().Contains("Main"))
        {
            HouseScript houseScript = GameObject.Find(gameScript.getCardSelObjName()).GetComponent<HouseScript>();
            houseScript.removeCard();
            gameScript.setCardSelected(null);
            gameScript.setCardSelObjName(null);
        }

        loadCard();

    }

    public void loadCard()
    {
        if (trashDeck.Count != 0)
        {
            Card card = trashDeck[trashDeck.Count - 1];
            if (card.getSuit() == Card.Suit.CLUBS)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/clubs");
            }
            else if (card.getSuit() == Card.Suit.HEARTS)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/hearts");
            }
            else if (card.getSuit() == Card.Suit.SPADES)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/spades");
            }
            else if (card.getSuit() == Card.Suit.DIAMONDS)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/diamonds");
            }

            this.GetComponentInChildren<Text>().text = card.getNumberStr();

            if (card.GetColour() == Card.Colour.RED)
            {
                this.GetComponentInChildren<Text>().color = Color.red;
            }
            else
            {
                this.GetComponentInChildren<Text>().color = Color.black;
            }
        }
        else
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/trash");
            this.GetComponentInChildren<Text>().text = "";
        }
            
    }

    public void removeCard()
    {
        trashDeck.RemoveAt(trashDeck.Count - 1);
        loadCard();
    }

    public void emptyTrash()
    {
        trashDeck = new List<Card>();
        loadCard();
    }

    public List<Card> getTrashDeck()
    {
        return trashDeck;
    }

    public bool isEmpty()
    {
        bool b = false;
        if(trashDeck.Count == 0) { b = true; }
        return b;
    }
}
