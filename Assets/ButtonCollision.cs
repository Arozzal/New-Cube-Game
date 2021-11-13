using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollision : MonoBehaviour
{
    bool moveDown = false;
    float timer = 0;
    float timerDown = 0;
    public float timeButtonIsActivated = 15;
    public GameObject doorGameObject;
    public float doorUpMoveAmount = -2.0f;

    float originalY = 0;
    void Start()
    {
        originalY = doorGameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
		if (moveDown)
		{
            timerDown = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.25f, transform.localPosition.z);
            timer += Time.deltaTime;
            doorGameObject.transform.localPosition = new Vector3(doorGameObject.transform.localPosition.x, Mathf.Lerp(originalY, originalY + doorUpMoveAmount, timer), doorGameObject.transform.localPosition.z);

            if(timer > timeButtonIsActivated)
			{
                timer = 0;
                moveDown = false;
                transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            }

		}
		else
		{
            if(Mathf.Abs(doorGameObject.transform.localPosition.y - originalY) > 0.05)
			{
                timerDown += Time.deltaTime;
                doorGameObject.transform.localPosition = new Vector3(doorGameObject.transform.localPosition.x, Mathf.Lerp(originalY + doorUpMoveAmount, originalY, timerDown), doorGameObject.transform.localPosition.z);
            }
		}

        

    }


	private void OnCollisionEnter(Collision collision)
	{
        
        moveDown = true;
	}
}
