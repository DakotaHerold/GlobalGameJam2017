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
    private NavMeshAgent agent;
    private Vector3 moveDirection = Vector3.zero;
    private PlayerScript player; 

    // Use this for initialization
    new void Start()
    {
        base.Start(); 
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed; 
        if(angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed; 
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); 
        // Add the time since Update was last called to the timer.
        damageTimer += Time.deltaTime;

        // Search for player 
        agent.SetDestination(target.position);

        if(damageTimer >= damageInterval && attacking == true)
        {
            DealDamage(); 
        }
        
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
            player.TakeDamage(damage);
            Debug.Log("Player health:" + player.health);
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
