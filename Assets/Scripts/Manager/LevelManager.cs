using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;
    public static Ball ball;
    public bool ballInAir = false;
    public static Mortar[] levelMortars;
    public static Mortar currentMortar;
    public Mortar starterMortar;
    public Ball ballPrefab;

    private void Awake()
    {
        levelMortars = FindObjectsOfType<Mortar>();
        Debug.Log(levelMortars.Length);
        main = this;
        ball = Instantiate(ballPrefab,starterMortar.transform.position + new Vector3(0,4),Quaternion.identity);
        currentMortar = starterMortar;
    }
    private void Update()
    {
        if (!ballInAir)
        {
            ball.transform.position = currentMortar.transform.position;
        }
    }
    
}
