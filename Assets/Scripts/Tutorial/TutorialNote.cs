using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{
    public void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (PlayerCol.inst.state == PlayerCol.STATE.NONE)
            Move();
    }

    void Move()
    {
        this.transform.position += Vector3.back * NoteManager.inst.speed * Time.deltaTime;
    }

}
