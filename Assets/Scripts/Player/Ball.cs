using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Ball : MonoBehaviour {
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(Vector2 Direction,float Speed)
    {
        LevelManager.main.ballInAir = true;
        transform.position = LevelManager.currentMortar.transform.position + new Vector3(Direction.x,Direction.y);
        rb.velocity = Direction * Speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Mortar om = other.gameObject.GetComponent<Mortar>();
        if (om)
        {
            LevelManager.main.ballInAir = false;
            LevelManager.currentMortar = om;
        }
    }
}
