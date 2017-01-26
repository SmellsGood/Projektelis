using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour {
    public bool iSeeIt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") iSeeIt = true;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") iSeeIt = true;
    }
    void OnTriggerExit2D(Collider2D col)  {
        if (col.gameObject.tag == "Player") iSeeIt = false;
    }

}
