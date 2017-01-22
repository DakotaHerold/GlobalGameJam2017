using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public float gravity = 9.8f;
    public int playerNumber = 0; 

    //Rigidbody rigidBody;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private string verticalMovementAxis1;
    private string verticalMovementAxis2;
    private string triggerAxis;


    Animator anim;

    public Vector3 GetCharacterVelocity()
    {
        return controller.velocity; 
    }


    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        //rigidBody = GetComponent<Rigidbody>();
        verticalMovementAxis1 = "VerticalA" + playerNumber;
        verticalMovementAxis2 = "VerticalB" + playerNumber;
        triggerAxis = "Wave" + playerNumber;


        anim = GetComponent<Animator>();
 	}
	
	// Update is called once per frame
	void Update () {

        // Apply translation 
        if (controller.isGrounded)
        {
            if (Input.GetAxis(verticalMovementAxis1) != 0 || Input.GetAxis(verticalMovementAxis2) != 0)
            {
                transform.forward = new Vector3(-Input.GetAxis(verticalMovementAxis2), transform.forward.y, Input.GetAxis(verticalMovementAxis1));
                moveDirection = Vector3.forward;
            }
            else
            {
                moveDirection = Vector3.forward * 0;
            }
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;

            if(moveDirection == Vector3.zero)
            {
                anim.SetBool("IsMoving",false);
            }
            else
            {
                anim.SetBool("IsMoving", true);
            }   
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }

    public float GetTriggerAxisValue()
    {
        return Input.GetAxis(triggerAxis); 
    }
}
