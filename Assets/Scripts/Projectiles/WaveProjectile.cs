using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProjectile : MonoBehaviour {

    public float lifeSpan;
    public float rotationSpeed;
    public float moveSpeed; 

    PlayerScript controllingPlayer;
    CharacterController3D controller;
    private float timer = 0.0f;
    private Rigidbody body; 

	// Use this for initialization
	void Start () {
        controllingPlayer = GameManager.gmInstance.GetWavePlayer();
        controller = controllingPlayer.controller;
        body = GetComponent<Rigidbody>(); 
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime; 
        // Check projectile is still valid
        if(timer > lifeSpan)
        {
            Destroy(gameObject); 
        }

        // Apply rotation 
        transform.Rotate(0, controller.GetTriggerAxisValue() * rotationSpeed, 0);


        Vector3 targetVelocity = transform.forward.normalized * moveSpeed;
        targetVelocity.y = 0;
        body.velocity = targetVelocity;
    }
}
