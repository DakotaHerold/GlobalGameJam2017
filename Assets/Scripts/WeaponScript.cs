﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Parent " + this.transform.parent);
        if (other.gameObject.tag == "enemy" && transform.parent != null)
        {
            if (this.GetComponentInParent<PlayerAttack>().combo ==  0)
            {
                other.GetComponent<EnemyScript>().contact = 0;
                //other.GetComponent<Renderer>().material.color = other.GetComponent<EnemyScript>().GetCurrentColor();
            }
            //Debug.Log("Collision");
            if (this.GetComponentInParent<PlayerAttack>().isAttacking == true && other.GetComponent<EnemyScript>().contact != this.GetComponentInParent<PlayerAttack>().combo)
            {
                Debug.Log("Hit!");
                other.GetComponent<Renderer>().material.color = Color.red;
                other.GetComponent<EnemyScript>().TakeDamage(player.GetComponent<PlayerAttack>().attDamage);
                other.GetComponent<EnemyScript>().contact = player.GetComponent<PlayerAttack>().combo;
            }
            else
            {
                other.GetComponent<Renderer>().material.color = other.GetComponent<EnemyScript>().GetCurrentColor();
            }
        }
    }
}
