using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseScript : MonoBehaviour {

    private List<Card> pile = new List<Card>();
    private bool selected = false;
    private int houseNum = 0;
    
    // Use this for initialization
	void Start () {
		for (int i = 1; i<9; i++)
        {
            if (gameObject.name.Contains(i.ToString()))
            {
                houseNum = i;
            }
        }
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

    public int getHouseNum() { return houseNum; }

    public bool isEmpty()
    {
        if (pile.Count == 0) { return true; }
        else { return false; }
    }

    public void clickHouse()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();

        // If there is a card selected and it is not itself, then that card must be added to pile
        if (gameScript.getCardSelected() != null && !selected)
        {
            // If pile is empty, the card can immediatly be added
            if (pile.Count == 0)
                { addToPile(); }
            // If not it must test if the card select is the different colour and +1 number of the last card in pile
            else if (gameScript.getCardSelected().GetColour() != pile[pile.Count - 1].GetColour())
            {
                if (gameScript.getCardSelected().getNumber() == pile[pile.Count -1].getNumber() - 1)
                    { addToPile(); }
            }
        }
        // If the card is the one selected, then it must unselected
        else if (selected)
        {
            gameScript.setCardSelected(null);
            gameScript.setCardSelObjName(null);
        }
        // Else it means no card is selected and we want to select the card
        // (but can't be selected if there's a card on table)
        else if (gameScript.noCardsOnTable() && pile.Count != 0)
        {
            gameScript.setCardSelected(this.pile[pile.Count - 1]);
            gameScript.setCardSelObjName(gameObject.name);
        }
    }

    public void addToPile()
    {
        GameScript gameScript = FindObjectOfType<Camera>().GetComponent<GameScript>();

        pile.Add(gameScript.getCardSelected());
        if (pile.Count > 1)
        { 
            GameObject newObj;
            GameObject createdObj;
            Transform parent = gameObject.transform.parent;
        
            if (gameObject.name.Contains("LH"))
            {
                newObj = Resources.Load<GameObject>("LPiledCard");
                //parentName = "LHCanvas" + houseNum;
                createdObj = Instantiate(newObj, parent);
                createdObj.name = "LH" + houseNum + "C" + (pile.Count - 1);
                createdObj.transform.localPosition = new Vector3((pile.Count - 2) * -15.0f, 0.0f, 0.0f);

            }
            else //if (gameObject.name.Contains("RH"))
            {
                newObj = Resources.Load<GameObject>("RPiledCard");
                createdObj = Instantiate(newObj, parent);
                createdObj.name = "RH" + houseNum + "C" + (pile.Count - 1);
                createdObj.transform.localPosition = new Vector3((pile.Count - 2) * 15.0f, 0.0f, 0.0f);
            }

            gameObject.transform.SetSiblingIndex(pile.Count-1);
            PiledScript piledScript = createdObj.GetComponent<PiledScript>();
            piledScript.setCard(pile[pile.Count - 2]);
            piledScript.loadCard();
        }

        if (gameScript.getCardSelObjName().Contains("Card"))
        {
            CardScript cardScript = GameObject.Find(gameScript.getCardSelObjName()).GetComponent<CardScript>();
            DeckScript deckScript = GameObject.Find("P" + gameScript.getPlayer() + " Deck").GetComponent<DeckScript>();
            TrashScript trashScript = GameObject.Find("P" + gameScript.getPlayer() + " Trash").GetComponent<TrashScript>();

            deckScript.removeCard();

            cardScript.setEnabled(false);
            cardScript.selectCard(); //To unselect it

            if (trashScript.isEmpty() && deckScript.isEmpty())
            {
                Instantiate(Resources.Load<GameObject>("Winner"), GameObject.Find("Canvas").transform);
                GameObject.Find("Winner").GetComponent<Text>().text = "Player " + gameScript.getPlayer() + " wins!!";
            }
        }

        else if (gameScript.getCardSelObjName().Contains("Trash"))
        {
            TrashScript trashScript = GameObject.Find(gameScript.getCardSelObjName()).GetComponent<TrashScript>();
            trashScript.removeCard();
            gameScript.setCardSelected(null);
            gameScript.setCardSelObjName(null);

            DeckScript deckScript = GameObject.Find("P" + gameScript.getPlayer() + " Deck").GetComponent<DeckScript>();
            if (trashScript.isEmpty() && deckScript.isEmpty())
            {
                Instantiate(Resources.Load<GameObject>("Winner"), GameObject.Find("Canvas").transform);
                GameObject.Find("Winner(Clone)").GetComponent<Text>().text = "Player " + gameScript.getPlayer() + " wins!!";
            }
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

    public void removeCard()
    {
        pile.RemoveAt(pile.Count - 1);

        if (pile.Count > 0)
        {
            GameObject objToDestroy;
            string objName;

            if (gameObject.name.Contains("LH"))
            {
                objName = "LH" + houseNum + "C" + pile.Count;
            }
            else //if (gameObject.name.Contains("RH"))
            {
                objName = "RH" + houseNum + "C" + pile.Count;
            }

            objToDestroy = GameObject.Find(objName);
            Destroy(objToDestroy);
        }

        loadCard();
    }

    public void addFirstCardToPile(Card card)
    {
        pile.Add(card);
        loadCard();
    }

    public void loadCard()
    {
        if (pile.Count != 0)
        {
            Card card = pile[pile.Count - 1];
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

            float d = 0;
            if (gameObject.name.Contains("LH")) { d = -15; }
            else if (gameObject.name.Contains("RH")) { d = 15; }
            gameObject.transform.localPosition = new Vector3((pile.Count - 1) * d, 0.0f, 0.0f);
        }
        else
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/empty");
            this.GetComponentInChildren<Text>().text = "";
            gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    
}
