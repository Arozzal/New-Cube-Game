using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public GameObject splitCube;
    public bool isChildCube = false;
    public Transform center;
    public float orbitDistance = 16.0f;
    public float orbitDegreesPerSec = 180.0f;
    private Camera camera;
    private float origTrans;
    private bool collidedRight = false;
    private bool collidedLeft = false;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


	private void LateUpdate()
	{
        camera.transform.position = new Vector3(camera.transform.position.x, transform.position.y + 5f, camera.transform.position.z);

        if (isChildCube == false)
        {

        }

        if (isChildCube == false && Input.GetKeyDown(KeyCode.R))
        {
            GameObject child = Instantiate(splitCube);
            child.GetComponent<CubeScript>().isChildCube = true;
            child.transform.localScale = new Vector3(0.5f, 1, 1);
            child.transform.position = transform.position;
            transform.localScale = new Vector3(0.5f, 1, 1);
        }

        if (isChildCube)
        {
            KeyUpdate(new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.RightShift });
        }
        else
        {
            KeyUpdate(new KeyCode[] { KeyCode.A, KeyCode.D, KeyCode.Space });
        }
    }
	
    
	void Update()
    {
        
        
       
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }


    public void KeyUpdate(KeyCode[] keys)
	{
        
        if (Input.GetKey(keys[0]) && transform.Find("left").GetComponent<CollisionLeft>().collision == false)
        {
            transform.parent.transform.Rotate(new Vector3(0, 0, 50 * Time.deltaTime));
        }

        if (Input.GetKey(keys[1]) && transform.Find("right").GetComponent<CollisionRight>().collision == false)
        {
            transform.parent.transform.Rotate(new Vector3(0, 0, -50 * Time.deltaTime));
        }

        if (Input.GetKeyDown(keys[2]) && IsGrounded())
		{
            GetComponent<Rigidbody>().AddForce(Vector3.up * 700, ForceMode.Acceleration);
		}
        
    }

    void Orbit(float move)
    {
        if (center != null)
        {
            transform.RotateAround(center.position, Vector3.up, move * Time.deltaTime);
        }
    }


}
