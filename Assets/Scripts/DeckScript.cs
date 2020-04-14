using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckScript : MonoBehaviour {

    private List<Card> cardDeck;
    private GameObject cardObject;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void drawCard()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();
        GameObject trashObj = GameObject.Find("P" + gameScript.getPlayer() + " Trash");
        TrashScript trashScript = trashObj.GetComponent<TrashScript>();

        string objName = "P" + gameScript.getPlayer() + " Deck";

        if (gameObject.name.CompareTo(objName) == 0 && (gameScript.noEmptyHouses() || trashScript.isEmpty()))
        {
            // if it's empty we must pull the cards from trash
            if (cardDeck.Count == 0)
            {
                cardDeck = trashScript.getTrashDeck();
                trashScript.emptyTrash();
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/deck");
            }
            cardObject = GameObject.Find("P" + gameScript.getPlayer() + " Card");

            CardScript cardScript = cardObject.GetComponent<CardScript>();

            cardScript.setCard(cardDeck[0]);

            cardScript.loadCard();

            cardObject.GetComponent<Image>().enabled = true;
            cardObject.GetComponentInChildren<Text>().enabled = true;

            if (cardDeck.Count == 1) // if this is the last card being drawn
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/empty");
            }
        }
  
    }

    public void setDeck(List<Card> deck)
    {
        this.cardDeck = deck;
    }

    public void removeCard()
    {
        cardDeck.RemoveAt(0);
    }

    public bool isEmpty()
    {
        bool b = false;
        if (cardDeck.Count == 0) { b = true; }
        return b;
    }
}
