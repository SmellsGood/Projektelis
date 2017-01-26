using UnityEngine;
using System.Collections;

public class portal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player") {
            if (Application.loadedLevelName == "world1")
            {                Application.LoadLevel("world2");
            }
            else if (Application.loadedLevelName == "world2") Application.LoadLevel("world1");
            } }
}
