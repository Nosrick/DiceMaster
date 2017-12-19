using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Touch m_Touch;
    private DiceObject[] m_Dice;

	// Use this for initialization
	void Start ()
    {
        m_Touch = new Touch();
        m_Dice = FindObjectsOfType<DiceObject>();

        Parent = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(Parent.State)
        {
            case TurnState.Roll:
                if (Input.GetMouseButtonDown(0))
                {
                    m_Touch.Start = Input.mousePosition;

                    foreach (DiceObject die in m_Dice)
                    {
                        if (die.State == DiceState.Rolling)
                        {
                            die.Lift();
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    m_Touch.End = Input.mousePosition;
                    Vector3 velocity = m_Touch.End - m_Touch.Start;
                    foreach (DiceObject die in m_Dice)
                    {
                        if (die.State == DiceState.Rolling)
                        {
                            die.Roll();
                            Parent.SetState(TurnState.Play);
                        }
                    }
                }
                break;

            case TurnState.Play:
                if(Input.GetMouseButtonUp(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if(Physics.Raycast(ray, out hit, 100.0f))
                    {
                        if(hit.transform.name.StartsWith("Dice"))
                        {
                            DiceObject die = hit.transform.gameObject.GetComponent<DiceObject>();
                            if(die.Selected == false)
                            {
                                die.Lift();
                                die.Selected = true;
                            }
                            else
                            {
                                die.Lower();
                                die.Selected = false;
                            }
                        }
                    }
                }
                break;
        }
	}

    public GameManager Parent
    {
        get;
        private set;
    }
}