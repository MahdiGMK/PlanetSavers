using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerDoor : MonoBehaviour {
    public bool Change;
    public GameObject Ston;
    public GameObject Glass;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Change)
        {
            Change = false;
            if (Ston.active)
            {
                Ston.SetActive(false);
                Glass.SetActive(true);
            }
            else if (Glass.active)
            {
                Glass.SetActive(false);
                Ston.SetActive(true);
            }
        }
	}
}
