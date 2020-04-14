using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    private Card cardSelected;
    private GameObject cardObjSelected;
    private string cardSelObjName;
    private int player;

	// Use this for initialization
	void Start () {
        player = 1;

        List<Card> cardDeck1 = new List<Card>();
        List<Card> cardDeck2 = new List<Card>();
        List<Card> tableDeck = new List<Card>();
        int numDecks = 1;
        while (numDecks <= 2)
        {
            for (int i = 0; i < CardDeck.getCardDeck().Length; i++)
            {
                cardDeck1.Add(CardDeck.getCardDeck()[i]);
            }
            numDecks++;
        }

        Debug.Log(cardDeck1.Count);

        CardDeck.Shuffle<Card>(cardDeck1);
        CardDeck.Shuffle<Card>(cardDeck1);

        while (cardDeck2.Count < 48)
        {
            cardDeck2.Add(cardDeck1[0]);
            cardDeck1.RemoveAt(0);
        }

        while (tableDeck.Count < 8)
        {
            tableDeck.Add(cardDeck1[0]);
            cardDeck1.RemoveAt(0);
        }

        Debug.Log(cardDeck1.Count + ", " + cardDeck2.Count + ", " + tableDeck.Count);

        DeckScript deckScript1 = GameObject.Find("P1 Deck").GetComponent<DeckScript>();
        DeckScript deckScript2 = GameObject.Find("P2 Deck").GetComponent<DeckScript>();

        for(int i=1; i<9; i++)
        {
            HouseScript houseScript;
            if (i < 5)
            {
                houseScript = GameObject.Find("LH" + i + " MainC").GetComponent<HouseScript>();
            }
            else
            {
                houseScript = GameObject.Find("RH" + i + " MainC").GetComponent<HouseScript>();
            }

            houseScript.addFirstCardToPile(tableDeck[i - 1]);
            houseScript.loadCard();
        }

        deckScript1.setDeck(cardDeck1);
        deckScript2.setDeck(cardDeck2);
    }
	
	// Update is called once per frame
	void Update () {
		if(player == 1)
        {
            GameObject text = GameObject.Find("Turn text");
            text.GetComponent<Text>().text = "It's Player 1's turn";
            text.transform.localPosition = new Vector3(0.0f, -280.0f, 0.0f);
        }
        else if (player == 2)
        {
            GameObject text = GameObject.Find("Turn text");
            text.GetComponent<Text>().text = "It's Player 2's turn";
            text.transform.localPosition = new Vector3(0.0f, 280.0f, 0.0f);
        }
    }

    public Card getCardSelected()
    {
        return cardSelected;
    }

    public void setCardSelected(Card card)
    {
        this.cardSelected = card;
    }

    public string getCardSelObjName()
    {
        return cardSelObjName;
    }

    public void setCardSelObjName(string objName)
    {
        this.cardSelObjName = objName;
    }

    public int getPlayer()
    {
        return player;
    }

    public void setPlayer(int playerNum)
    {
        this.player = playerNum;
    }

    // if neitheir of decks has a card on table that needs to be played
    public bool noCardsOnTable()
    {
        GameObject card1 = GameObject.Find("P1 Card");
        GameObject card2 = GameObject.Find("P2 Card");

        if(!card1.GetComponent<Image>().enabled && !card2.GetComponent<Image>().enabled)
            { return true; }
        else { return false; }
    }
    
    public bool noEmptyHouses()
    {
        bool b = true;
        for (int i = 1; i < 9; i++)
        {
            HouseScript houseScript;
            if (i < 5)
            {
                houseScript = GameObject.Find("LH" + i + " MainC").GetComponent<HouseScript>();
            }
            else
            {
                houseScript = GameObject.Find("RH" + i + " MainC").GetComponent<HouseScript>();
            }

            if (houseScript.isEmpty()) { b = false; }
        }
        return b;
    }
}
