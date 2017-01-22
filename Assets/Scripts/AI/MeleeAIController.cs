using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAIController : EnemyScript {

    public float damageInterval;
    

    [HideInInspector]
    public bool attacking = false;

    private float damageTimer = 0.0f; 
    private SphereCollider trigger; 

    private Vector3 moveDirection = Vector3.zero;
    private PlayerScript player; 

    // Use this for initialization
    new void Start()
    {
        base.Start(); 

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); 
        // Add the time since Update was last called to the timer.
        damageTimer += Time.deltaTime;

        // Seek player if agent exists
        if (agent == null)
            return;


        

        agent.SetDestination(target.position);
        

        

        if(damageTimer >= damageInterval && attacking == true)
        {
            DealDamage(); 
        }
        
    }

    new void Death()
    {
        base.Death(); 
    }

    // Trigger functions 
    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        player = collidedObject.GetComponent<PlayerScript>();
        if(player != null)
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
        }
    }

    void DealDamage()
    {
        damageTimer = 0.0f;
        // Asserts player is not null
        if (player != null)
        {
            player.isHit = true;
            player.TakeDamage(damage);
            //Debug.Log("Player health:" + player.health);
        }
        
    }

    //IEnumerator DealDamageRoutine(PlayerScript player)
    //{
    //    while(attacking)
    //    {
    //        DealDamage(player);
    //        yield return new WaitForSeconds(damageInterval); 
    //    }
    //}



}
