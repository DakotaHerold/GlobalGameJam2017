using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAIController : MonoBehaviour {

    public float speed = 1.0f;
    public float angularSpeed = 0.0f;
    public float damage;
    public float damageInterval;
    public Transform target;

    [HideInInspector]
    public bool attacking = false;

    private float damageTimer = 0.0f; 
    private SphereCollider trigger; 
    private NavMeshAgent agent;
    private Vector3 moveDirection = Vector3.zero;
    private PlayerScript player; 

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed; 
        if(angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed; 
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }

    void DealDamage()
    {
        damageTimer = 0.0f;
        // Asserts player is not null
        player.TakeDamage(damage);
        //Debug.Log("Player health:" + player.health);
        
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
