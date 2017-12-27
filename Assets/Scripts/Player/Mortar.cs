using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum MortarTypes
{
    Starter,
    Relax,
    Normal,
    Angree
}
public class Mortar : MonoBehaviour
{
    public MortarTypes MortarType;
    public bool hasDegrees = true;
    public Transform DegreesObj;
    public AnimationCurve firstEase;
    public List<GameObject> OtherBodyParts;
    public float Speed = 10;

    bool touchOver;
    bool tweenDid;
    Tweener dT;
    private void Update()
    {
        HandleRotation();
        HandleTouchOver();
        HandleBallCathing();
    }
    void HandleBallCathing()
    {
        if (LevelManager.currentMortar == this)
        {
            if(MortarType == MortarTypes.Angree)
            {
                Shoot();
            }
        }
    }
    void HandleTouchOver()
    {
        if (!touchOver)
        {
            dT = DegreesObj.DOScale(0, 0.4f).SetEase(Ease.OutSine);
        }
    }
    public void TouchIsOver()
    {
        touchOver = true;
        Debug.Log("TouchIsOver");
        dT = DegreesObj.DOScale(1, 0.5f).SetEase(firstEase);
    }
    public void TouchUpdate(Vector3 Dir)
    {
        if (MortarType != MortarTypes.Angree && MortarType != MortarTypes.Normal)
            transform.DORotateQuaternion(Quaternion.LookRotation(Vector3.forward, -Vector3.Cross(Dir.normalized, Vector3.forward)), 0.5f);
    }
    public void TouchOut()
    {
        touchOver = false;
        Shoot();
    }
    void HandleRotation()
    {
        GetComponent<SpriteRenderer>().flipY = transform.up.y < 0;
    }
    void Shoot()
    {
        LevelManager.ball.Shoot(transform.right, Speed);
        LevelManager.currentMortar = null;
    }
}
