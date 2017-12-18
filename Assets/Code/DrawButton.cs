using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawButton : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.GetComponent<Button>().onClick.AddListener(gameManager.Draw);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
