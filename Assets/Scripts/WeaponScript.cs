using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Parent " + this.transform.parent);
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("Collision");
            if (this.GetComponentInParent<PlayerAttack>().isAttacking == true)
            {
                Debug.Log("Hit!");
                other.GetComponent<EnemyScript>().TakeDamage(transform.parent.GetComponent<PlayerAttack>().attDamage);
            }
        }
    }
}
