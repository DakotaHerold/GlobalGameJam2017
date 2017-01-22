﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public float health;
    public float damage;
    public float speed = 1.0f;
    public float angularSpeed = 0.0f;
    public int tears;
    public bool isDead;
    public Transform target;

    protected Animator anim;

    [HideInInspector]
    public int contact = 0;


    private Color currentColor;
    public NavMeshAgent agent;

    public virtual void Start()
    {
        //tears = 0;
        currentColor = GetComponent<Renderer>().material.color;
        isDead = false;
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        agent.speed = speed;
        if (angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Rotate towards target
        Vector3 targetDir = target.position - transform.position;
        float step = angularSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        //Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);

        //Death(); 
        if (isDead)
        {
            anim.SetBool("IsDead", true);
            Death(); 
        }
    }
    public virtual void TakeDamage(float damg)
    {
        Debug.Log("Health before " + health);
        anim.SetBool("GotHit", true);
        health -= damg;
        if(health <= 0)
        {
            isDead = true; 
        }
        Debug.Log("After: " + health);

        gameObject.GetComponent<Animator>().Play("Hit");
    }
    public virtual void Death()
    {
        // TO DO play death anim 
        gameObject.GetComponent<Animator>().Play("Death");
        agent = null; 
        if(GameManager.gmInstance.enemies.Contains(this.gameObject))
        {
            GameManager.gmInstance.enemies.Remove(this.gameObject); 
        }
        
        DestroyObject(gameObject); 
    }
    
    // Getters 
    public Color GetCurrentColor()
    {
        return currentColor; 
    }

    public void SetClosestPlayerToTarget()
    {
        GameObject[] playersObjects = GameObject.FindGameObjectsWithTag("player") as GameObject[];

        GameObject closestPlayer = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject player in playersObjects)
        {
            float currentDist = Vector3.Distance(player.transform.position, transform.position);
            if (currentDist < minDist)
            {
                minDist = currentDist;
                closestPlayer = player;
            }
        }

        if(closestPlayer != null)
        {
            target = closestPlayer.transform; 
        }
        else
        {
            Debug.Log(gameObject.name + " couldn't find a closest player in scene!"); 
        }
        //Debug.Log("Closest player set for " + gameObject.name);
    }
}
