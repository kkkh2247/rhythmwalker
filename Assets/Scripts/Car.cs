using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private BoxCollider col;

    public float sizeX;

    void Awake()
    {
        sizeX = col.size.x;
    }

}

