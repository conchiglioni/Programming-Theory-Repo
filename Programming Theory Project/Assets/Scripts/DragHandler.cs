using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private Vector3 offset;
    private float offsetMagnitude;
    private Camera myMainCamera;
    Rigidbody rb;
    LineRenderer lineRenderer;
    private float dragSpeed = 7.0f;
    private Color lineColor;
    private bool isHolding = false;
    private bool lineVisibilitySwitch = false;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        myMainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;
        lineColor = Color.white;
        lineColor.a = 0.0f;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0) && isHolding)
        {
            isHolding = false;
            lineColor.a = 0.0f;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
        }

        // Keep object in bounds
        if(transform.position.y <= -0.5 && transform.position.x <= 25 && transform.position.x >= -25 && transform.position.z >= - 25 && transform.position.z <= 25)
        {
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        if(!isHolding)
        {
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, 100) && hit.transform.tag == "Shape")
            {
                offset = transform.position - myMainCamera.transform.position;
                offsetMagnitude = offset.magnitude;
                isHolding = true;
                Debug.Log(rb.centerOfMass);
                lineVisibilitySwitch = true;
            }
        }
    }
    
    private void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 target = camRay.direction * offsetMagnitude;
        // Move GameObject in the direction of the cursor at the inital grab radius
        Vector3 movementVector = myMainCamera.transform.position + target - transform.position;
        rb.velocity = movementVector * dragSpeed;
        var points = new Vector3[2];
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100);
        points[0] = transform.position;
        points[1] = hit.point;
        if(lineVisibilitySwitch)
        {
            lineColor.a = 1.0f;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineVisibilitySwitch = false;
        }
        lineRenderer.SetPositions(points);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, 2);
    }
}
