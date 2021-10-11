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
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 targetPosition = transform.position + new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, targetPosition, speed);
        transform.position = lerpPosition;
    }
}
