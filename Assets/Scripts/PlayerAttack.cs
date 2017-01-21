using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public int combo;
    public float specialCD;
    public float attackTimer;
    public float attackDelay;
    public float attDamage;
    public bool isAttacking;
    public bool comboEnd;
    public bool specialActive;
    bool isHit;


    private int playerNumber;
    private string swingButton;
    private string specialButton;
    private PlayerScript playerScript; 
    // Use this for initialization
    void Start () {
        isAttacking = false;
        isHit = false;
        comboEnd = false;
        specialActive = false;
        playerNumber = GetComponent<CharacterController3D>().playerNumber; 

        swingButton = "Swing" + playerNumber;
        specialButton = "Special" + playerNumber;

        playerScript = GetComponent<PlayerScript>(); 
    }
	
	// Update is called once per frame
	void Update () {
        if (playerScript.hasWeapon)
        {
            Attack();
        }
        else
        {
            // Player doesn't have hammer

            Special();    
        }
        
	}
    IEnumerator ResetCombo()
    {
        yield return new WaitForSeconds(1.0f);
        combo = 0;
        comboEnd = false;
    }
    void Attack()
    {
        if ((Input.GetButtonDown(swingButton)) && comboEnd == false)
        {
            Debug.Log("attack!");
            combo += 1;
            attackTimer = attackDelay;
            isAttacking = true;

            if (combo >= 3)
            {
                comboEnd = true;
                StartCoroutine(ResetCombo());
            }
        }

        if(attackTimer <= 0.0f)
        {
            attackTimer = 0.0f;
            combo = 0;
            isAttacking = false;
        }
        else
        {
            attackTimer -= 0.01f;
        }
    }
    void Special()
    {
        // Special Check 
        if (Input.GetButtonDown(specialButton) && specialCD <= 0.0f)
        {
            specialActive = true;
            specialCD = 3.0f;
            Debug.Log(playerNumber + " Special Logic!");
        }
        if (specialCD <= 0.0f)
        {
            specialCD = 0.0f;
            specialActive = false;
        }
        else
        {
            specialCD -= 0.01f;
        }
    }

}
