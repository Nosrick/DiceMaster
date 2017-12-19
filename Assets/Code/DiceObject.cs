using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        Renderer renderer = this.GetComponent<Renderer>();
        renderer.material.color = m_Faces[0].DieColour;

        Canvas[] faces = this.GetComponentsInChildren<Canvas>();
        for(int i = 0; i < m_Faces.Length; i++)
        {
            Text[] text = faces[i].GetComponentsInChildren<Text>();
            text[0].text = m_Faces[i].Level.ToString();
            text[0].color = m_Faces[i].TextColour;
            text[1].color = m_Faces[i].TextColour;
            text[2].color = m_Faces[i].TextColour;

            Image image = faces[i].GetComponentInChildren<Image>();
            image.sprite = m_Faces[i].Image;
            image.color = m_Faces[i].TextColour;

            if (m_Faces[i].Face == FaceType.Play)
            {
                PlayFace playFace = (PlayFace)m_Faces[i];
                text[1].text = playFace.Attack.ToString();
                text[2].text = playFace.Toughness.ToString();
            }
            else if(m_Faces[i].Face == FaceType.Special)
            {
                text[1].text = "";
                text[2].text = "";
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (m_Faces == null)
        {
            m_Faces = new DiceFace[6];
        }

        m_Spinning = false;
        m_Checked = false;
        State = DiceState.InDeck;

        m_Values = new Dictionary<Vector3, int>();
        m_Values.Add(Vector3.up, 3);
        m_Values.Add(Vector3.down, 2);

        m_Values.Add(Vector3.forward, 5);
        m_Values.Add(Vector3.back, 0);

        m_Values.Add(Vector3.right, 4);
        m_Values.Add(Vector3.left, 1);
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
                //Debug.Log("Facing is: " + m_Values[vector]);
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
        this.transform.position = this.transform.position + new Vector3(0.0f, 3.0f, 0.0f);
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    public void Lower()
    {
        this.transform.position = this.transform.position - new Vector3(0.0f, 3.0f, 0.0f);
    }

    public void Draw()
    {
        State = DiceState.Rolling;
    }

    public void Play()
    {
        State = DiceState.InPlay;
    }

    public void Spend()
    {
        Spent = true;
    }

    public void NewTurn()
    {
        Spent = false;
        m_LastRoll = 0;
        m_Spinning = false;
        m_Checked = false;
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

    public bool Selected
    {
        get;
        set;
    }

    public bool Spent
    {
        get;
        private set;
    }

    public DiceFace FaceUp
    {
        get
        {
            return m_Faces[m_LastRoll];
        }
    }
}

public enum DiceState
{
    InDeck,
    Rolling,
    InPlay,
}