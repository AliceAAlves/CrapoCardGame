using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiledScript : MonoBehaviour {

    private Card card;

	// Use this for initialization
	void Start () {
        loadCard();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Card getCard()
    {
        return card;
    }

    public void setCard(Card card)
    {
        this.card = card;
    }

    public void loadCard()
    {
        if (card != null)
        {
            if (card.getSuit() == Card.Suit.CLUBS)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/clubs-piled");
            }
            else if (card.getSuit() == Card.Suit.HEARTS)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/hearts-piled");
            }
            else if (card.getSuit() == Card.Suit.SPADES)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/spades-piled");
            }
            else if (card.getSuit() == Card.Suit.DIAMONDS)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/diamonds-piled");
            }

            this.GetComponentsInChildren<Text>()[0].text = card.getNumberStr();
            this.GetComponentsInChildren<Text>()[1].text = card.getNumberStr();

            if (card.GetColour() == Card.Colour.RED)
            {
                this.GetComponentsInChildren<Text>()[0].color = Color.red;
                this.GetComponentsInChildren<Text>()[1].color = Color.red;
            }
            else
            {
                this.GetComponentsInChildren<Text>()[0].color = Color.black;
                this.GetComponentsInChildren<Text>()[1].color = Color.black;
            }
        }
    }
}
