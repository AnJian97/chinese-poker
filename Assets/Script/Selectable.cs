using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public int countDiamonds;
    public bool faceUp = false;


    private string valueString;
    private string suitString;

    public VALUE value = VALUE . TWO;
    public SUIT suit=SUIT.DIAMOND;

    public SUIT Suit
    {
        get
        {
            return suit;
        }

        set
        {
              suit =  value ;
        }
    }

    public VALUE Value
    {
        get
        {
            return value;
        }

        set
        {
            this . value =  value ;
        }
    }

    public enum VALUE
    {
        TWO=2,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        ACE
    }

    public enum SUIT 
    {
        DIAMOND,
        CLUB,
        HEART,
        SPADE
    }


    // Start is called before the first frame update
    void Start()
    {

        if (CompareTag("Card"))
        {

            suitString = transform.name[0].ToString();


            if (suitString == "D")
            {
                Suit =  SUIT.DIAMOND;
            }
            if (suitString == "C")
            {
                Suit =  SUIT . CLUB;
            }
            if (suitString == "H")
            {
                Suit =  SUIT .HEART;
            }
            if (suitString == "S")
            {
                Suit =  SUIT .SPADE;
            }

            for (int i = 1; i < transform.name.Length; i++)
            {
                char c = transform.name[i];
                valueString = valueString + c.ToString();
            }

            if (valueString == "2")
            {
                Value =  VALUE .TWO;
            }
            if (valueString == "3")
            {
                Value =  VALUE .THREE;
            }
            if (valueString == "4")
            {
                Value =  VALUE .FOUR;
            }
            if (valueString == "5")
            {
                Value =  VALUE .FIVE;
            }
            if (valueString == "6")
            {
                Value =  VALUE .SIX;
            }
            if (valueString == "7")
            {
                Value =  VALUE .SEVEN;
            }
            if (valueString == "8")
            {
                Value =  VALUE .EIGHT;
            }
            if (valueString == "9")
            {
                Value =  VALUE .NINE;
            }
            if (valueString == "10")
            {
                Value =  VALUE .TEN;
            }
            if (valueString == "J")
            {
                Value =  VALUE .JACK;
            }
            if (valueString == "Q")
            {
                Value =  VALUE .QUEEN;
            }
            if (valueString == "K")
            {
                Value =  VALUE .KING;
            }
            if (valueString == "A")
            {
                Value =  VALUE .ACE;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake ( )
    {
    
}

}
