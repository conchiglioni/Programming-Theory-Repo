using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SquarePyramid : Shape
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
    }

    // POLYMORPHISM
    protected override void CreateShape()
    {
        Vector3 centroidOffset = new Vector3(0.5f, 0.1767766953f, 0.5f);
        Vector3 p0 = new Vector3(0, 0, 0) - centroidOffset;
        Vector3 p1 = new Vector3(0, 0, 1) - centroidOffset;
        Vector3 p2 = new Vector3(1, 0, 1) - centroidOffset;
        Vector3 p3 = new Vector3(1,0,0) - centroidOffset;
        Vector3 p4 = new Vector3(0.5f, Mathf.Sqrt(0.75f), 0.5f) - centroidOffset;


        vertices = new Vector3[]
        {
            p0,p2,p1,
            p0,p3,p2,
            p0,p4,p3,
            p3,p4,p2,
            p2,p4,p1,
            p1,p4,p0
        };

        triangles = new int[]
        {
            0,1,2,
            3,4,5,
            6,7,8,
            9,10,11,
            12,13,14,
            15,16,17
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
