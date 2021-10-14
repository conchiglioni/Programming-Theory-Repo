using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class TriangularPyramid : Shape
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    // POLYMORPHISM
    public override void GenerateObject()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // ABSTRACTION
        CreateShape();
        UpdateMesh();

        Vector3 rotationAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationAngles + Vector3.up * -30);
    }

    // POLYMORPHISM
    protected override void CreateShape()
    {
        Vector3 centroidOffset = new Vector3(0.2886751346f, 0.2165063509f, 0.5f);
        Vector3 p0 = new Vector3(0, 0, 0) - centroidOffset;
        Vector3 p1 = new Vector3(0, 0, 1) - centroidOffset;
        Vector3 p2 = new Vector3(Mathf.Sqrt(0.75f), 0, 0.5f) - centroidOffset;
        Vector3 p3 = new Vector3(Mathf.Sqrt(0.75f) / 3, Mathf.Sqrt(0.75f), 0.5f) - centroidOffset;


        vertices = new Vector3[]
        {
            p0,p2,p1,
            p0,p3,p2,
            p2,p3,p1,
            p1,p3,p0
        };

        triangles = new int[]
        {
            0,1,2,
            3,4,5,
            6,7,8,
            9,10,11
        };
    }

    // POLYMORPHISM
    protected override void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
