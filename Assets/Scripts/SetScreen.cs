using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreen : MonoBehaviour
{
    [SerializeField]
    private bool IsActive;
    void Start()
    {
        if (IsActive)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
