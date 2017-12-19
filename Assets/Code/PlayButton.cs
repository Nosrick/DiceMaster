using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Parent = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.GetComponent<Button>().onClick.AddListener(Play);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Play()
    {
        if(Parent.State != TurnState.Play)
        {
            return;
        }

        if(Parent.ActingPlayer.Play())
        {
            Parent.SetState(TurnState.Attack);
        }
    }

    public GameManager Parent
    {
        get;
        set;
    }
}
