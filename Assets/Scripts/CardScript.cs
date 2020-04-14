using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {

    private Card card;
    private bool selected = false;
    

    // Use this for initialization
    void Start () {

        //card = new Card(13, (Card.Suit)1);
        ////cardObj = GameObject.Find("Card");

        //loadCard();
        //gameObject.SetActive(false);
        setEnabled(false);

    }

    // Update is called once per frame
    void Update()
    {
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

    public void changeCard()
    {
        System.Random rndGen = new System.Random();
        int random = rndGen.Next(52); // creates a number between 1 and 52

        card = CardDeck.getCardDeck()[random];

        loadCard();

    }

    public void loadCard()
    {
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

    public Card getCard()
        { return this.card; }

    public void setCard(Card card)
        { this.card = card; }

    public void selectCard()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();
        
        if (selected)
        {
            gameScript.setCardSelected(null);
            gameScript.setCardSelObjName(null);
        }
        else
        {
            gameScript.setCardSelected(this.card);
            gameScript.setCardSelObjName(gameObject.name);
        }
        
    }

    public void setEnabled(bool enabled)
    {
        this.GetComponent<Image>().enabled = enabled;
        this.GetComponentInChildren<Text>().enabled = enabled;
    }
}
