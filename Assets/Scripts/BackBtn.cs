using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    private bool IsExit = false;


    void Update()
    {
        Pause();
        ExitInput();
    }

    void ExitInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if(GameManager.inst.state == GameManager.STATE.NONE)
            if (Input.GetKeyDown(KeyCode.Escape))
                IsExit = !IsExit;

            if (IsExit)
                UImanagement.instance.MoveUI(UImanagement.instance.exitimage);
            else
                UImanagement.instance.ReturnUI(UImanagement.instance.exitimage);
            
        }
    }

    public void Exit(bool isexit)
    {
        IsExit = isexit;

        if (IsExit)
            Application.Quit();
        else
            UImanagement.instance.ReturnUI(UImanagement.instance.exitimage);
    }

    void Pause()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (GameManager.inst.state == GameManager.STATE.PLAY)
                if (Input.GetKeyDown(KeyCode.Escape))
                    print("일시정지");
            // 멈추는 이미지 띄우기
                
        }
    }
}
