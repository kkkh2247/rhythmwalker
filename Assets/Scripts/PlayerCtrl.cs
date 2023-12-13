using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;

    [SerializeField]
    private float JumpPower;        //점프 높이를 결정하는 점프할때 힘 변수
    [SerializeField]
    private float JumpSpeed;        //점프 하고 내려올때까지의 속도

    [SerializeField]
    private float HighJumpPower;    //하이점프 위와 동일
    [SerializeField]
    private float HighJumpSpeed;

    [SerializeField]
    private float PunchDistance;    //펀치 거리
    [SerializeField]
    private LayerMask PunchLayer;   //펀치가 적중할 레이어

    [SerializeField]
    private float dodgeSpeed;
    [SerializeField]
    private float dodgeDistance;

    [SerializeField]
    private float SlidingSpeed;     //슬라이딩 스피드

    [SerializeField]
    private Animator myAnim;        //플레이어 애니메이터
    [SerializeField]
    private BoxCollider myCol;      //플레이어 박스콜리더

    private bool inMotion;          //현재 모션 진행중에 있는지 체크하는 변수
    public bool EndHitMotion;

    [SerializeField]
    private float WaitHitsoundTime;

    public Vector3 firstPos;
    public GameObject m_EffectPrefab;
    public GameObject m_EffectHitPrefab;

    void Awake()
    {
        inMotion = false;
        firstPos = this.transform.position;
        instance = this;
        EndHitMotion = true;
    }

    void imsi()
    {
        Ray ray = new Ray(this.transform.position, Vector3.forward);    //플레이어 위치에서 정면을 향하는 레이 선언

        RaycastHit hit;                                                 //레이캐스트 정보 받아올 레이캐스트힛 클래스 선언

        Vector3 hitposition = gameObject.transform.position + gameObject.transform.forward * PunchDistance;


        if (Physics.Raycast(ray, out hit, PunchDistance, PunchLayer))   //펀치에 장애물이 맞았을경우
        {
            myAnim.SetTrigger("Punch");
            ScoreManager.inst.HitNote(hit.distance < (PunchDistance / 2));
            //hit.collider.enabled = false;
            hit.collider.gameObject.GetComponent<NormalNote>().Punched();      //해당 장애물에서 펀치당했을때의 함수를 호출
            hitposition = hit.point;
            GameObject BBBBB = Instantiate(m_EffectPrefab, hitposition, Quaternion.LookRotation(hit.normal));


            Destroy(BBBBB, 0.5f);
            SoundManager.inst.punchsound.PlayOneShot(SoundManager.inst.punchsound.clip);
        }
    }

    void Update()
    {
       

        if (PlayerCol.inst.state == PlayerCol.STATE.NONE)
        {

            if (Input.GetKeyDown(KeyCode.DownArrow) && !inMotion)
            {
                if (EndHitMotion == true)
                {
                    Debug.Log("down");
                    StartCoroutine(Sliding());
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && !inMotion)
            {
                if (EndHitMotion == true)
                {
                    StartCoroutine(HighJump());
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && !inMotion)
            {
                if (EndHitMotion == true)
                {
                    StartCoroutine(LeftDodge());
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !inMotion)
            {
                if (EndHitMotion == true)
                {
                    StartCoroutine(RightDodge());
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {

                Punch();

            }
        }

        if (GameManager.inst.cheatmodeauto == true)
        {
            imsi();
        }

        if (GameManager.inst.cheatmodegod == true)
        {
            myCol.enabled = false;
        }

        if (GameManager.inst.cheatmodegod == false)
        {
            myCol.enabled = true;
        }

#if UNITY_ANDROID

        if (PlayerCol.inst.state == PlayerCol.STATE.NONE) 
        { 

            if (SwipeManager.swipeDirection == Swipe.Left && !inMotion && PlayerCol.inst.state == PlayerCol.STATE.NONE)
            {
                if (EndHitMotion == true)
                {

                StartCoroutine(LeftDodge());

                 }
            }
            else if (SwipeManager.swipeDirection == Swipe.Right && !inMotion && PlayerCol.inst.state == PlayerCol.STATE.NONE)
            {
              if (EndHitMotion == true)
                {

                StartCoroutine(RightDodge());
                }

            }
            else if (SwipeManager.swipeDirection == Swipe.Up && !inMotion && PlayerCol.inst.state == PlayerCol.STATE.NONE)
            {
              if (EndHitMotion == true)
               {

                StartCoroutine(HighJump());

                }
            }
            else if (SwipeManager.swipeDirection == Swipe.Down && !inMotion && PlayerCol.inst.state == PlayerCol.STATE.NONE)
            {
                  if (EndHitMotion == true)
                    {

                StartCoroutine(Sliding());
                    }

            }
            else if (SwipeManager.swipeDirection == Swipe.Punch&& PlayerCol.inst.state == PlayerCol.STATE.NONE)
            {
                 
                    Punch();
                
            }
        } 
#endif

    }

    IEnumerator Jump()      //일반 점프 (그냥 터치했을때)
    {
        inMotion = true;
        myAnim.SetBool("Jump", true);

        for (float i = 0; i < 1; i += Time.deltaTime * JumpSpeed)
        {
            float x = (i - 0.5f) * JumpPower;
            float y = x * x * -1;
            float posY = y + ((0.5f * JumpPower) * (0.5f * JumpPower));

            this.transform.position = new Vector3(transform.position.x, posY, transform.position.z);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        myAnim.SetBool("Jump", false);
        inMotion = false;
        SwipeManager.swipeDirection = Swipe.None;
    }

    IEnumerator Sliding()   //슬라이딩 (아래로 드래그)
    {

        inMotion = true;
        myAnim.SetTrigger("Sliding");
        myCol.center = new Vector3(myCol.center.x, myCol.center.y - 1f, myCol.center.z);


        yield return new WaitForSeconds(SlidingSpeed);


        myCol.center = new Vector3(myCol.center.x, myCol.center.y + 1f, myCol.center.z);
        inMotion = false;
        SwipeManager.swipeDirection = Swipe.None;
    }

    IEnumerator HighJump()  //하이점프 (위로 드래그)
    {
        inMotion = true;
        myAnim.SetBool("HighJump", true);

        for (float i = 0; i < 1; i += Time.deltaTime * HighJumpSpeed)
        {
            float x = (i - 0.5f) * HighJumpPower;
            float y = x * x * -1;
            float posY = y + ((0.5f * HighJumpPower) * (0.5f * HighJumpPower));

            this.transform.position = new Vector3(transform.position.x, posY + firstPos.y, transform.position.z);

            yield return new WaitForSeconds(Time.deltaTime / (HighJumpSpeed * 2));
        }
        this.transform.position = firstPos;
        myAnim.SetBool("HighJump", false);
        inMotion = false;
        SwipeManager.swipeDirection = Swipe.None;
    }

    IEnumerator LeftDodge() //왼쪽 회피 (왼쪽으로 드래그)
    {
        inMotion = true;
        for (float i = 0; i < 1; i += Time.deltaTime * dodgeSpeed)
        {
            float x = (i - 0.5f) * dodgeDistance;
            float y = x * x * -1;
            float posX = y + ((0.5f * dodgeDistance) * (0.5f * dodgeDistance));

            this.transform.position = new Vector3(firstPos.x - posX, transform.position.y, transform.position.z);

            yield return new WaitForSeconds(Time.deltaTime / (dodgeSpeed * 2));
        }
        this.transform.position = firstPos;

        inMotion = false;
        SwipeManager.swipeDirection = Swipe.None;
    }

    IEnumerator RightDodge()    //오른쪽 회피 (오른쪽으로 드래그)
    {
        inMotion = true;
        for (float i = 0; i < 1; i += Time.deltaTime * dodgeSpeed)
        {
            float x = (i - 0.5f) * dodgeDistance;
            float y = x * x * -1;
            float posX = y + ((0.5f * dodgeDistance) * (0.5f * dodgeDistance));

            this.transform.position = new Vector3(posX + firstPos.x, transform.position.y, transform.position.z);

            yield return new WaitForSeconds(Time.deltaTime / (dodgeSpeed * 2));
        }
        this.transform.position = firstPos;

        inMotion = false;
        SwipeManager.swipeDirection = Swipe.None;
    }

    public IEnumerator Gethit()
    {
        Invoke("WaitHitSound", WaitHitsoundTime);
        GameObject BBBBB = Instantiate(m_EffectHitPrefab, this.transform.position, Quaternion.identity);
        Destroy(BBBBB, 0.5f);
        SoundManager.inst.gethit.PlayOneShot(SoundManager.inst.gethit.clip);
        myAnim.SetTrigger("Hit");
        SoundManager.inst.Pausesound();
        yield return new WaitForSeconds(1.2f);
        SoundManager.inst.Playsound();
        yield return null;
    }

    void WaitHitSound()
    {
        SoundManager.inst.gethit.PlayOneShot(SoundManager.inst.gethit.clip);
    }

    void Punch() //일반 터치를 했을떄 앞의 장애물에 펀치하는 기능
    {
        myAnim.SetTrigger("Punch");                                     //펀치 애니메이션 호출 (펀치가 맞았든 맞지않았든 호출되어야함)

        Ray ray = new Ray(this.transform.position, Vector3.forward);    //플레이어 위치에서 정면을 향하는 레이 선언

        RaycastHit hit;                                                 //레이캐스트 정보 받아올 레이캐스트힛 클래스 선언

        Vector3 hitposition = gameObject.transform.position + gameObject.transform.forward * PunchDistance;


        if (Physics.Raycast(ray, out hit, PunchDistance, PunchLayer))   //펀치에 장애물이 맞았을경우
        {

            if (GameManager.inst.state == GameManager.STATE.TUTORIALPLAY)
            {
                hit.collider.gameObject.GetComponent<TutorialNormalNote>().Punched();
                SwipeManager.swipeDirection = Swipe.None;
                UImanagement.instance.t_objcnt++;

                hitposition = hit.point;
                GameObject BBBBB = Instantiate(m_EffectPrefab, hitposition, Quaternion.LookRotation(hit.normal));


                Destroy(BBBBB, 0.5f);
                SoundManager.inst.punchsound.PlayOneShot(SoundManager.inst.punchsound.clip);
                SwipeManager.swipeDirection = Swipe.None;
            }
            else { 
            ScoreManager.inst.HitNote(hit.distance < (PunchDistance / 2));
            //hit.collider.enabled = false;
            hit.collider.gameObject.GetComponent<NormalNote>().Punched();      //해당 장애물에서 펀치당했을때의 함수를 호출
           
            hitposition = hit.point;
            GameObject BBBBB = Instantiate(m_EffectPrefab, hitposition, Quaternion.LookRotation(hit.normal));


            Destroy(BBBBB, 0.5f);
            SoundManager.inst.punchsound.PlayOneShot(SoundManager.inst.punchsound.clip);
            SwipeManager.swipeDirection = Swipe.None;
            }
        }

    }
}
