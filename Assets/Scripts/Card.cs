using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Card : IComparable<Card>{

    public enum Suit
    {
        HEARTS,
        SPADES,
        CLUBS,
        DIAMONDS
    }

    public enum Colour
    {
        RED,
        BLACK
    }

    private int number;
    private Suit suit;
    private Colour colour;

    public Card(int number, Suit suit)
    {
        this.number = number;
        this.suit = suit;

        if (suit == Suit.HEARTS || suit == Suit.DIAMONDS)
            { this.colour = Colour.RED; }
        else { this.colour = Colour.BLACK; }

    }

    public Card(string strNumber, string strSuit)
    {
        try
        {
            this.number = Int32.Parse(strNumber);
        }
        catch (FormatException e)
        {
            if(string.Equals(strNumber, "a", StringComparison.OrdinalIgnoreCase) || string.Equals(strNumber, "ace", StringComparison.OrdinalIgnoreCase))
                { this.number = 1; }
            else if (string.Equals(strNumber, "j", StringComparison.OrdinalIgnoreCase) || string.Equals(strNumber, "jack", StringComparison.OrdinalIgnoreCase))
                { this.number = 11; }
            else if (string.Equals(strNumber, "q", StringComparison.OrdinalIgnoreCase) || string.Equals(strNumber, "queen", StringComparison.OrdinalIgnoreCase))
                { this.number = 12; }
            else if (string.Equals(strNumber, "k", StringComparison.OrdinalIgnoreCase) || string.Equals(strNumber, "king", StringComparison.OrdinalIgnoreCase))
                { this.number = 13; }
        }

        if (string.Equals(strSuit, "hearts", StringComparison.OrdinalIgnoreCase))
            { this.suit = Suit.HEARTS; }
        else if (string.Equals(strSuit, "spades", StringComparison.OrdinalIgnoreCase))
            { this.suit = Suit.SPADES; }
        else if (string.Equals(strSuit, "clubs", StringComparison.OrdinalIgnoreCase))
            { this.suit = Suit.CLUBS; }
        else if (string.Equals(strSuit, "diamonds", StringComparison.OrdinalIgnoreCase))
            { this.suit = Suit.DIAMONDS; }

        if (this.suit == Suit.HEARTS || this.suit == Suit.DIAMONDS)
            { this.colour = Colour.RED; }
        else { this.colour = Colour.BLACK; }
    }

    public int CompareTo(Card other)
    {
        return this.number - other.number;
    }

    public int getNumber()
    {
        return number;
    }

    public string getNumberStr()
    {
        string numberStr = "inexistent";

        if (number>1 && number < 11)
            { numberStr = number.ToString(); }
        else if (number == 1)
            { numberStr = "A"; }
        else if (number == 11)
            { numberStr = "J"; }
        else if (number == 12)
            { numberStr = "Q"; }
        else if (number == 13)
            { numberStr = "K"; }

        return numberStr;
    }

    public Suit getSuit()
    {
        return suit;
    }

    public Colour GetColour()
    {
        return colour;
    }

    public bool isSameSuit(Card other)
    {
        if (this.suit == other.suit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isOneNextTo(Card other)
    {
        bool b = false;
        if (this.isSameSuit(other))
        {
            if (this.number == other.number + 1 || this.number == other.number - 1)
                { b = true; }
            else if (this.number == 1 && other.number == 13)
                { b = true; }
            else if (this.number == 13 && other.number == 1)
                { b = true; }
        }

        return b;
    }
}
