using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialNormalNote : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRig;
    [SerializeField]
    private float forcePower;

    private Transform t;



    public void Awake()
    {
        t = this.transform;
        print(t.position);

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(PlayerCol.inst.state == PlayerCol.STATE.NONE)
        Move();
    }

    void Move()
    {
        this.transform.position += Vector3.back * NoteManager.inst.speed * Time.deltaTime;
    }
    public void Punched() //플레이어의 펀치에 맞았을 경우 호출되는 함수
    {
        Vector3 forceVec = (Vector3.forward + (Vector3.up * 0.25f) + (Vector3.right * Random.Range(-0.2f, 0.2f))) * forcePower;
        myRig.AddForce(forceVec, ForceMode.Impulse);
    }

}
