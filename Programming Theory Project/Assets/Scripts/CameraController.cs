using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get user inputs
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Convert inputs to vector and normalize
        Vector3 targetPosition = transform.position + new Vector3(horizontalInput, 0, verticalInput).normalized;
        // Allow the user to go up when pressing 'Space'
        if(Input.GetKey(KeyCode.Space))
        {
            targetPosition += Vector3.up;
        }
        // Allow the user to go down when pressing 'LShift'
        if(Input.GetKey(KeyCode.LeftShift))
        {
            targetPosition -= Vector3.up;
        }
        // Interpolate between current position and target position for smooth motion
        Vector3 lerpPosition = Vector3.Lerp(transform.position, targetPosition, speed);
        transform.position = lerpPosition;
    }
}
