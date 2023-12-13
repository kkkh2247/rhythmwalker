using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCol :  MonoBehaviour
{
    
    public static PlayerCol inst;

    public enum STATE {NONE,HIT };

    public STATE state;

    public Image img;

    //[SerializeField]
    //private Sprite RedLifeSprite;
    //[SerializeField]
    //private Sprite BlackLifeSprite;

    //[SerializeField]
    //private Image[] LifeImages;

    private int life;

    [SerializeField]
    private int startLife;

    public bool CheckHit;

    private void Awake()
    {
        inst = this;
        life = startLife;
        CheckHit = false;
        state = STATE.NONE;
    }

    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "huddle" && CheckHit == false)
        {
          //  Handheld.Vibrate();
            state = STATE.HIT;
            CheckHit = true;
            col.gameObject.transform.position = Vector3.zero;
            ScoreManager.inst.FailNote();
            if(GameManager.inst.state == GameManager.STATE.PLAY)
            col.GetComponent<Note>().isCollision = true;
            Damaged();
            PlayerCtrl.instance.StartCoroutine(PlayerCtrl.instance.Gethit());
            Invoke("DelayCheckHit", 1.2f);
            NoteManager.inst.endTime += 1.2f;
            
            if(GameManager.inst.state == GameManager.STATE.TUTORIALPLAY)
            {
                UImanagement.instance.t_count++;
            }
        }
    }

    void DelayCheckHit()
    {
        CheckHit = false;
        state = STATE.NONE;
    }

    void Damaged()
    {
        life--;
        SetHeartSprite();

       
    }

    void SetHeartSprite()
    {
        int black = startLife - life;

        for (int i = 0; i < startLife; i++)
        {
            //if (i < black)
            //    LifeImages[i].sprite = BlackLifeSprite;
            //else
            //    LifeImages[i].sprite = RedLifeSprite;
        }
    }

    
}
