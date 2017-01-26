using UnityEngine;
using System.Collections;

public class LiftUpDetection : MonoBehaviour {

    public bool liftup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "softGround") liftup = true;

    }
    void OnTriggerExit2D(Collider2D col)
    {
        liftup = false;
    }





}
