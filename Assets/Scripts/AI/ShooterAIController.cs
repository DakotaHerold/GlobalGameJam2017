using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterAIController : MonoBehaviour {

    public float speed = 1.0f;
    public float angularSpeed = 0.0f;
    public bool shooting = false; 
    
    public Transform target;

    private NavMeshAgent agent;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //target = transform.GetChild(0).GetComponent<Transform>(); 

        agent.speed = speed;
        if (angularSpeed == 0.0f)
        {
            agent.angularSpeed = angularSpeed;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current Transform: " + transform.position + " Target Transform" + target.position); 
        if (target != null)
        { 
            if (!shooting)
            {
                agent.SetDestination(target.position);
            }
            else
            {

                // Shooting 
            }
        }
        
    }

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget; 
    }
}
