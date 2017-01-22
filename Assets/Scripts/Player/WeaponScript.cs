using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour 
{

    // Use this for initialization
    public GameObject player;
    public float spinSpeed;
    public AudioClip hitSound; 

	
	// Update is called once per frame
	void Update ()
    {
        // spin the weapon
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy" && transform.parent != null)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(hitSound, 1.0f);
            //Debug.Log("Enemy Hit!"); 
            //GameManager.gmInstance.cameraShaker.ShakeCamera();
            if (this.GetComponentInParent<PlayerAttack>().combo == 0)
            {
                
                Debug.Log("Playing sound");  
                collision.gameObject.GetComponent<EnemyScript>().contact = 0;
                //other.GetComponent<Renderer>().material.color = other.GetComponent<EnemyScript>().GetCurrentColor();
            }
            //Debug.Log("Collision");
            //if (this.GetComponentInParent<PlayerAttack>().isAttacking == true && other.GetComponent<EnemyScript>().contact != this.GetComponentInParent<PlayerAttack>().combo)
            if (this.GetComponentInParent<PlayerAttack>().isAttacking == true)
            {
                Debug.Log("Hit!");
                collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
                collision.gameObject.GetComponent<EnemyScript>().TakeDamage(player.GetComponent<PlayerAttack>().attDamage);
                collision.gameObject.GetComponent<EnemyScript>().contact = player.GetComponent<PlayerAttack>().combo;
            }
            else
            {
                collision.gameObject.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<EnemyScript>().GetCurrentColor();
            }
            EnemyScript e = collision.gameObject.GetComponent<EnemyScript>(); 
            Debug.Log("Enemy Health " +  e.health);
        }
    }

    
    void KnockBack(GameObject obj)
    {
        //-obj.transform.forward * 2;
    }
}
