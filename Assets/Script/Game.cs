using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine . SceneManagement;

public class Game : MonoBehaviour
{
    private static int countDiamond,countClub,countHeart,countSpade,AIcardSuit,AIcardValue,countTwo,countThree,countFour,countFive,countSix,countSeven,countEight,countNine,countTen,countJack,countQueen,countKing,countAce;
    private Selectable selectable;
    private HandEvalutor handEvalutor;

    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject cardBox;
    public GameObject[] holderPos;
    public GameObject[] handPos;
    public Sprite Win,Lost;
    public Image img_result;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public List<string> deck;
    public List<string>[] holder;
    public List<string> plyerHolder = new List<string>();
    public List<string> AIHolder = new List<string>();

    public List<GameObject> PlayerFrontHand = new List<GameObject>();
    public List<GameObject> PlayerMiddleHand = new List<GameObject>();
    public List<GameObject> PlayerBackHand = new List<GameObject>();

    public List<GameObject> AIFrontHand = new List<GameObject>();
    public List<GameObject> AIMiddleHand = new List<GameObject>();
    public List<GameObject> AIBackHand = new List<GameObject>();

    public List<GameObject> AICard=new List<GameObject>();
    public List<GameObject> PlayerCard=new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        selectable = FindObjectOfType<Selectable> ( );
        handEvalutor = FindObjectOfType<HandEvalutor> ( );
        holder = new List<string>[] { plyerHolder, AIHolder };
        PlayCards ();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayCards()
    {

        deck = GenerateDeck();
        Shuffle (deck);
        CardAnimate ( );
        SplitCard();

    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void CardAnimate ()
    {
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(cardBox.transform.position.x, cardBox.transform.position.y, cardBox.transform.position.z), Quaternion.identity);
            newCard.name = card;
            newCard.GetComponent<Selectable>().faceUp = false;
        }
    }

    void SplitCard()
    {

        for (int j = 0; j < 13; j++)
        {
            holder[0].Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);
            holder[1].Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);
        }

        StartCoroutine(DealPlayerAnimate());
        StartCoroutine(DealAIAnimate());
 

    }

    IEnumerator DealPlayerAnimate()
    {
        float xOffset = 5.5f;
        float zOffset = -0.2f;
        foreach (string card in holder[0])
        {
            yield return new WaitForSeconds(0.1f);
            GameObject newCard = Instantiate(cardPrefab, new Vector3(holderPos[0].transform.position.x + xOffset, holderPos[0].transform.position.y, holderPos[0].transform.position.z - zOffset), Quaternion.identity, holderPos[0].transform);
            newCard.name = card;
            newCard.GetComponent<Selectable>().faceUp = true;
            PlayerCard . Add ( newCard );
            xOffset = xOffset - 1f;
            zOffset = zOffset - 0.2f;
        }
    }

    IEnumerator DealAIAnimate()
    {
        float xOffset = 0f;
        float zOffset = -0.2f;
        foreach (string card in holder[1])
        {
            yield return new WaitForSeconds(0.1f);
            GameObject newCard = Instantiate(cardPrefab, new Vector3(holderPos[1].transform.position.x + xOffset, holderPos[1].transform.position.y, holderPos[1].transform.position.z - zOffset), Quaternion.identity, holderPos[1].transform);
            newCard.name = card;
            newCard.GetComponent<Selectable>().faceUp = false;
            AICard . Add ( newCard );
            xOffset = xOffset - 0.1f;
            zOffset = zOffset - 0.2f;
        }
        BotMove ( );
    }

    void BotMove ( )
    {
        int f=AIFrontHand.Count;
        int m=AIMiddleHand.Count;
        int b=AIBackHand.Count;
        for(int i=0 ;i<AICard.Count ;i++ )
        {
            GameObject[] arrayOfAICard=AICard.ToArray();
            HandEvalutor AIMove=new HandEvalutor(arrayOfAICard);
            HandEvalutor.Hand AIM=AIMove.EvaluateHand();

            if ( AIM == HandEvalutor . Hand . StraightFlush )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add (temp);
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . FourKind )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . FullHouse )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . Flush )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . Straight )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIFrontHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . ThreeKind )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( f == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIFrontHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . TwoPairs )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
            else if ( AIM == HandEvalutor . Hand . OnePair )
            {
                if ( b == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIBackHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( m == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIMiddleHand . Add ( temp );
                    AICard . Remove ( temp );
                }
                else if ( f == 0 )
                {
                    var temp=Instantiate(Resources.Load("AIM"),Vector3.zero,Quaternion.identity)as GameObject;
                    AIFrontHand . Add ( temp );
                    AICard . Remove ( temp );
                }
            }
        }
    }

    public void Result ( )
    {
        int resultPlayerFH=0;
        int resulPlayerMH=0;
        int resultPlayerBH=0;

        int resultAIFH=0;
        int resulAIMH=0;
        int resultAIBH=0;

        int resultPlayer=0;
        int resultAI=0;

        GameObject[] arrayOfAIFrontHandCard=AIFrontHand.ToArray();
        GameObject[] arrayOfAIMiddleHandCard=AIMiddleHand.ToArray();
        GameObject[] arrayOfAIBackHandCard=AIBackHand.ToArray();

        GameObject[] arrayOfPlayerFrontHandCard=PlayerFrontHand.ToArray();
        GameObject[] arrayOfPlayerMiddleHandCard=PlayerMiddleHand.ToArray();
        GameObject[] arrayOfPlayerBackHandCard=PlayerBackHand.ToArray();

        HandEvalutor AIFront=new HandEvalutor(arrayOfAIFrontHandCard);
        HandEvalutor AIMiddle=new HandEvalutor(arrayOfAIMiddleHandCard);
        HandEvalutor AIBack=new HandEvalutor(arrayOfAIBackHandCard);

        HandEvalutor PlayerFront=new HandEvalutor(arrayOfPlayerFrontHandCard);
        HandEvalutor PlayerMiddle=new HandEvalutor(arrayOfPlayerMiddleHandCard);
        HandEvalutor PlayerBack=new HandEvalutor(arrayOfPlayerBackHandCard);

        HandEvalutor.Hand AIFHC=AIFront.EvaluateHand();
        HandEvalutor.Hand AIMHC=AIMiddle.EvaluateHand();
        HandEvalutor.Hand AIBHC=AIBack.EvaluateHand();

        HandEvalutor.Hand PlayerFHC=PlayerFront.EvaluateHand();
        HandEvalutor.Hand PlayerMHC=PlayerMiddle.EvaluateHand();
        HandEvalutor.Hand PlayerBHC=PlayerBack.EvaluateHand();

        if ( PlayerFHC > AIFHC )
        {
            resultPlayerFH++;
        }
        else if ( PlayerFHC < AIFHC )
        {
            resultAIFH++;
        }

        if ( PlayerMHC > AIMHC )
        {
            resulPlayerMH++;
        }
        else if ( PlayerMHC < AIMHC )
        {
            resulAIMH++;
        }

        if ( PlayerBHC > AIBHC )
        {
            resultPlayerBH++;
        }
        else if ( PlayerBHC < AIBHC )
        {
            resultAIBH++;
        }

        resultPlayer = resultPlayerFH + resulPlayerMH + resultPlayerBH;
        resultAI = resultAIFH + resulAIMH + resultAIBH;

        if ( resultPlayer > resultAI )
        {
            img_result . enabled = true;
            img_result . sprite = Win;
        }
        else if ( resultPlayer < resultAI )
        {
            img_result . enabled = true;
            img_result . sprite = Lost;
        }

    }

    public void PlayAgain ( )
    {
        SceneManager . LoadScene ( "GameScene" );

    }




}
