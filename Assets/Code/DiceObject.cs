using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceObject : MonoBehaviour
{
    private bool m_Spinning;
    private float m_Timer;
    private const float MAX_TIMER = 1.5f;

    private const int SPIN_SPEED = 360;
    private const float SPIN_MODIFIER = 0.1f;

    private Quaternion m_Rotation;

    private DiceFace[] m_Faces;
    private Dictionary<Vector3, int> m_Values;

    private int m_LastRoll;
    private bool m_Checked;

    public void SetFaces(DiceFace[] facesRef)
    {
        m_Faces = facesRef;
    }

    // Use this for initialization
    void Start ()
    {
        m_Spinning = false;
        m_Checked = false;
        State = DiceState.InDeck;

        m_Values = new Dictionary<Vector3, int>();
        m_Values.Add(Vector3.up, 4);
        m_Values.Add(Vector3.down, 3);

        m_Values.Add(Vector3.forward, 6);
        m_Values.Add(Vector3.back, 1);

        m_Values.Add(Vector3.right, 5);
        m_Values.Add(Vector3.left, 2);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_Timer -= Time.deltaTime;

        if (m_Spinning)
        {
            transform.Rotate(m_Rotation.eulerAngles);

            if (m_Timer <= 0.0f)
            {
                m_Spinning = false;

                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                m_Timer = MAX_TIMER;
            }
        }

        if(m_LastRoll == 0 && !m_Spinning && !m_Checked && State == DiceState.Rolling && m_Timer <= 0)
        {
            if(GetComponent<Rigidbody>().velocity.magnitude < 5.0f)
            {
                m_LastRoll = GetFacing();
                if (m_LastRoll != 0)
                {
                    m_Checked = true;
                }
            }
        }
    }

    private int GetFacing()
    {
        foreach(Vector3 vector in m_Values.Keys)
        {
            Vector3 objectSpace = transform.InverseTransformDirection(Vector3.up);
            float angle = Vector3.Angle(objectSpace, vector);
            if(angle <= 5.0f)
            {
                Debug.Log("Facing is: " + m_Values[vector]);
                return m_Values[vector];
            }
        }

        return 0;
    }

    public void Roll()
    {
        m_Rotation = new Quaternion();
        m_Rotation.eulerAngles = new Vector3((UnityEngine.Random.value - 0.5f) * SPIN_SPEED, (UnityEngine.Random.value - 0.5f) * SPIN_SPEED, (UnityEngine.Random.value - 0.5f) * SPIN_SPEED) * SPIN_MODIFIER;

        m_Timer = MAX_TIMER;
        m_Spinning = true;
        m_Checked = false;
        m_LastRoll = 0;
    }

    public void Lift()
    {
        this.transform.position = this.transform.position + new Vector3(0.0f, 4.0f, 0.0f);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    public void Draw()
    {
        State = DiceState.Rolling;
    }

    public void Play()
    {
        State = DiceState.InPlay;
    }

    public void Return()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        State = DiceState.InDeck;
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