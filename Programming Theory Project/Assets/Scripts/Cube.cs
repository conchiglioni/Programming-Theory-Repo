using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Shape
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void GenerateObject()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(0, 0, 1);
        Vector3 p2 = new Vector3(1, 0, 1);
        Vector3 p3 = new Vector3(1, 0, 0);
        Vector3 p4 = new Vector3(0, 1, 0);
        Vector3 p5 = new Vector3(0, 1, 1);
        Vector3 p6 = new Vector3(1, 1, 1);
        Vector3 p7 = new Vector3(1, 1, 0);

        vertices = new Vector3[]
        {
            p0,p2,p1,
            p0,p3,p2,
            p0,p4,p7,
            p0,p7,p3,
            p0,p1,p5,
            p0,p5,p4,
            p3,p6,p2,
            p3,p7,p6,
            p1,p6,p5,
            p1,p2,p6,
            p4,p5,p6,
            p4,p6,p7
        };

        triangles = new int[]
        {
            0,1,2,
            3,4,5,
            6,7,8,
            9,10,11,
            12,13,14,
            15,16,17,
            18,19,20,
            21,22,23,
            24,25,26,
            27,28,29,
            30,31,32,
            33,34,35
        };
    }

    void UpdateMesh()
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
