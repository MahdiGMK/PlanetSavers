using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    public List<Touch> touchs = new List<Touch>();

    Mortar current;
    Mortar previous;
    public Transform TouchBubble;
    private void Start()
    {
        previous = current;
    }
    private void Update()
    {
        HandleTouchOverObjects();
    }
    void HandleTouchOverObjects()
    {
        touchs.Clear();
        touchs.AddRange(Input.touches);
        if (Input.GetMouseButton(0))
        {
            Touch m = new Touch();
            m.position = Input.mousePosition;
            touchs.Add(m);
        }
        if (touchs.Count == 0)
        {
            if (current)
                current.TouchOut();
            previous = current = null;
        }
        foreach (Touch t in touchs)
        {
            Ray r = Camera.main.ScreenPointToRay(t.position);
            RaycastHit rh;
            if (current)
            {
                TouchBubble.GetComponent<SpriteRenderer>().DOFade(1, 0.4f);
                Vector3 TargetPosition = new Vector3(r.origin.x, r.origin.y, 0);
                Vector3 Dir = (TargetPosition - current.transform.position);
                if (Dir.sqrMagnitude > 8.5)
                {
                    TargetPosition = Dir.normalized * 4.25f + current.transform.position;
                }
                TouchBubble.DOMove(TargetPosition, 0.1f);
                current.TouchUpdate(Dir);
            }
            if (Physics.Raycast(r.origin, Vector3.forward, out rh, 20))
            {
                Mortar m = rh.transform.parent.GetComponent<Mortar>();
                if (m && m == LevelManager.currentMortar)
                {
                    current = m;
                    m.TouchIsOver();
                    if (current != previous && previous)
                    {
                        previous.TouchOut();
                    }
                    previous = current;

                    break;
                }
            }
        }
        if (!current)
        {
            TouchBubble.GetComponent<SpriteRenderer>().DOFade(0, 0.4f);
        }


    }
}
