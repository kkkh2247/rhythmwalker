using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenRotation : MonoBehaviour
{
    public Canvas[] Scalers;
    bool IsWdith = false;
    void Start()
    {
        if (!IsWdith)
        {
            foreach (Canvas s in Scalers)
            {
                s.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            }
           
        }
        else
        {
            foreach (Canvas s in Scalers)
            {
                s.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
        }

    }

    private void Update()
    {
        if (Screen.autorotateToPortrait)
        {
            Width();
        }
        //else if (Screen.autorotateToLandscapeLeft)
        //{
        //    Hegiht();
        //}
    }

    public void Width()
    {
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        foreach (Canvas s in Scalers)
        {
            s.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
    }

    public void Hegiht()
    {
        //Screen.orientation = ScreenOrientation.Portrait;
        foreach (Canvas s in Scalers)
        {
            s.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }
}
