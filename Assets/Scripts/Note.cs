using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float time; //해당 노트를 쳐야 할 시간
    public float type;
    public GameObject myObj;

    public bool isCollision; //플레이어와 충돌했는지 체크

    public void Awake()
    {
        myObj = this.gameObject;
        isCollision = false;
    }

    public void Update()
    {
        if(PlayerCol.inst.state == PlayerCol.STATE.NONE)
        this.transform.position += Vector3.back * NoteManager.inst.speed * Time.deltaTime;
    }
}
