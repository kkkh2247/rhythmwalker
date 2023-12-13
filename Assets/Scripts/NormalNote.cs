using UnityEngine;

public class NormalNote : Note
{
    [SerializeField]
    private Rigidbody myRig;
    [SerializeField]
    private float forcePower;

    [Header("선물상자에 적용할 머티리얼들")]
    [SerializeField]
    private Material[] myMat;

    public void Awake()
    {
        //this.transform.GetChild(0).GetComponent<Renderer>().material = myMat[Random.Range(0, myMat.Length)];
        if(type == 3)
            this.transform.Rotate(0, Random.Range(0,360), 0);

    }

    public void Punched() //플레이어의 펀치에 맞았을 경우 호출되는 함수
    {
        Vector3 forceVec = (Vector3.forward + (Vector3.up * 0.25f) + (Vector3.right * Random.Range(-0.2f, 0.2f))) * forcePower;
        myRig.AddForce(forceVec, ForceMode.Impulse);
        if(GameManager.inst.state == GameManager.STATE.PLAY)
        Invoke("ClearVelocity", 2.5f);
    }

    public void ClearVelocity()
    {

        myRig.velocity = Vector3.zero;

        this.transform.position = Vector3.zero;

        

        //this.transform.GetChild(0).GetComponent<Renderer>().material = myMat[Random.Range(0, myMat.Length)];
    }

}
