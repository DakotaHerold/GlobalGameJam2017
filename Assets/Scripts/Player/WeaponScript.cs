using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour 
{

    // Use this for initialization
    public GameObject player;
    public float spinSpeed;
    //public AudioClip hitSound; 
    private PlayerAttack pAttack;


    void Start()
    {
        pAttack = this.GetComponentInParent<PlayerAttack>(); 
    }

	// Update is called once per frame
	void Update ()
    {
        // spin the weapon
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collision)
    {
        //audioSource.Play();
        if(collision.gameObject.tag == "enemy" && transform.parent != null)
        {
            //Debug.Log(collision.gameObject.tag);
            //gameObject.GetComponent<AudioSource>().clip = hitSound;

            //Debug.Log("Enemy Hit!"); 
            //GameManager.gmInstance.cameraShaker.ShakeCamera();
            if (pAttack.combo == 0)
            {
                

                collision.gameObject.GetComponent<EnemyScript>().contact = 0;
                //other.GetComponent<Renderer>().material.color = other.GetComponent<EnemyScript>().GetCurrentColor();
            }
            //Debug.Log("Collision");
            //if (this.GetComponentInParent<PlayerAttack>().isAttacking == true && other.GetComponent<EnemyScript>().contact != this.GetComponentInParent<PlayerAttack>().combo)
            if (pAttack.isAttacking == true)
            {
                Debug.Log("Hit!");
                
                collision.gameObject.GetComponent<EnemyScript>().TakeDamage(player.GetComponent<PlayerAttack>().attDamage);
                collision.gameObject.GetComponent<EnemyScript>().contact = player.GetComponent<PlayerAttack>().combo;
                EnemyScript e = collision.gameObject.GetComponent<EnemyScript>();
                e.StartFlasher(); 
            }
        }
    }

    

    
    void KnockBack(GameObject obj)
    {
        //-obj.transform.forward * 2;
    }
}
