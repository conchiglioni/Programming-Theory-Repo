using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void CheckOutofBounds()
    {
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    protected virtual Mesh GenerateMesh()
    {
        return gameObject.GetComponent<Mesh>();
    }
}
