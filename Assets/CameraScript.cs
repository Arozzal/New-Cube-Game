using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;

    private Vector3 distanceAxis = new Vector3(0, 0, 10);
    private Vector3 oldDistanceAxis;
    private Vector3 newDistanceAxis;

    private Quaternion distanceRotation = Quaternion.Euler(0, 180, 0);
    private Quaternion oldDistanceRotation;
    private Quaternion newDistanceRotation;

    private Vector3 oldPlPos;
    private Vector3 newPlPos;

    bool AxisInterpolation { get; set;} = false;
    float steps = 0;

    void Start()
    {
        
    }

    public void SetNewAxis(Vector3 axis, Vector3 playerAxis, Quaternion rotation, Vector3 newPlPos, bool topDown)
	{
        if (AxisInterpolation) return;

        Debug.Log("new axis");

        AxisInterpolation = true;
        oldDistanceAxis = distanceAxis;
        newDistanceAxis = axis;

        oldDistanceRotation = distanceRotation;
        newDistanceRotation = rotation;

        this.oldPlPos = GameObject.Find("Player").transform.position;
        this.newPlPos = newPlPos;

        GameObject.Find("Player").GetComponent<PlayerScript>().SetMoveVector(playerAxis, topDown);

        steps = 0;
	}

    // Update is called once per frame
    void Update()
    {
		if (AxisInterpolation)
		{
            steps += Time.deltaTime;
            distanceAxis = Vector3.Lerp(oldDistanceAxis, newDistanceAxis, steps);
            distanceRotation = Quaternion.Lerp(oldDistanceRotation, newDistanceRotation, steps);
            GameObject.Find("Player").transform.position = Vector3.Lerp(oldPlPos, newPlPos, steps);
            if (steps > 1)
			{
                AxisInterpolation = false;
                steps = 0;
			}

		}

        transform.rotation = distanceRotation;
        transform.position = player.transform.position + distanceAxis + new Vector3(0, 0, 0);
    }
}
