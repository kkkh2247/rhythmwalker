using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance = null;

    public TextMesh Scoretext;
    public TextMesh[] RankingScoreText;
    public TextMesh[] RankingNameText;
    public InputField inputfield;


    bool IsGet;

    [HideInInspector]
    public bool IsStart;
    [HideInInspector]
    public int Score; // 점수
    [HideInInspector]
    public string Name; // 이름

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetRanking()
    {
        for (int i = 0; i < RankingScoreText.Length; i++)
        {
            bool IsActive = PlayerPrefs.HasKey("Name" + i); // 만약에 Playerprefs에 저장되있는 것 중에 저런 이름이 있으면 True 없으면 false다 

            if (!IsActive)
            { // 처음에 실행되면 값이 아무것도 없으니
                RankingNameText[i].text = // TST라고 넣어주고
                    "TST";
                RankingScoreText[i].text = // 점수도 이걸로 넣어주자
                    "0";
                return; // 그리고 리턴해주자
            }

            // 텍스트에 이름이랑 점수 가져옴
            RankingScoreText[i].text =
                PlayerPrefs.GetInt("Rank" + i).ToString();
            RankingNameText[i].text =
                PlayerPrefs.GetString("Name" + i).ToString();
        }


    }



    void Save()//이름 집어넣어주는 부분
    {
        Name = inputfield.text;
        Score = ScoreManager.inst.score;
        SetRanking();
    }

    // 여기에 있는 점수랑 이름 가져와서 playerprefs에 넣어주자
    void SetRanking()
    {
        if (IsGet) return; // 계속 세팅해주면 안되니까 리턴시키자
        for (int i = 0; i < RankingScoreText.Length; i++)
        {

            if (Score >= PlayerPrefs.GetInt("Rank" + i.ToString())) // 만약에 지금 점수가 저장되어있는 점수보다 크면
            {
                for (int j = (RankingScoreText.Length) - i; j > 0; j--)
                {
                    PlayerPrefs.SetInt("Rank" + j.ToString(), PlayerPrefs.GetInt("Rank" + (j - 1).ToString())); // 하나씩 떙겨주자
                    PlayerPrefs.SetString("Name" + j.ToString(), PlayerPrefs.GetString("Name" + (j - 1).ToString())); // 이름도 떙겨주자
                }
                PlayerPrefs.SetInt("Rank" + i.ToString(), Score); // 그리고나서 넣어주자
                PlayerPrefs.SetString("Name" + i.ToString(), Name); // 이름도 넣어주자
                break; // 그리고 돌필요가 없으니 빠져나가자
            }
        }
        // PlayerPrefs.Save();

        GetRanking(); // 세팅해줬으니 텍스트에 넣어주어야 하니 불러오자
    }
}
