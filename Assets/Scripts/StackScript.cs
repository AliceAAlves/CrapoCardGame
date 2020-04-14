using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackScript : MonoBehaviour {

    private List<Card> stack = new List<Card>();
    private Card.Suit stackSuit;

    // Use this for initialization
    void Start () {
        if (gameObject.name.Contains("Hearts"))
            { stackSuit = Card.Suit.HEARTS; }
        else if (gameObject.name.Contains("Spades"))
            { stackSuit = Card.Suit.SPADES; }
        else if (gameObject.name.Contains("Diamonds"))
            { stackSuit = Card.Suit.DIAMONDS; }
        else if (gameObject.name.Contains("Clubs"))
            { stackSuit = Card.Suit.CLUBS; }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickStack()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();

        // You can only interact with it if there's a card selected
        if (gameScript.getCardSelected() != null)
        {
            if (gameScript.getCardSelected().getSuit() == stackSuit)
            {
                if (stack.Count == 0 && gameScript.getCardSelected().getNumber() == 1)
                    { addToStack(); }
                else if (stack.Count > 0)
                {
                    if(gameScript.getCardSelected().getNumber() == stack[stack.Count - 1].getNumber() + 1)
                        { addToStack(); }
                }
            }
            
        }
    }

    public void addToStack()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();
        stack.Add(gameScript.getCardSelected());

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
        if (stack.Count != 0)
        {
            Card card = stack[stack.Count - 1];
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
    }
}
