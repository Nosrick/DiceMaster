using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<BasePlayer> m_Players;
    private int m_CurrentPlayer;

	// Use this for initialization
	void Start ()
    {
        State = TurnState.Draw;
        m_CurrentPlayer = 0;
        DiceObject diceObject = Resources.Load<DiceObject>("Prefabs/Dice");
        List<DiceObject> dice = new List<DiceObject>();

        SpriteLoader.Load();
        DiceFaceLoader.Load();

        DiceFace[] faces = new DiceFace[6];
        for(int i = 1; i < 7; i++)
        {
            faces[i - 1] = DiceFaceLoader.Get("DebugCat-1"); //new PlayFace(i, i, i, FaceType.Play, Color.black, Color.cyan, "DEFAULT-" + i, MaterialLoader.Get("DebugCat"));
            DiceFaceLoader.Serialise(faces[i - 1]);
        }

        for(int i = 0; i < 18; i++)
        {
            DiceObject instance = GameObject.Instantiate<DiceObject>(diceObject);
            instance.SetFaces(faces);
            instance.transform.position = new Vector3(-10.0f, 2.0f, -10.0f);
            instance.GetComponent<Rigidbody>().isKinematic = true;
            dice.Add(instance);
        }
        m_Players = new List<BasePlayer>();
        CreatePlayer(dice, typeof(ControllingPlayer));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetState(TurnState stateRef)
    {
        State = stateRef;
    }

    public void Draw()
    {
        if (State == TurnState.Draw)
        {
            m_Players[m_CurrentPlayer].Draw();
            State = TurnState.Roll;
        }
    }

    private void CreatePlayer(List<DiceObject> diceRef, Type playerType)
    {
        if (playerType == typeof(ControllingPlayer))
        {
            m_Players.Add(new ControllingPlayer(diceRef));
            GameObject player = new GameObject("Player" + m_Players.Count);

            foreach(DiceObject die in diceRef)
            {
                die.transform.parent = player.transform;
            }

            player.transform.parent = this.transform;
        }
    }

    public List<BasePlayer> Players
    {
        get
        {
            return m_Players;
        }
    }

    public BasePlayer ActingPlayer
    {
        get
        {
            return m_Players[m_CurrentPlayer];
        }
    }

    public TurnState State
    {
        get;
        protected set;
    }
}

public enum TurnState
{
    Draw,
    Roll,
    Play,
    Attack,
    End
}