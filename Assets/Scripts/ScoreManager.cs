using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Rank { S, A, B, C, D }

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager inst; //싱글턴

    private int failNote;           //실패한 노트
    private int combo;            //콤보

    private int perfectNote;
    private int goodNote;

    private float judgeTIme;

    private int maxCombo;
    public int score;

    [Header("콤보 표시할 텍스트")]
    [SerializeField]
    private Text comboText;

    [Header("판정 표시할 텍스트")]
    [SerializeField]
    private Text judgeText;

    [Header("결과 창 오브젝트")]
    public GameObject ResultCanvasObject;

    [Header("결과 창 텍스트")]
    [SerializeField]
    private Text[] ResultCanvasText;

    private void Awake()
    {
        inst = this;
        Initialize();
    }

    private void Update()
    {
        judgeText.color = Color.Lerp(judgeText.color, new Color(judgeText.color.r, judgeText.color.g, judgeText.color.b, 0), Time.time - judgeTIme);
    }

    public void Initialize()    //변수 초기화
    {
        perfectNote = 0;
        goodNote = 0;
        failNote = 0;
        combo = 0;
        comboText.text = "";
        judgeText.enabled = false;
        judgeTIme = 0f;
        maxCombo = 0;
        score = 0;
    }

    //public void HitNote()   //노트를 성공적으로 쳤을때
    //{
    //    successNote++;
    //    AddCombo();

    //    print("HitNote");
    //}

    public void SetResultCanvas()
    {
        ResultCanvasObject.SetActive(true);
        ResultCanvasText[0].text = GetRank().ToString();
        ResultCanvasText[1].text = perfectNote.ToString();
        ResultCanvasText[2].text = goodNote.ToString();
        ResultCanvasText[3].text = failNote.ToString();
        ResultCanvasText[4].text = maxCombo.ToString();
        ResultCanvasText[5].text = score.ToString();
    }

    public void HitNote(bool isPerfect)   //노트를 성공적으로 쳤을때
    {
        if (isPerfect)
        {
            perfectNote++;
            SetJudgeText("Perfect");
            score += 300 + combo;
        }
        else
        {
            goodNote++;
            SetJudgeText("Good");
            score += 200 + combo;
        }

        AddCombo();

    }



    public void FailNote()  //노트를 실패했을때
    {
        failNote++;
        ClearCombo();

        SetJudgeText("Fail");

    }

    private void SetJudgeText(string str)
    {
        judgeText.enabled = true;

        judgeText.text = str;

        Color temp = Color.white;

        switch (str)
        {
            case "Perfect":
                temp = Color.HSVToRGB(0.23f, 0.8f, 1f);
                break;
            case "Good":
                temp = Color.HSVToRGB(0.23f, 0.4f, 1f);
                break;
            case "Fail":
                temp = Color.HSVToRGB(0.23f, 0.1f, 1f);
                break;
        }



        judgeTIme = Time.time + 0.5f;
        judgeText.color = temp;
    }


    private void AddCombo() //콤보 더해주기
    {
        comboText.fontSize = 110;
        combo++;
        if (combo > maxCombo) maxCombo = combo;
        DisplayCombo();
        StartCoroutine(combodisplay());
    }

    IEnumerator combodisplay()
    {
        while (true)
        {
            if (comboText.fontSize <= 70) break;
            comboText.fontSize -= 2;
            yield return null;
        }


    }

    private void ClearCombo()   //콤보 초기화
    {
        combo = 0;
        DisplayCombo();
    }

    private void DisplayCombo() //텍스트에 콤보 출력
    {
        // comboText.fontSize = 80;

        if (combo == 0) comboText.text = "";
        else
            comboText.text = combo.ToString() + " Combo";


    }


    public Rank GetRank()   //게임 끝나고 랭크 받아오는 함수
    {
        int totalNote = perfectNote + goodNote + failNote;         //전체 노트 수 계산

        float rat = (float)failNote / (float)totalNote; //전체 노트중 틀린 비율

        if (rat < 0.01f)
        {
            return Rank.S;
        }
        else if (rat < 0.05f)
        {
            return Rank.A;
        }
        else if (rat < 0.1f)
        {
            return Rank.B;
        }
        else if (rat < 0.2f)
        {
            return Rank.C;
        }
        else
        {
            return Rank.D;
        }
    }
}
