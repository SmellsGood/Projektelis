using UnityEngine;
using System.Collections;

public class WallDetection : MonoBehaviour
{
    public bool walling;
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
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "softGround") walling = true;

    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "softGround") walling = true;

    }

    void OnTriggerExit2D(Collider2D col)
    {
        walling = false;
    }

}
