using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Vector3 offset;
    private float offsetMagnitude;
    private Camera myMainCamera;
    Rigidbody rb;
    private float dragSpeed = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        myMainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -0.5 && transform.position.x <= 25 && transform.position.x >= -25 && transform.position.z >= - 25 && transform.position.z <= 25)
        {
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.Log(camRay);
        RaycastHit hit;
        if(Physics.Raycast(camRay, out hit, 100) && hit.transform.tag == "Shape")
        {
            offset = transform.position - myMainCamera.transform.position;
            offsetMagnitude = offset.magnitude;
        }
    }
    
    private void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseDirection = camRay.direction;
        // Project mouseDirection onto the different axes
        float directionX = Vector3.Dot(mouseDirection, new Vector3(1,0,0));
        float directionY = Vector3.Dot(mouseDirection, new Vector3(0,1,0));
        float directionZ = Vector3.Dot(mouseDirection, new Vector3(0,0,1));
        Vector3 target = new Vector3(directionX, directionY, directionZ) * offsetMagnitude;
        // Move GameObject in the direction of the cursor at the inital grab radius
        Vector3 movementVector = myMainCamera.transform.position + target - transform.position;
        rb.velocity = movementVector * dragSpeed;
    }
}
