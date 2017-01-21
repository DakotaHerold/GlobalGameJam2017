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
    private string horizontalMovementAxis;
    private string verticalMovementAxis1;
    private string verticalMovementAxis2;

    public Vector3 GetCharacterVelocity()
    {
        return controller.velocity; 
    }


    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        //rigidBody = GetComponent<Rigidbody>();

        horizontalMovementAxis = "Horizontal" + playerNumber;
        verticalMovementAxis1 = "VerticalA" + playerNumber;
        verticalMovementAxis2 = "VerticalB" + playerNumber;
 	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, Input.GetAxis(horizontalMovementAxis) * rotationSpeed, 0);

        if (controller.isGrounded)
        {
            //moveDirection = new Vector3(Input.GetAxis(horizontalMovementAxis), 0, Input.GetAxis(verticalMovementAxis));
            moveDirection = Vector3.forward * (Input.GetAxis(verticalMovementAxis1) + Input.GetAxis(verticalMovementAxis2)); 
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= movementSpeed;

            
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
       
    }
}
