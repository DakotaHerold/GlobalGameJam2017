using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAIController : MonoBehaviour {

    public float speed = 1.0f;
    public float angularSpeed = 0.0f;
    public float damage = 2.0f;
    public float damageInterval = 2.0f; 
    public Transform target;

    private bool attacking = false; 
    private SphereCollider trigger; 
    private NavMeshAgent agent;
    private Vector3 moveDirection = Vector3.zero;

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
        agent.SetDestination(target.position);
    }

    // Trigger functions 
    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        PlayerScript playerScript = collidedObject.GetComponent<PlayerScript>();
        DealDamage(playerScript); 
    }

    void OnTriggerStay(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        PlayerScript playerScript = collidedObject.GetComponent<PlayerScript>();
        attacking = true; 
        StartCoroutine(DealDamageRoutine(playerScript)); 
    }

    void OnTriggerExit(Collider other)
    {
        attacking = false; 
    }

    void DealDamage(PlayerScript player)
    {
        player.health -= damage;
        Debug.Log(player.health); 
    }

    IEnumerator DealDamageRoutine(PlayerScript player)
    {
        while(attacking)
        {
            DealDamage(player);
            yield return new WaitForSeconds(damageInterval); 
        }
    }



}
