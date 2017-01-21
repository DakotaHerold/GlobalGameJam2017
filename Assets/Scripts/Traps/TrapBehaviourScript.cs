using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviourScript : MonoBehaviour {


    public float damage;
    public float damageInterval; 

    private float damageTimer = 0.0f;
    private bool dealDamage = false;
    private PlayerScript player = null;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        // Add the time since Update was last called to the timer.
        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval && dealDamage == true)
        {
            DealDamage();
        }

    }

    // Trigger functions 
    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        player = collidedObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            dealDamage = true;
        }


    }

    void OnTriggerExit(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        player = collidedObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            dealDamage = false;
            player = null;
        }
    }

    void DealDamage()
    {
        damageTimer = 0.0f;
        // Asserts player is not null
        player.TakeDamage(damage);
        Debug.Log("Player health:" + player.health);
    }
}
