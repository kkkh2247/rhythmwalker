using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMove : MonoBehaviour
{
   // public static UiMove inst = null;

    private RectTransform SelectPos;

    [SerializeField]
    private Vector2 distance;

    public bool IsActive;


    void Awake()
    {
       // if (inst == null)
      //      inst = this;
       // else
         //   Destroy(this.gameObject);
    }

    void Start()
    {
        SelectPos = GetComponent<RectTransform>();

        SelectPos.anchoredPosition = new Vector2(Screen.width + distance.x,distance.y);
        //SelectPos.anchoredPosition = new Vector2(distance.x, distance.y);
    }

    void Update()
    {
        if (IsActive) {
             SelectPos.anchoredPosition = Vector2.Lerp(SelectPos.anchoredPosition, Vector2.zero, 0.1f);
           // SelectPos.anchoredPosition = Vector2.Lerp(SelectPos.anchoredPosition, Vector2.zero, 0.1f);
        }
        else {
            SelectPos.anchoredPosition = Vector2.Lerp(SelectPos.anchoredPosition, new Vector2(Screen.width + distance.x, distance.y), 0.1f);
          //  SelectPos.anchoredPosition = Vector2.Lerp(SelectPos.anchoredPosition, new Vector2(distance.x, distance.y), 0.1f);
        }
    }

}
