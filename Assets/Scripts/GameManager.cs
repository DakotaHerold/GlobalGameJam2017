using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    GameObject[] players;
	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("player") as GameObject[];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
