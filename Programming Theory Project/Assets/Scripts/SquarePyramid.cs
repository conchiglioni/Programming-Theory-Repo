using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePyramid : Shape
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
        Vector3 p3 = new Vector3(1,0,0);
        Vector3 p4 = new Vector3(0.5f, Mathf.Sqrt(0.75f), 0.5f);


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
