using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[SerializeField]
	float speed, minDistance, maxDistance, speedScale;

    [SerializeField]
    GameObject baseModule;

    [SerializeField]
    bool locked;

    Vector3 lastMousePos = new Vector3(-1, -1, -1);

    Vector3 focus;
    
    // Use this for initialization
    void Start () {
        focus = baseModule.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        //TODO: Adjust Speed

        //Both CameraMovements should be adjusted
        if (locked)
        {
            lockedMovement();
        } else
        {
            rightClickMovement();
        }
    }

    /// <summary>
    /// A/D: Rotate the Camera around the Base
    /// W/S: Move towards the Base
    /// TODO: Adjust speed; Adjust max/min Distance
    /// </summary>
    void lockedMovement()
    {
        float amtToMoveForward = Input.GetAxis("Vertical");
        float amtToMoveRight = Input.GetAxis("Horizontal");
        Vector3 moveDir = Vector3.zero;

        //Move towards the Base
        //Vector3 forwardComponent = new Vector3(0, 0, transform.forward.z);
        Vector3 forwardComponent = transform.worldToLocalMatrix.MultiplyVector(transform.forward);


        //Look at Base
        transform.LookAt(baseModule.transform, Vector3.up);

        moveDir = (forwardComponent * amtToMoveForward * speed * Time.deltaTime);

        transform.Translate(moveDir);
        float dist = (baseModule.transform.position - transform.position).magnitude;

        if (dist >= maxDistance || dist <= minDistance) //needs to be optimized
        {
            transform.Translate(-moveDir); //Go back inside Boundaries
        }

        transform.RotateAround(baseModule.gameObject.transform.position, Vector3.down, amtToMoveRight);
    }

    /// <summary>
    /// Right Click + Drag: Move Camera
    /// A/D: Rotate the Camera; should probably be adjusted
    /// </summary>
    void rightClickMovement()
    {
        float rotateInput = Input.GetAxis("Horizontal");

        if (Input.GetMouseButton(1))
        {
            if (lastMousePos.Equals(new Vector3(-1, -1, -1)))
            {
                //First time pressed: Do nothing
                Debug.Log("Do nothing");
            }
            else
            {
                //Move Camera
                Vector3 mouseMove = Input.mousePosition - lastMousePos;

                Vector3 forward = transform.localToWorldMatrix * new Vector3(0, 0, 1) * -1;
                Vector3 sideways = transform.localToWorldMatrix * new Vector3(1, 0, 0) * -1;

                sideways.y = forward.y = 0;

                Vector3 movement = mouseMove.x * sideways * speed * Time.deltaTime + mouseMove.y * forward * speed * Time.deltaTime;

                transform.Translate(movement, Space.World);
                focus += movement;

            }
            lastMousePos = Input.mousePosition;
        }
        else
        {
            lastMousePos = new Vector3(-1, -1, -1);
        }

        transform.RotateAround(focus, Vector3.down, rotateInput);
    }
    
}