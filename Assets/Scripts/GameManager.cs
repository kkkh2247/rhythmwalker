using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum STATE { NONE, PLAY, END,CLEAR,TUTORIAL,OPENING,TUTORIALPLAY};

    public STATE state;

    public static GameManager inst;

    public float jumpDistance = 5f;
    public float SpawnPosZ = 0f;

    private const float stepDistance = 5f;

    public bool cheatmodeauto = false;
    public bool cheatmodegod = false;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        state = STATE.OPENING;
        Application.targetFrameRate = 60;
    }

    public void NextStep()
    {
        SpawnPosZ += stepDistance;
    }

    public void GameStart(string str)
    {
        state = STATE.PLAY;
        switch (str) {
            case "TestMusic":
                NoteManager.inst.StartMusic("TestMusic");
                break;
            case "bike":
                NoteManager.inst.StartMusic("bike");
                break;
            case "GetOudside":
                NoteManager.inst.StartMusic("GetOudside");
                break;
            case "BeatYourCompetition":
                NoteManager.inst.StartMusic("BeatYourCompetition");
                break;
            case "Bringiton":
                NoteManager.inst.StartMusic("Bringiton");
                break;
            case "forever":
                NoteManager.inst.StartMusic("forever");
                break;
        }

    }

    public void GameTutorial()
    {
        state = STATE.TUTORIAL;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cheatmodeauto = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cheatmodeauto = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cheatmodegod = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            cheatmodegod = false;
        }
    }
}
