using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    [Header("첫번쨰 맵 오브젝트")]
    [SerializeField]
    private GameObject map1;

    [Header("두번쨰 맵 오브젝트")]
    [SerializeField]
    private GameObject map2;

    //[Header("맵 사이의 거리")]
    //[SerializeField]
    private float distance;

    private float speed;

    private Vector3 firVec1;
    private Vector3 firVec2;


    void Start()
    {
        distance = Vector3.Distance(map1.transform.position, map2.transform.position);
        firVec1 = map1.transform.position;
        firVec2 = map2.transform.position;
        speed = NoteManager.inst.speed;
    }

    void Update()
    {
        if (PlayerCol.inst.CheckHit == false)
        {
            map1.transform.position += Vector3.back * speed * Time.deltaTime;
            map2.transform.position += Vector3.back * speed * Time.deltaTime;

            if (Vector3.Distance(map1.transform.position, firVec1) > distance)
            {
                map1.transform.position = firVec1;
                map2.transform.position = firVec2;
            }

        }
    }
}
