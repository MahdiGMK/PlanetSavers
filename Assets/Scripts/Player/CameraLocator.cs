using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraLocator : MonoBehaviour
{
    public float zOffset;
    public Transform _Targ;
    public Rigidbody2D _Rigid;

    Vector2 TargetLocation;
    Vector2 prevLocation;
    void Update()
    {
        if (_Targ == null)
            _Targ = LevelManager.ball.transform;
        else if (_Targ != null)
            TargetLocation = _Targ.position + new Vector3(_Rigid.velocity.x,_Rigid.velocity.y);
        if (TargetLocation != prevLocation)
        {
            Locate();
        }
        prevLocation = TargetLocation;
    }
    void Locate()
    {
        Debug.Log("Locating...");
        Vector2 p2 = TargetLocation;
        Vector3 p3 = p2;
        p3 += new Vector3(0, 0, zOffset);

        transform.DOMove(p3, 1).SetEase<Tween>(Ease.OutSine);
    }
}
