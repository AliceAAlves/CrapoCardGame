using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDeck  {

    // A static method that creates and stores a complete deck of cards.

    private static Card[] cardDeck = new Card[0];

    public static Card[] getCardDeck()
    {
        if (cardDeck.Length == 0)
        {
            cardDeck = new Card[52];
            int i = 0;
            for (int k = 0; k<4; k++)
            {
                for (int j = 1; j<14; j++)
                {
                    cardDeck[i++] = new Card(j, (Card.Suit)k);
                }
            }
        }

        return cardDeck;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
