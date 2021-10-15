using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private float offsetMagnitude;
    private Camera myMainCamera;
    Rigidbody rb;
    LineRenderer lineRenderer;
    private float dragSpeed = 7.0f;
    private Color lineColor;
    private bool isHolding = false;
    private bool lineVisibilitySwitch = false;
    private bool isRotatable = false;
    Quaternion originalRotation;
    Quaternion tempRotation;

    // Start is called before the first frame update
    void Start()
    {
        // ABSTRACTION
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // When the user drops the object
        if(!Input.GetMouseButton(0) && isHolding)
        {
            isHolding = false;
            isRotatable = false;
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
        // ABSTRACTION
        HandleObjectClick();
    }
    
    private void OnMouseDrag()
    {
        // ABSTRACTION
        HandleObjectDrag();
    }

    void Initialize()
    {
        // Get scene camera
        myMainCamera = Camera.main;
        // Get Rigidbody
        rb = GetComponent<Rigidbody>();
        // Get rotation of object upon spawning in
        originalRotation = transform.rotation;
        // Add line renderer component and initialize
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 2;
        lineColor = Color.white;
        lineColor.a = 0.0f; // Make it invisible for now
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void UpdateLineRenderer(Vector3[] points)
    {
        if (lineVisibilitySwitch)
        {
            lineColor.a = 1.0f; // line is visible while being held
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
            lineVisibilitySwitch = false; // we only want to change line visibility once per unique click
        }
        lineRenderer.SetPositions(points);
    }

    void HandleObjectClick()
    {
        // If we are not currently holding something
        if (!isHolding)
        {
            // Raycast to where the mouse clicked
            Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // If it hits a "Shape"/Object
            if (Physics.Raycast(camRay, out hit, 100) && hit.transform.tag == "Shape")
            {
                // Get current radius from camera
                offsetMagnitude = (transform.position - myMainCamera.transform.position).magnitude;
                // Hold a copy of the original rotation, so that the user can rotate an object while holding
                tempRotation = originalRotation;
                // Set flags
                isHolding = true;
                lineVisibilitySwitch = true;
            }
        }
    }

    void HandleObjectDrag()
    {
        // If the object has returned to spawn rotation, the user should now be able to rotate it in the air
        if(transform.rotation == originalRotation && !isRotatable)
        {
            Debug.Log("Back to spawn rotation");
            rb.angularVelocity = Vector3.zero;
            isRotatable = true;
        }
        if(isRotatable)
        {
            bool keyPressed = false;
            // Allow the user to rotate the object along the world x-axis by pressing 'E'
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.right * 2, Space.World);
                keyPressed = true;
            }
            // Allow the user to rotate the object along the world y-axis by pressing 'Q'
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up * 2, Space.World);
                keyPressed = true;
            }
            if(keyPressed)
            {
                tempRotation = Quaternion.RotateTowards(tempRotation, transform.rotation, 30);
            }
        }

        // Correct the object's rotation on pickup
        transform.rotation = Quaternion.RotateTowards(transform.rotation, tempRotation, 4);
        // Raycast from the camera to the mouse
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);
        // Convert to normalized vector and multiply by initial grab radius
        Vector3 target = camRay.direction * offsetMagnitude;
        // Move GameObject in the direction of the cursor at the inital grab radius
        Vector3 movementVector = myMainCamera.transform.position + target - transform.position;
        rb.velocity = movementVector * dragSpeed;
        // Set the line renderer down from the object to indicate where it will land
        var points = new Vector3[2];
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100);
        points[0] = transform.position;
        points[1] = hit.point;
        UpdateLineRenderer(points);
    }
}
