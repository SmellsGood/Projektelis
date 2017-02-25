using UnityEngine;
using System.Collections;

public class LandingDetection : MonoBehaviour
{
    public bool landing;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "softGround") landing = true;

    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "softGround") landing = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {
        landing = false;
    }

}
