﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    PlayerScript psScript;
    PlayerAttack paScript;
    CharacterController3D ccScript;


    private Animator anim;
    private int playerNumber;
    private string throwButton;
    private string pingButton;
    private string swingButton;
    private string specialButton;

    private AudioSource audio; 
    // Use this for initialization
    void Start () {
        psScript = GetComponent<PlayerScript>();
        paScript = GetComponent<PlayerAttack>();
        ccScript = GetComponent<CharacterController3D>();

        playerNumber = ccScript.playerNumber;
        throwButton = "Throw" + playerNumber;
        pingButton = "Ping" + playerNumber;
        swingButton = "Swing" + playerNumber;
        specialButton = "Special" + playerNumber;

        anim = GetComponent<Animator>();

        audio = GetComponent<AudioSource>(); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Swing1"))
        {
            //Debug.Log("test");
            //GameManager.gmInstance.cameraShaker.ShakeCamera(0.0001f, 0.01f);
        }


        //Player Script
        CheckIfPing();
        ThrowStick();
        //Player Attack Script
        if (psScript.hasWeapon)
        {          
            HammerAttack();
        }
        else
        {
            // Player doesn't have hammer
            // Special Check 
            if (Input.GetButtonDown(specialButton) && paScript.specialCD <= 0.0f)
            {
                paScript.specialActive = true;
                paScript.specialCD = 3.0f;
                paScript.Special();
            }
            if (paScript.specialCD <= 0.0f)
            {
                paScript.specialCD = 0.0f;
                paScript.specialActive = false;
            }
            else
            {
                paScript.specialCD -= 0.01f;
            }
        }

    }

    //From the Player Script 
    public void ThrowStick()
    {
        if (psScript.hasWeapon == true)
        {
            if (Input.GetButtonDown(throwButton))
            {
                psScript.stickHold.transform.GetChild(0).gameObject.SetActive(false);
                anim.SetBool("HasWeapon", false);
                GameObject stickClone = Instantiate(psScript.StickPrefab, psScript.stickSpawn.transform.position, psScript.stickSpawn.transform.rotation) as GameObject;
                Rigidbody stickPhysics = stickClone.GetComponent<Rigidbody>(); // You should be able to hold to throw?

                Vector3 targetVelocity = (transform.forward + transform.up).normalized * psScript.throwingSpeed;
                stickPhysics.velocity = targetVelocity;
                psScript.hasWeapon = false;
            }
        }
        else
        {
            psScript.stickHold.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void CheckIfPing()
    {
        if ((Input.GetButtonDown(pingButton)) && psScript.hasWeapon == false)
        {
            //Debug.Log(playerNumber + " is pinging"); 
        }
    }

    //From the Player Attack Script
    IEnumerator ResetCombo()
    {
        yield return new WaitForSeconds(1.0f);
        paScript.combo = 0;
        paScript.comboEnd = false;
    }
    void HammerAttack()
    {
        if ((Input.GetButtonDown(swingButton)) && paScript.comboEnd == false)
        {
            // TO-DO find out how to call this at the end of animation 
            //GameManager.gmInstance.cameraShaker.ShakeCamera();
            //Debug.Log("attack!");
            paScript.combo += 1;
            paScript.attackTimer = paScript.attackDelay;
            paScript.isAttacking = true;
            audio.PlayDelayed(0.75f);
            anim.SetBool("IsAttacking", true);
            if (paScript.combo >= 1)
            {
                paScript.comboEnd = true;
                StartCoroutine(ResetCombo());
                
            }
        }

        if(paScript.attackTimer <= 0.0f)
        {
            paScript.attackTimer = 0.0f;
            paScript.combo = 0;
            paScript.isAttacking = false;
            anim.SetBool("IsAttacking", false);
            // TO-DO Add camera shake here 
            
        }
        else
        {
            paScript.attackTimer -= 0.01f;
        }
    }
}
