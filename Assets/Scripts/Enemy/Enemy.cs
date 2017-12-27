using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public enum Act
    {
        Go, Wait0, Back, Wait1
    }
    [HideInInspector]
    public Act act;
    [HideInInspector]
    public float time;
    [HideInInspector]
    public float percent;
    [HideInInspector]
    public Vector2 startPos;
    [HideInInspector]
    public Vector2 endPos;

    public float TimeToGo = 1;
    public AnimationCurve GoingFlow = new AnimationCurve();
    public float RestTime0 = 1;
    public float TimeToComeBack = 1;
    public AnimationCurve CommingFlow = new AnimationCurve();
    public float RestTime1 = 1;
    private void Update()
    {
        ManTime();
        DoBalance();
        DoMove();
    }
    void ManTime()
    {
        switch (act)
        {
            case Act.Go:
                time += UnityEngine.Time.deltaTime / TimeToGo;
                percent = GoingFlow.Evaluate(time);
                break;
            case Act.Wait0:
                time += UnityEngine.Time.deltaTime / RestTime0;
                percent = time;
                break;
            case Act.Back:
                time += UnityEngine.Time.deltaTime / TimeToComeBack;
                percent = CommingFlow.Evaluate(time);
                break;
            case Act.Wait1:
                time += UnityEngine.Time.deltaTime / RestTime1;
                percent = time;
                break;
        }
    }
    void DoBalance()
    {
        if (time > 0.99)
        {
            time = 0;
            percent = 0;
            ManActions();
        }
    }
    void ManActions()
    {
        switch (act)
        {
            case Act.Go:
                act = Act.Wait0;
                break;
            case Act.Wait0:
                act = Act.Back;
                break;
            case Act.Back:
                act = Act.Wait1;
                break;
            case Act.Wait1:
                act = Act.Go;
                break;
        }
    }
    void DoMove()
    {
        Vector3 WantedPos = Vector3.zero;
        switch (act)
        {
            case Act.Go:
                WantedPos = Vector2.Lerp(startPos, endPos, percent);
                break;
            case Act.Back:
                WantedPos = Vector2.Lerp(endPos, startPos, percent);
                break;
            case Act.Wait0:
                WantedPos = endPos;
                break;
            case Act.Wait1:
                WantedPos = startPos;
                break;
        }
        transform.position = WantedPos;
    }
}
