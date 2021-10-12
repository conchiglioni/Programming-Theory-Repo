using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public void CreateObject(int sides, float scale)
    {
        switch(sides)
        {
            case 4:
                gameObject.AddComponent<TriangularPyramid>();
                GetComponent<TriangularPyramid>().GenerateObject();
                break;
            case 5:
                gameObject.AddComponent<SquarePyramid>();
                GetComponent<SquarePyramid>().GenerateObject();
                break;
            default:
                gameObject.AddComponent<Cube>();
                GetComponent<Cube>().GenerateObject();
                break;
        }
        transform.localScale *= scale;
        gameObject.GetComponent<Rigidbody>().mass *= scale;
        Debug.Log("Object Created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
