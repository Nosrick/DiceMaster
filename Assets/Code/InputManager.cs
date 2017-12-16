using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Touch m_Touch;
    private GameObject m_LastTouched;

    private bool m_Spinning;
    private float m_Timer;
    private const float MAX_TIMER = 1.5f;

    private const int SPIN_SPEED = 360;
    private const float SPIN_MODIFIER = 10.0f;

	// Use this for initialization
	void Start ()
    {
        m_Touch = new Touch();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_Spinning)
        {
            m_Timer -= Time.deltaTime;

            if(m_Timer <= 0.0f)
            {
                m_Spinning = false;

                m_LastTouched.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            m_Touch.Start = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.name.StartsWith("Dice"))
                {
                    hit.transform.position = ray.origin + ray.direction * 10;
                    m_LastTouched = hit.transform.gameObject;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            m_Touch.End = Input.mousePosition;
            Vector3 velocity = m_Touch.End - m_Touch.Start;
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3((Random.value - 0.5f) * SPIN_SPEED, (Random.value - 0.5f) * SPIN_SPEED, (Random.value - 0.5f) * SPIN_SPEED);

            m_LastTouched.GetComponent<Rigidbody>().useGravity = false;
            //m_LastTouched.GetComponent<Rigidbody>().velocity = velocity / 10;
            m_LastTouched.GetComponent<Rigidbody>().angularVelocity = rotation.eulerAngles * SPIN_MODIFIER;
            m_LastTouched.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            m_Timer = MAX_TIMER;
            m_Spinning = true;
        }
	}
}