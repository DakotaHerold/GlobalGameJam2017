using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public bool isAlive = true;
    public float frequency; // speed
    public float magnitude; //size 
    public Transform target; 

    private Vector3 pos;
    private Vector3 axis; 
    private Rigidbody body;
    public float speed; 
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        pos = body.transform.position;
        axis = body.transform.up;
        //speed = body.velocity.magnitude; 
    }
	
	// Update is called once per frame
	void Update () {
		if(!isAlive)
        {
            Destroy(gameObject);
        }
        //Debug.Log("Y: " + transform.position.y); 
        // Move y velocity in sin wave 

        // Move in Sine Wave 
        pos += body.transform.forward * Time.deltaTime * speed;
        body.transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;


        //Vector3 targetvelocity = transform.forward.normalized * speed;
        //targetvelocity.y = 0;
        //body.velocity = targetvelocity;
    }

    void FixedUpdate()
    {
         
    }

    void OnCollisionEnter(Collision collision)
    { 
        isAlive = false; 
    }


}
