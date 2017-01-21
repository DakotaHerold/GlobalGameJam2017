using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public bool isAlive = true;

    private Rigidbody body; 
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if(!isAlive)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        
        isAlive = false; 
    }


}
