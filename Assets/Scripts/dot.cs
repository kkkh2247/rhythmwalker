using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class dot : MonoBehaviour
{
    public Vector3 startpos;
    public Vector3 endpos;
    public Vector3 endrotate;
    private void Start()
    {
        Sequence sq = DOTween.Sequence()
            .AppendInterval(0.1f)
            .Append(transform.DOMove(endpos, 5f, false));
        sq.Play();

        Sequence sq1 = DOTween.Sequence()
           .AppendInterval(0.1f)
           .Append(transform.DORotate(endrotate, 5f, RotateMode.Fast))
           .AppendCallback(() =>
           {
               GameManager.inst.state = GameManager.STATE.NONE;
           });
        sq1.Play();
       
    }

    void play()
    {
        GameManager.inst.state = GameManager.STATE.NONE;
    }
}
