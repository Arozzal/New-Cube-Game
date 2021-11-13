using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    CameraScript cameraScript;
    public GameObject connectedCollider;
    public bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        cameraScript = GameObject.Find("Camera").GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        Quaternion rotation;
        bool topDown;

        if (connectedCollider.transform.rotation.eulerAngles.x - transform.rotation.eulerAngles.x == 270)
		{
            topDown = true;
            rotation = Quaternion.Euler(connectedCollider.transform.rotation.eulerAngles + new Vector3(180, 180, 0));

        }
		else
		{
            topDown = false;
            rotation = Quaternion.Euler(connectedCollider.transform.rotation.eulerAngles + new Vector3(0, 180, 0));
        }

        

        cameraScript.SetNewAxis(connectedCollider.transform.forward * 10, connectedCollider.transform.right * 10, rotation, connectedCollider.transform.position, topDown);

	}
}
