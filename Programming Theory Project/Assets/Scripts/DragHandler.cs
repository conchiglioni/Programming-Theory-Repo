using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Vector3 offset;
    private float offsetMagnitude;
    private Camera myMainCamera;
    private GameObject cameraFollower;
    bool isUsingGravity;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        myMainCamera = Camera.main;
        cameraFollower = GameObject.Find("CameraFollower");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0) && !isUsingGravity)
        {
            rb.useGravity = true;
            isUsingGravity = true;
        }
        if(transform.position.y <= transform.localScale.y/2)
        {
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        rb.useGravity = false;
        isUsingGravity = false;
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(camRay, out hit, 100))
        {
            offset = transform.position - cameraFollower.transform.position;
            offsetMagnitude = offset.magnitude;
        }
    }
    
    private void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay, out hit, 100))
        {
            Vector3 fromVector = transform.position - cameraFollower.transform.position;
            Vector3 toVector = hit.point - cameraFollower.transform.position;
            float angle = 90-Vector3.Angle(fromVector, toVector);
            angle = angle * Mathf.Deg2Rad;
            // offsetMagnitude * Mathf.Cos(angle) - transform.position.z // z part of deltaVector, taken out for testing
            Vector3 deltaVector = new Vector3(0, offsetMagnitude * Mathf.Cos(angle) - transform.position.y, offsetMagnitude * Mathf.Sin(angle) - transform.position.z);
            Vector3 targetVector = cameraFollower.transform.position + offset + deltaVector;
            //Vector3 lerpVector = Vector3.Lerp(transform.position, targetVector, 0.5f);
            //transform.position = lerpVector;
            transform.position = targetVector;
            offset = transform.position - cameraFollower.transform.position;
        }

    }
}
