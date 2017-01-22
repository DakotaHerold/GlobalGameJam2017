using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour 
{

    // Use this for initialization
    public GameObject player;
    public float spinSpeed;

	
	// Update is called once per frame
	void Update ()
    {
        // spin the weapon
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
	}

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Parent " + this.transform.parent);
        if (other.gameObject.tag == "enemy" && transform.parent != null)
        {
            if (this.GetComponentInParent<PlayerAttack>().combo ==  0)
            {
                other.GetComponent<EnemyScript>().contact = 0;
                //other.GetComponent<Renderer>().material.color = other.GetComponent<EnemyScript>().GetCurrentColor();
            }
            //Debug.Log("Collision");
            if (this.GetComponentInParent<PlayerAttack>().isAttacking == true && other.GetComponent<EnemyScript>().contact != this.GetComponentInParent<PlayerAttack>().combo)
            {
                Debug.Log("Hit!");
                other.GetComponent<Renderer>().material.color = Color.green;
                other.GetComponent<EnemyScript>().TakeDamage(player.GetComponent<PlayerAttack>().attDamage);
                other.GetComponent<EnemyScript>().contact = player.GetComponent<PlayerAttack>().combo;
            }
            else
            {
                other.GetComponent<Renderer>().material.color = other.GetComponent<EnemyScript>().GetCurrentColor();
            }
        }
    }
    void KnockBack(GameObject obj)
    {
        //-obj.transform.forward * 2;
    }
}
