using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceObject : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public DiceState State
    {
        get;
        set;
    }
}

public enum DiceState
{
    InDeck,
    Rolling,
    InPlay,
}