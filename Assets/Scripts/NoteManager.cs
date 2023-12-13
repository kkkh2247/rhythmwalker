using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using UnityEngine.UI;

public class cNote
{
    public float time; //해당 노트를 쳐야 할 시간
    public int type;

    public cNote(float _time, int _type)
    {
        time = _time;
        type = _type;
    }
}

public class NoteManager : MonoBehaviour
{
    public static NoteManager inst;

    private float startTime;        //게임 시작 시간
    private float lastSpawnTime;    //가장 마지막으로 노트가 스폰된 시간

    private int musicCount;       //음악 개수

    private Queue<cNote> musicSheet; //해당 게임에서 칠 노트들의 정보가 전부 들어가 있는 큐

    //private Queue<GameObject> notes; //현재 스폰되어있는 노트들을 담아두는 큐

    private GameObject[,] notes; //미리 생성해둔 노트들을 담아두는 배열
    private GameObject[,] normalNotes; //펀치 노트들 담아둘 배열
    private int[] noteCnt;      //불러올 노트 번호를 저장해두는 배열 (메모리풀을 위해 정의됨)
    private int[] normalNoteCnt;




    [SerializeField]
    private AudioSource audioSource; //게임 음악
    [Header("노트 프리팹")]
    [SerializeField]
    private GameObject[] notePrefabs;

    public GameObject[] tutorialnotePrefabs;

    [Header("판정선에서 스폰지점까지의 거리")]
    [SerializeField]
    private float distance;
    [Header("노트가 다가오는 스피드")]
    [SerializeField]
    public float speed;

    [Header("미리 생성해둘 노트 개수 (종류별)")]
    [SerializeField]
    private int InitNoteNum = 20;

    [Header("음악 파일들")]
    [SerializeField]
    private AudioClip[] musicFiles;

    private GameObject noteobj;

    public float endTime;

    [Header("펀치 노트 오브젝트들")]
    [SerializeField]
    private GameObject[] NormalNotePrefs;

    [Header("노트 속도 조절할 슬라이더")]
    [SerializeField]
    private Slider speedSlider;

    private float firstSpeed;



    private void Awake()
    {
        inst = this;

        //변수초기화부분
        musicCount = -1;
        endTime = 9999999f;
        firstSpeed = speed;
    }

    void Start()
    {
        noteobj = new GameObject("noteobj");
        InitNoteObjects();
    }

    public void StartMusic(string musicName)
    {
        ReadMusicSheet(musicName);
        StartSpawn();
        switch (musicName)
        {
            case "TestMusic":
                audioSource.clip = musicFiles[0];
                break;
            case "bike":
                audioSource.clip = musicFiles[1];
                break;
            case "GetOudside":
                audioSource.clip = musicFiles[2];
                break;
            case "BeatYourCompetition":
                audioSource.clip = musicFiles[3];
                break;
            case "Bringiton":
                audioSource.clip = musicFiles[4];
                break;
            case "forever":
                audioSource.clip = musicFiles[5];
                break;
        }

        //SoundManager.inst.Playsound();
        audioSource.Play();
        startTime = 0.0f;
        lastSpawnTime = 0.0f;
        ScoreManager.inst.Initialize();
        endTime = 9999999f;
        speed = firstSpeed * (speedSlider.value + 0.5f);
    }



    private void Update()
    {
        if (GameManager.inst.state == GameManager.STATE.PLAY)
        {
            PlayMusicSheet();
        }

        if (Time.time > endTime)
        {
            GameEnd();
            endTime = 9999999f;
        }
    }

    private GameObject GetNote(int noteNum) //타입에 맞는 노트를 불러오는 함수
    {
        int temp = noteCnt[noteNum] % InitNoteNum;



        noteCnt[noteNum]++;
        return notes[noteNum, temp];
    }

    private GameObject GetNormalNote() //타입에 맞는 노트를 불러오는 함수
    {
        int noteNum = Random.Range(0, NormalNotePrefs.Length);

        print(noteNum);

        int temp = normalNoteCnt[noteNum] % InitNoteNum;



        normalNoteCnt[noteNum]++;
        return normalNotes[noteNum, temp];
    }

    private void InitNoteObjects() //메모리풀을 위해서 시작할때 미리 노트 오브젝트들을 생성해두는 함수
    {
        notes = new GameObject[notePrefabs.Length, InitNoteNum];
        normalNotes = new GameObject[NormalNotePrefs.Length, InitNoteNum];

        noteCnt = new int[notePrefabs.Length];
        normalNoteCnt = new int[NormalNotePrefs.Length];

        for (int i = 0; i < noteCnt.Length; i++) noteCnt[i] = 0;
        for (int i = 0; i < normalNoteCnt.Length; i++) normalNoteCnt[i] = 0;

        for (int i = 0; i < InitNoteNum; i++)
        {
            for (int j = 0; j < notePrefabs.Length; j++)
            {
                notes[j, i] = Instantiate(notePrefabs[j], Vector3.zero, Quaternion.identity);
                notes[j, i].transform.parent = noteobj.transform;
            }

            for (int j = 0; j < NormalNotePrefs.Length; j++)
            {
                normalNotes[j, i] = Instantiate(NormalNotePrefs[j], Vector3.zero, Quaternion.identity);
                normalNotes[j, i].transform.parent = noteobj.transform;
            }
        }


    }

    private void ReadMusicSheet(string musicName) //json파일을 읽어와서 노트 정보를 큐에 담는 함수
    {
        string fileName = "musics";
        //string fileName = "bike";
        //읽어올 파일 이름

        string resStr = Resources.Load<TextAsset>(fileName).text;
        //파일 불러옴

        musicSheet = new Queue<cNote>();
        //노트 저장할 큐 초기화

        var json = Json.Deserialize(resStr) as Dictionary<string, object>;

        musicCount = json.Count;

        List<object> jMusicSheet = (List<object>)json[musicName];
        //파일 읽어온 내용 리스트로 저장

        List<object> noteInfo;

        for (int i = 0; i < jMusicSheet.Count; i++)
        //리스트 읽어온 것을 Note클래스에 맞추어 저장하고 큐에 넣음
        {
            noteInfo = (List<object>)jMusicSheet[i];

            float time = float.Parse(noteInfo[1].ToString()) / 1000;
            int type = int.Parse(noteInfo[0].ToString());

            cNote nt = new cNote(time, type);

            lastSpawnTime = nt.time;
            musicSheet.Enqueue(nt);
        }

        //notes = new Queue<GameObject>();


        startTime = Time.time;      //시작시간지정


    }

    public int GetMusicCount()  //음악 개수 받아가는 함수
    {
        return musicCount;
    }


    public bool PlayMusicSheet() //노트를 타이밍에 맞춰 소환해주는 함수
    //startTime = 곡이 시작된 시간
    {
        //float curTime = Time.time - startTime;  //curTime = 곡 시작 후 지난 시간
        float curTime = audioSource.time;
        float timing = 0;

        try
        {
            timing = musicSheet.Peek().time - (distance / speed); //노트가 소환될 타이밍 계산
            //생성 타이밍 = 타격타이밍 - (거리/속도)
        }
        catch
        {
            print("ddd");
            //Invoke("GameEnd", 13f);
            endTime = Time.time + 18f;
            GameManager.inst.state = GameManager.STATE.CLEAR;
            return false;
            //소환 못할경우 게임 종료 코드 필요
        }
        if (timing <= curTime) //노트가 소환될 타이밍인경우 소환
        {
            cNote note = musicSheet.Dequeue();

            if (note.type < 5)
                SpawnNote(note);
            else
                CurveMap(note.type);
            return true;
        }

        return false;
    }

    private void CurveMap(int type)
    {
        switch (type)
        {
            case 5:
                CurvedControls.inst.CurveMap(20, 0);
                break;
            case 6:
                CurvedControls.inst.CurveMap(-20, 0);
                break;
            case 7:
                CurvedControls.inst.CurveMap(0, 0);
                break;
        }

    }

    private void SpawnNote(cNote note)   //노트 소환해주는 함수
    {
        Vector3 notePos = this.transform.position + new Vector3(0, 0, distance);

        //Note temp = Instantiate(notePrefabs[note.type], notePos, Quaternion.identity).GetComponent<Note>();
        //temp.time = note.time;
        //temp.type = note.type;

        //temp.transform.parent = noteobj.transform;

        GameObject temp;

        if (note.type != 0)
            temp = GetNote(note.type);
        else
            temp = GetNormalNote();

        temp.transform.position = notePos;
        temp.GetComponent<Note>().time = note.time;
        temp.GetComponent<Note>().type = note.type;

        temp.transform.parent = noteobj.transform;
    }

    private void StartSpawn() //게임 시작할떄 미리 생성되어있어야 하는 노트들 생성
    {
        float curTime = audioSource.time;
        float timing = 0;

        try
        {
            timing = musicSheet.Peek().time - (distance / speed); //노트가 소환될 타이밍 계산
        }
        catch
        {
            //소환 못할경우 게임 종료 코드 필요
            //Invoke("GameEnd", 10f);
            print("d");
            //Invoke("GameEnd",10f);

            return;
        }
        if (timing <= curTime) //노트가 소환될 타이밍인경우 소환
        {
            cNote note = musicSheet.Dequeue();
            float pos = (note.time - curTime) * speed;

            if (note.type < 5)
            {
                Note temp;
                if (note.type == 0)
                    temp = Instantiate(NormalNotePrefs[Random.Range(0, NormalNotePrefs.Length)], this.transform.position + new Vector3(0, 0, pos), Quaternion.identity).GetComponent<Note>();
                else
                    temp = Instantiate(notePrefabs[note.type], this.transform.position + new Vector3(0, 0, pos), Quaternion.identity).GetComponent<Note>();



                temp.time = note.time;
                temp.type = note.type;

                temp.transform.parent = noteobj.transform;
            }
            else
            {
                CurveMap(note.type);
            }
            StartSpawn();

        }

    }

    void GameEnd()
    {
        GameManager.inst.state = GameManager.STATE.CLEAR;
        noteobj = new GameObject("noteobj");
        //UImanagement.instance.rankimage.enabled = true;
        //UImanagement.instance.ranktext.enabled = true;
        //UImanagement.instance.ranktext.text = "Rank " + "A";
        ScoreManager.inst.SetResultCanvas();
        ScoreManager.inst.Initialize();
        audioSource.Stop();
        //print("clear");
        Invoke("ReturnStage", 3f);
        //print(ScoreManager.inst.GetRank());
    }

    void ReturnStage()
    {
        GameManager.inst.state = GameManager.STATE.NONE;
        UImanagement.instance.selectsongcanvas.GetComponent<UiMove>().IsActive = true;
        //UImanagement.instance.rankimage.enabled = false;
        //UImanagement.instance.ranktext.enabled = false;
        ScoreManager.inst.ResultCanvasObject.SetActive(false);
        for (int i = 0; i < UImanagement.instance.songtext.Length; i++)
        {
            UImanagement.instance.songtext[i].enabled = true;
        }
    }


}
