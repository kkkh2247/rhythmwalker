using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScale : MonoBehaviour
{

    private RectTransform title;
    [SerializeField]
    private Vector2 speed;
    [SerializeField]
    private Vector2 limit;

    private Vector2 size;

    bool Islimit = false;

    void Start()
    {
        title = GetComponent<RectTransform>();
        size = title.sizeDelta;
    }

    void Update()
    {
        if (title.sizeDelta.x >= limit.x ||
          title.sizeDelta.y >= limit.y)
        {
            Islimit = true;
        }

        if(title.sizeDelta.x < size.x ||
            title.sizeDelta.y < size.y){
                Islimit = false;
        }

        if(!Islimit)
        title.sizeDelta += speed; 

        if (Islimit)
            title.sizeDelta -= speed;
    }




}
