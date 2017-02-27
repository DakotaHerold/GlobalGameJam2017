using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAIController : EnemyScript {

    public float damageInterval;
    private AudioSource audio; 

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
        audio = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    new void Update()
    {
        //base.anim.SetBool("GotHit", false);
        //base.anim.SetBool("IsAttacking", false);
        base.Update(); 
        // Add the time since Update was last called to the timer.
        damageTimer += Time.deltaTime;

        // Seek player if agent exists
        if (agent != null)
        {
            if (target == null)
            {
                SetClosestPlayerToTarget();
                return;
            }

            agent.SetDestination(target.position);
            //base.anim.SetBool("IsMoving", true);

            if (damageTimer >= damageInterval && attacking == true)
            {
                base.anim.SetBool("IsAttacking", true);
                DealDamage();
            }
            else
            {
                //base.anim.SetBool("IsAttacking", false);
            }
        }
        
    }

    new void Death()
    {
        base.Death(); 
    }

    public override void TakeDamage(float damg)
    {
        base.TakeDamage(damg);

        audio.PlayOneShot(audio.clip, 1.0f);
        
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
            //
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
