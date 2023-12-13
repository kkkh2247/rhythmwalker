using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "huddle")
        {
            if(other.GetComponent<Note>().isCollision == false)
            {
                ScoreManager.inst.HitNote(true);
            }
        }
    }
}
