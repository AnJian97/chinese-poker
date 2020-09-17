using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvalutor : MonoBehaviour
{
    private int heartsSum;
    private int diamondSum;
    private int clubSum;
    private int spadesSum;
    private GameObject[] cards;
    private HandValue handValue;


    public void Start ( )
    {

    }


    public enum Hand
    {
        Nothing,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush
    }

    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }

    public HandValue HandValues
    {
        get { return handValue; }
        set { handValue = value; }
    }

    public GameObject[] Cards
    {
        get { return cards; }
        set
        {
            for ( int i = 0 ; i < cards . Length ; i++ )
            {
                cards [ i ] = value [ i ];

            }
        }
    }

    public HandEvalutor ( GameObject[] sortedHand )
    {
        heartsSum = 0;
        diamondSum = 0;
        clubSum = 0;
        spadesSum = 0;
        cards = new GameObject[5] ;
        Cards = sortedHand;
        handValue = new HandValue ( );
    }

    public Hand EvaluateHand ( )
    {
        getNumberOfSuit ( );
        if ( StraightFlush ( ) )
            return Hand . StraightFlush;
        else if ( FourOfKind ( ) )
            return Hand . FourKind;
        else if ( FullHouse ( ) )
            return Hand . FullHouse;
        else if ( Flush ( ) )
            return Hand . Flush;
        else if ( Straight ( ) )
            return Hand . Straight;
        else if ( ThreeOfKind ( ) )
            return Hand . ThreeKind;
        else if ( TwoPairs ( ) )
            return Hand . TwoPairs;
        else if ( OnePair ( ) )
            return Hand . OnePair;

        //if the hand is nothing, than the player with highest card wins
        handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable>().Value;
        return Hand . Nothing;
    }




    private void getNumberOfSuit ( )
    {
        foreach ( var element in Cards )
        {
            if ( element . GetComponent<Selectable> ( ) . Suit . Equals( Selectable . SUIT . HEART ))
                heartsSum++;
            else if ( element . GetComponent<Selectable> ( ) . Suit . Equals ( Selectable . SUIT . DIAMOND ) )
                diamondSum++;
            else if ( element . GetComponent<Selectable> ( ) . Suit . Equals ( Selectable . SUIT . CLUB ) )
                clubSum++;
            else if ( element . GetComponent<Selectable> ( ) . Suit . Equals ( Selectable . SUIT . SPADE ) )
                spadesSum++;
        }
    }

    private bool StraightFlush ( )
    {
        if ( Flush ( ) && Straight ( ) )
        {
            handValue . Total = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        return false;
    } 

    private bool FourOfKind ( )
    {
        //if the first 4 cards, add values of the four cards and last card is the highest
        if ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value && cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value && cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value * 4;
            handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value && cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value && cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value * 4;
            handValue . HighCard = ( int ) cards [ 0 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }

        return false;
    }

    private bool FullHouse ( )
    {
        if ( ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value && cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value && cards [ 3 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value ) ||
            ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value && cards [ 2 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value && cards [ 2 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value ) )
        {
            handValue . Total = ( int ) ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value ) + ( int ) ( cards [ 1 ] . GetComponent<Selectable> ( ) . Value ) + ( int ) ( cards [ 2 ] . GetComponent<Selectable> ( ) . Value ) +
                ( int ) ( cards [ 3 ] . GetComponent<Selectable> ( ) . Value ) + ( int ) ( cards [ 4 ] . GetComponent<Selectable> ( ) . Value );
            return true;
        }

        return false;
    }

    private bool Flush ( )
    {
        //if all suits are the same
        if ( heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5 )
        {
            //if flush, the player with higher cards win
            //whoever has the last card the highest, has automatically all the cards total higher
            handValue . Total = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }

        return false;
    }

    private bool Straight ( )
    {
        //if 5 consecutive values
        if ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value + 1 == cards [ 1 ] . GetComponent<Selectable> ( ) . Value &&
            cards [ 1 ] . GetComponent<Selectable> ( ) . Value + 1 == cards [ 2 ] . GetComponent<Selectable> ( ) . Value &&
            cards [ 2 ] . GetComponent<Selectable> ( ) . Value + 1 == cards [ 3 ] . GetComponent<Selectable> ( ) . Value &&
            cards [ 3 ] . GetComponent<Selectable> ( ) . Value + 1 == cards [ 4 ] . GetComponent<Selectable> ( ) . Value )
        {
            //player with the highest value of the last card wins
            handValue . Total = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }

        return false;
    }

    private bool ThreeOfKind ( )
    {
        //if the 1,2,3 cards are the same OR
        //2,3,4 cards are the same OR
        //3,4,5 cards are the same
        //3rds card will always be a part of Three of A Kind
        if ( ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value && cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value ) ||
        ( cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value && cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value ) )
        {
            handValue . Total = ( int ) cards [ 2 ] . GetComponent<Selectable> ( ) . Value * 3;
            handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 2 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value && cards [ 2 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 2 ] . GetComponent<Selectable> ( ) . Value * 3;
            handValue . HighCard = ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        return false;
    }

        private bool TwoPairs ( )
    {
        //if 1,2 and 3,4
        //if 1.2 and 4,5
        //if 2.3 and 4,5
        //with two pairs, the 2nd card will always be a part of one pair 
        //and 4th card will always be a part of second pair
        if ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value && cards [ 2 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value * 2 ) + ( ( int ) cards [ 3 ] . GetComponent<Selectable> ( ) . Value * 2 );
            handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value && cards [ 3 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value * 2 ) + ( ( int ) cards [ 3 ] . GetComponent<Selectable> ( ) . Value * 2 );
            handValue . HighCard = ( int ) cards [ 2 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value && cards [ 3 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value * 2 ) + ( ( int ) cards [ 3 ] . GetComponent<Selectable> ( ) . Value * 2 );
            handValue . HighCard = ( int ) cards [ 0 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        return false;
    }

    private bool OnePair ( )
    {
        //if 1,2 -> 5th card has the highest value
        //2.3
        //3,4
        //4,5 -> card #3 has the highest value
        if ( cards [ 0 ] . GetComponent<Selectable> ( ) . Value == cards [ 1 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 0 ] . GetComponent<Selectable> ( ) . Value * 2;
            handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 1 ] . GetComponent<Selectable> ( ) . Value == cards [ 2 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 1 ] . GetComponent<Selectable> ( ) . Value * 2;
            handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 2 ] . GetComponent<Selectable> ( ) . Value == cards [ 3 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 2 ] . GetComponent<Selectable> ( ) . Value * 2;
            handValue . HighCard = ( int ) cards [ 4 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }
        else if ( cards [ 3 ] . GetComponent<Selectable> ( ) . Value == cards [ 4 ] . GetComponent<Selectable> ( ) . Value )
        {
            handValue . Total = ( int ) cards [ 3 ] . GetComponent<Selectable> ( ) . Value * 2;
            handValue . HighCard = ( int ) cards [ 2 ] . GetComponent<Selectable> ( ) . Value;
            return true;
        }

        return false;
    }

}
