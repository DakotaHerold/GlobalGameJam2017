using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProjectile : MonoBehaviour {

    public float lifeSpan;
    public float rotationSpeed;
    public float moveSpeed;
    public float damage;
    public int playerNum;

    private string horizontalMovementAxis;

    PlayerScript controllingPlayer;
    CharacterController3D controller;
    private float timer = 0.0f;
    private Rigidbody body;
    
	// Use this for initialization
	void Start () {
        controllingPlayer = GameManager.gmInstance.GetWavePlayer();
        controller = controllingPlayer.controller;
        body = GetComponent<Rigidbody>();

        //Debug.Log("Wave player : " + controller.playerNumber);
        body.transform.forward = controller.transform.forward; 
        horizontalMovementAxis = "Horizontal" + playerNum;
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
        transform.Rotate(0, Input.GetAxis(horizontalMovementAxis) * rotationSpeed, 0);

        Vector3 targetVelocity = transform.forward.normalized * moveSpeed;
        targetVelocity.y = 0;
        body.velocity = targetVelocity;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject; 
        if(obj.tag == "wall")
        {
            Destroy(gameObject); 
        }
        else if (obj.tag == "enemy")
        {
            EnemyScript enemy = obj.GetComponent<EnemyScript>();
            enemy.TakeDamage(damage);
            DestroyObject(gameObject); 
        }
    }
}
