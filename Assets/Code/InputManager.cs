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
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_Touch.Start = Input.mousePosition;
            
            foreach(DiceObject die in m_Dice)
            {
                if(die.State == DiceState.Rolling)
                {
                    die.Lift();
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            m_Touch.End = Input.mousePosition;
            Vector3 velocity = m_Touch.End - m_Touch.Start;
            foreach(DiceObject die in m_Dice)
            {
                if (die.State == DiceState.Rolling)
                {
                    die.Roll();
                }
            }
        }
	}
}