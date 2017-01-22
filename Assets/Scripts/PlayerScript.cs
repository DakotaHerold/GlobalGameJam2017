using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    public Transform stickSpawn;
    public GameObject StickPrefab;
    public GameObject stickHold;

    public float health;
    public float healthGen;
    public float regenTimer;
    public float speed;
    public float throwingSpeed;
    public float immunity;
    public int tears;
    public bool regainHealth;
    public bool isDead;
    public bool hasWeapon;
    public bool isPinging;

    Animator anim;

    private int playerNumber;
    private string throwButton;
    private string pingButton;
    void Start ()
    {
        stickSpawn = transform.GetChild(0);

        regainHealth = false;
        isDead = false;
        hasWeapon = false;
        isPinging = false;
        playerNumber = GetComponent<CharacterController3D>().playerNumber;

        anim = GetComponent<Animator>();

        playerNumber = 0;
        throwButton = "Throw" + playerNumber;
        pingButton = "Ping" + playerNumber;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Death();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(50);
        }
        HealthReGen();
        CheckIfPing();
        ThrowStick();
        if(immunity > 0.0f)
        {
            immunity -= 0.01f;
        }

	}
    public void TakeDamage(float damg)
    {
        if (immunity <= 0.0f)
        {
            health -= damg;
            immunity = 0.5f;
        }
        
    }
    IEnumerator healthRegen()
    {
        yield return new WaitForSeconds(regenTimer);
        regainHealth = false;
        if (hasWeapon == false && health < 100)
        {
            health += healthGen;
        }
    }
    void HealthReGen()
    {
        if(regainHealth == false)
        {
            regainHealth = true;
            StartCoroutine(healthRegen());
        }
    }
    void CheckIfPing()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown(pingButton)) && hasWeapon == false)
        {
            Debug.Log("Is pinging");
        }
    }
    void Death()
    {
        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Is Deads");
        }
        if(isDead == true)
        {
            healthGen = 0;
        }
    }
    void ThrowStick()
    {
        if(hasWeapon == true)
        {
            if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown(throwButton))
            {
                stickHold.transform.GetChild(0).gameObject.SetActive(false);
                anim.SetBool("HasWeapon", false);
                GameObject stickClone = Instantiate(StickPrefab, stickSpawn.transform.position, stickSpawn.transform.rotation) as GameObject;
                Rigidbody stickPhysics = stickClone.GetComponent<Rigidbody>(); // You should be able to hold to throw?

                Vector3 targetVelocity = (transform.forward + transform.up).normalized * throwingSpeed;
                stickPhysics.velocity = targetVelocity;
                hasWeapon = false;
            }
        }
        else
        {
            stickHold.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "enemy")
        {
            TakeDamage(other.GetComponent<EnemyScript>().attDamage);
        }*/
       
        if (other.gameObject.tag == "tear")
        {
            tears += 1;
        }
        if(hasWeapon == false)
        {
            if (other.gameObject.tag == "stick")
            {
                //Debug.Log("test");
                hasWeapon = true;
                anim.SetBool("HasWeapon", true);
                stickHold.transform.GetChild(0).GetComponent<WeaponScript>().player = this.gameObject;

                stickHold.transform.GetChild(0).gameObject.SetActive(true);
                Destroy(other.gameObject);
            }
        }
    }
}
