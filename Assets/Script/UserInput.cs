using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System . Linq;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private Game game;
    private MainMenu mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<Game> ( );
        mainMenu = FindObjectOfType<MainMenu> ( );
        slot1 = this . gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if ( Input . GetMouseButtonDown ( 0 ) )
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if ( hit )
            {
                if ( hit . collider . CompareTag ( "Card" ) )
                {
                    Card ( hit . collider . gameObject );
                }
                else if ( hit . collider . CompareTag ( "FrontHand" ) )
                {
                    FrontHand ( hit . collider . gameObject );
                }
                else if ( hit . collider . CompareTag ( "MiddleHand" ) )
                {
                    MiddleHand ( hit . collider . gameObject );
                }
                else if ( hit . collider . CompareTag ( "BackHand" ) )
                {

                    BackHand ( hit . collider . gameObject );
                }

            }


        }
        
    }

    void Card(GameObject selected)
    {

        if ( !selected . GetComponent<Selectable> ( ) . faceUp )
        {
            return;
        }
        else
        {
            if ( slot1 == this . gameObject )
            {
                slot1 = selected;
            }

            else if ( slot1 != selected )
            {
                slot1 = selected;
            }
        }

    }

    void FrontHand(GameObject selected)
    {
        {
            if ( slot1 . CompareTag ( "Card" ) )
            {
                MoveCard ( selected );
                game . PlayerFrontHand . Add ( slot1 );
                game . PlayerCard . Remove ( slot1 );
                float xOffset = 1.5f;
                float zOffset = 2f;
                foreach ( GameObject card in game . PlayerFrontHand )
                {
                    card . transform . position = new Vector3 ( game . handPos [ 0 ] . transform . position . x + xOffset , game . handPos [ 0 ] . transform . position . y , game . handPos [ 0 ] . transform . position . z - zOffset );
                    xOffset = xOffset - 1.5f;
                    zOffset = zOffset - 0f;
                }
            }
        }
    }
    void MiddleHand( GameObject selected )
    {
            if ( slot1 . CompareTag ( "Card" ) )
            {
                MoveCard ( selected );
                game . PlayerMiddleHand . Add ( slot1 );
                game . PlayerCard . Remove ( slot1 );
                float xOffset = 3f;
                float zOffset = 2f;
                foreach ( GameObject card in game . PlayerMiddleHand )
                {
                    card . transform . position = new Vector3 ( game . handPos [ 1 ] . transform . position . x + xOffset , game . handPos [ 1 ] . transform . position . y , game . handPos [ 1 ] . transform . position . z - zOffset );
                    xOffset = xOffset - 1.5f;
                    zOffset = zOffset - 0f;
                }
            }
        
    }
    void BackHand( GameObject selected )
    {
        if ( slot1 . CompareTag ( "Card" ) )
        {
            MoveCard ( selected );
            game . PlayerBackHand . Add ( slot1 );
            game . PlayerCard . Remove ( slot1 );
            float xOffset = 3f;
            float zOffset = 2f;
            foreach ( GameObject card in game . PlayerBackHand )
            {
                card . transform . position = new Vector3 ( game . handPos [ 2 ] . transform . position . x + xOffset , game . handPos [ 2 ] . transform . position . y , game . handPos [ 2 ] . transform . position . z - zOffset );
                xOffset = xOffset - 1.5f;
                zOffset = zOffset - 0f;
            }
        }
    }


    void MoveCard ( GameObject selected )
    {
        Selectable s1= slot1.GetComponent<Selectable>();
        Selectable s2= selected.GetComponent<Selectable>();

        slot1 . transform . position =  new Vector3 ( selected . transform . position . x , selected . transform . position . y , selected . transform . position . z - 0.01f );
        slot1 . transform . parent = selected . transform;
    }
}
