using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody collider;
    private Vector3 moveVector = new Vector3(10, 0, 0);
    private bool topDown = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Rigidbody>();
    }

    public void SetMoveVector(Vector3 moveVector, bool topDown   )
	{
        this.moveVector = moveVector;
        this.topDown = topDown;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (Input.GetKey(KeyCode.A))
		{
            collider.MovePosition(transform.position + moveVector * Time.deltaTime);
		}
        if (Input.GetKey(KeyCode.D))
        {
            collider.MovePosition(transform.position - moveVector * Time.deltaTime);
        }
		if (topDown)
		{
            if (Input.GetKey(KeyCode.W))
            {
                collider.MovePosition(transform.position + Quaternion.AngleAxis(90, Vector3.up) * moveVector * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                collider.MovePosition(transform.position - Quaternion.AngleAxis(90, Vector3.up) * moveVector * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.Space))
        {
            collider.AddForce(0, 100, 0);
        }
    }
}
