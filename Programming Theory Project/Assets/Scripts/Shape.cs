using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    // POLYMORPHISM
    public virtual void GenerateObject()
    {
        return;
    }

    // POLYMORPHISM
    protected virtual void CreateShape()
    {
        return;
    }

    // POLYMORPHISM
    protected virtual void UpdateMesh()
    {
        return;
    }
}
