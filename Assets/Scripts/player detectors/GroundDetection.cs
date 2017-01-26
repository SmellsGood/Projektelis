using UnityEngine;
using System.Collections;

public class GroundDetection : MonoBehaviour
{
    public bool ground;
    public bool sticky;

    void Start() { }
    void Update() { }

    void OnTriggerEnter2D(Collider2D boi)
    {
        if (boi.gameObject.tag == "ground" || boi.gameObject.tag == "softGround" || boi.gameObject.tag == "sticky ground") ground = true;
        if (boi.gameObject.tag == "sticky ground") sticky = true;

    }
    void OnTriggerStay2D(Collider2D boi)
    {
        if (boi.gameObject.tag == "ground" || boi.gameObject.tag == "softGround" || boi.gameObject.tag == "sticky ground") ground = true;
        if (boi.gameObject.tag == "sticky ground") sticky = true;

    }

    void OnTriggerExit2D(Collider2D boi)
    {
        ground = false;
        sticky = false;
    }

    

}
