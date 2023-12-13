using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UImanagement : MonoBehaviour
{
    public static UImanagement instance = null;
    [Header("곡 선택 이미지")]
    public Image selectsongcanvas;
    [Header("타이틀 이미지")]
    public Image titleimage;
    [Header("콤보 텍스트")]
    public Text combotext;
    [Header("랭크 텍스트")]
    public Text ranktext;
    [Header("랭크 이미지")]
    public Image rankimage;
    [Header("설정슬라이더")]
    public  Slider bgmSlider, effectSlider;
    [Header("종료 이미지")]
    public Image exitimage;
    [Header("곡 텍스트")]
    public Button[] songtext;
    [Header("튜토리얼 메세지 텍스트")]
    public Text t_text;
    [Header("튜토리얼 메세지")]
    public string[] t_msg;
    public GameObject tutorialimage;

    private int t_cnt = 0;
    private int t_clearcnt = 0;
    [Header("튜토리얼 이미지")]
    [HideInInspector]
    public int t_objcnt = 0;
    [HideInInspector]
    public int t_count = 0;
    private bool Isexit = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void CloseCanvas(Canvas c)
    {
        c.transform.gameObject.SetActive(false);
    }

    public void OpenCanvas(Canvas c)
    {
        c.transform.gameObject.SetActive(true);
    }

    public void CloseGameObj(GameObject obj)
    {
        obj.transform.gameObject.SetActive(false);
    }

    public void OpenGameObj(GameObject obj)
    {
        obj.transform.gameObject.SetActive(true);
    }

    public void CloseButton(Button btn)
    {
        btn.enabled = false;
    }

    public void OpenButton(Button btn)
    {
        btn.enabled = true;
    }

    public void OpenImage(Image image)
    {
        image.enabled = true;
    }

    public void CloseImage(Image image)
    {
        image.enabled = false;
    }

    public void OpenText(Text t)
    {
        t.enabled = true;
    }

    public void CloseText(Text t)
    {
        t.enabled = false;
    }

    public void MoveUI(Image image)
    {
        image.GetComponent<UiMove>().IsActive = true;
    }

    public void ReturnUI(Image image)
    {
        image.GetComponent<UiMove>().IsActive = false;
    }

    //void SliderCtrl()
    //{
    //    SoundManager.inst.bgmSound.volume = bgmSlider.value; 

    //    SoundManager.inst.effectSound.volume = effectSlider.value;
    //}

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            Touch t = Input.GetTouch(0);

            if(GameManager.inst.state == GameManager.STATE.NONE) { 
            selectsongcanvas.gameObject.SetActive(true);
            selectsongcanvas.GetComponent<UiMove>().IsActive = true;
            titleimage.gameObject.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Isexit = !Isexit;
                    if (Isexit)
                        MoveUI(exitimage);
                    else
                        ReturnUI(exitimage);
                }
                    

            }

            if (GameManager.inst.state == GameManager.STATE.PLAY)
            {
                for (int i = 0; i < songtext.Length; i++)
                {
                    songtext[i].enabled = false;
                }
            }

            if (t.phase == TouchPhase.Ended && GameManager.inst.state == GameManager.STATE.TUTORIAL)
            {
                if (t_cnt > t_msg.Length) return;

                if (t_msg[t_cnt] == "/")
                {
                    switch (t_clearcnt)
                    {
                        case 0:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[0].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());

                            break;
                        case 1:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[1].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());
                            break;
                        case 2:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[2].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());
                            break;
                        case 3:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[3].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());
                            break;
                    }
                    // 플레이
                }

                if (t_msg[t_cnt] == "*")
                {
                    MoveUI(selectsongcanvas);
                    CloseGameObj(tutorialimage);
                }

                t_text.text = t_msg[t_cnt];
                t_cnt++;
            

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                print("00");
                selectsongcanvas.gameObject.SetActive(true);
                selectsongcanvas.GetComponent<UiMove>().IsActive = true;
                titleimage.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
               
                    Isexit = !Isexit;
                    if (Isexit)
                        MoveUI(exitimage);
                    else
                        ReturnUI(exitimage);
                
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {

                if (GameManager.inst.state == GameManager.STATE.OPENING) return;




                if (t_cnt > t_msg.Length) return;

                if (t_msg[t_cnt] == "/")
                {
                    switch (t_clearcnt)
                    {
                        case 0:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[0].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());

                            break;
                        case 1:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[1].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());
                            break;
                        case 2:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[2].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());
                            break;
                        case 3:
                            CloseGameObj(tutorialimage);
                            for (int i = 0; i < 3; i++) NoteManager.inst.tutorialnotePrefabs[3].transform.GetChild(i).gameObject.SetActive(true);
                            GameManager.inst.state = GameManager.STATE.TUTORIALPLAY;
                            StartCoroutine(tutorial());
                            break;
                    }
                    // 플레이
                }
                if (t_msg[t_cnt] == "*")
                {
                    MoveUI(selectsongcanvas);
                    CloseGameObj(tutorialimage);
                }
             
                t_text.text = t_msg[t_cnt];
                t_cnt++;
            }

            if (GameManager.inst.state == GameManager.STATE.PLAY)
            {
                for (int i = 0; i < songtext.Length; i++)
                {
                    songtext[i].enabled = false;
                }
            }

          

      
        }
       
    }

    

    IEnumerator tutorial()
    {
        SoundManager.inst.Playtutorialsound();

        yield return new WaitForSeconds(10f);
        
            NoteManager.inst.tutorialnotePrefabs[t_clearcnt].transform.GetChild(0).transform.position = new Vector3(-23.83f,2.5f,-142.5f);
            NoteManager.inst.tutorialnotePrefabs[t_clearcnt].transform.GetChild(1).transform.position = new Vector3(-23.83f, 2.5f, -130.5f);
            NoteManager.inst.tutorialnotePrefabs[t_clearcnt].transform.GetChild(2).transform.position = new Vector3(-23.83f, 2.5f, -118.5f);

        for (int i = 0; i < 3; i++)
        {
            NoteManager.inst.tutorialnotePrefabs[t_clearcnt].transform.GetChild(i).gameObject.SetActive(false);
            NoteManager.inst.tutorialnotePrefabs[0].transform.GetChild(i).GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(t_clearcnt <= 0) { 
        if (t_objcnt >= 3)
        {
            //t_cnt++;
            t_clearcnt++;
            t_text.text = "잘했어요";
        }
        else
        {
            t_cnt--;
            t_text.text = "다시해주세요";
        }
        }
        else
        {
            if(t_count == 0)
            {
                t_clearcnt++;
                t_text.text = "잘했어요";
            }
            else
            {
                t_cnt--;
                t_text.text = "다시해주세요";
                
            }
        }

        t_count = 0;

        t_objcnt = 0;
        OpenGameObj(tutorialimage);
        SoundManager.inst.Stoptutorialsound();
        GameManager.inst.state = GameManager.STATE.TUTORIAL;
       
    }
}

