﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAIController : EnemyScript {

    public float shootingSpeed = 10.0f; 
    public float shootingStartTime = 0.0f;
    public float shootingInterval = 5.0f;
    public float shotBufferY; 
    public bool shooting = false;

    public GameObject projectile;
    public float amplitudeY;
    public float omegaY;

    private float damageTimer = 0.0f;
    private PlayerScript player;

    private bool attacking = false; 

    // Use this for initialization
    new void Start()
    {
        base.Start(); 

        InvokeRepeating("LaunchProjectile", shootingInterval, shootingInterval); 
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //Debug.Log("Current Transform: " + transform.position + " Target Transform" + target.position); 

        if(agent == null)
        {
            return;
        }

        if (target == null)
        {
            SetClosestPlayerToTarget();
            return;
        }

        // Rotate towards target
        //Vector3 targetDir = target.position - transform.position;
        //float step = angularSpeed * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        ////Debug.DrawRay(transform.position, newDir, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDir);

        if (!shooting)
        {
            damageTimer += Time.deltaTime;

            // translate towards target 
            agent.SetDestination(target.position);

            if (damageTimer >= 1.0f && attacking == true)
            {
                
                DealDamageMelee();
            }
        }
        
        
    }

    new void Death()
    {
        base.Death();
    }

    void LaunchProjectile()
    {
        shooting = true;
        agent.velocity = Vector3.zero; 
        // TO-DO play anim
        //Debug.Log("Shooting!");
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y + shotBufferY, transform.position.z);
        projectile.transform.forward = transform.forward;
        ProjectileScript bullet = projectile.GetComponent<ProjectileScript>();
        //bullet.target = target;
        bullet.speed = shootingSpeed;
        Instantiate(projectile);
        
        shooting = false; 
    }

    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    void DealDamageMelee()
    {
        damageTimer = 0.0f;
        // Asserts player is not null
        if (player != null)
        {
            player.isHit = true;
            player.TakeDamage(damage / 3.0f);
        }

    }

    // Trigger functions 
    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        player = collidedObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            attacking = true;
        }


    }

    void OnTriggerExit(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        player = collidedObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            attacking = false;
            player = null;
            //
        }
    }


}
