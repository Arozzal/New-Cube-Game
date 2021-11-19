using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody collider;
    private Vector3 moveVector = new Vector3(10, 0, 0);
    private bool topDown = false;
    // Start is called before the first frame update
    private bool isGrounded = false;
    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    private float fallingSpeed = 0;
    private float playerSpeed = 0.5f;
    private float jumpHeight = 3.0f;
    private float gravityValue = -8.81f;

    void Start()
    {
        collider = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    public void SetMoveVector(Vector3 moveVector, bool topDown   )
	{
        this.moveVector = moveVector;
        this.topDown = topDown;

    }

	private void OnCollisionEnter(Collision collision)
	{
        collider.velocity = new Vector3(0, fallingSpeed, 0);
	}

	void OnCollisionStay(Collision theCollision)
    {
        if(Physics.Raycast(transform.position + new Vector3(0.24f, 0, 0.24f), -Vector3.up, 0.25001f))
        {
            isGrounded = true;
            Debug.Log("is 1");
		}
        else if (Physics.Raycast(transform.position + new Vector3(-0.24f, 0, 0.24f), -Vector3.up, 0.25001f))
        {
            isGrounded = true;
            Debug.Log("is 2");
        }
        else if(Physics.Raycast(transform.position + new Vector3(0.24f, 0, -0.24f), -Vector3.up, 0.25001f))
        {
            isGrounded = true;
            Debug.Log("is 3");
        }
        else if(Physics.Raycast(transform.position + new Vector3(-0.24f, 0, -0.24f), -Vector3.up, 0.25001f))
        {
            isGrounded = true;
            Debug.Log("is 4");
        }
		else
		{
            isGrounded = false;
            collider.velocity = playerVelocity + new Vector3(0, fallingSpeed, 0);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = Vector3.zero;
        if (isGrounded)
        {
            fallingSpeed = 0f;
        }

        fallingSpeed += gravityValue * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
		{
            playerVelocity = moveVector * playerSpeed;
		}
        if (Input.GetKey(KeyCode.D))
        {
            playerVelocity = -moveVector * playerSpeed;
        }

		if (topDown)
		{
            if (Input.GetKey(KeyCode.W))
            {
                controller.Move(Quaternion.AngleAxis(90, Vector3.up) * moveVector * Time.deltaTime * playerSpeed);
                gameObject.transform.forward = moveVector;
            }
            if (Input.GetKey(KeyCode.S))
            {
                controller.Move(Quaternion.AngleAxis(90, Vector3.up) * -moveVector * Time.deltaTime * playerSpeed);
                gameObject.transform.forward = -moveVector;
            }
		}
		else
		{
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                fallingSpeed += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            }
        }




        fallingSpeed += gravityValue * Time.deltaTime;


        playerVelocity = Vector3.Min(playerVelocity, new Vector3(50, 50, 50));
        playerVelocity = Vector3.Max(playerVelocity, new Vector3(-50, -50, -50));

        collider.velocity = playerVelocity + new Vector3(0, fallingSpeed, 0);
        isGrounded = false;
    }
}
