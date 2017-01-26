using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {

    public float speed_x;
    public float speed_y;
    private Rigidbody2D maMomsCar;
    private GameObject player;
    public bool  inCar;
    private SliderJoint2D slider;
    GroundDetection gScript;
    Movement mainScript;

    // Use this for initialization
    void Start () {
        maMomsCar = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        gScript = player.GetComponentInChildren<GroundDetection>();
        mainScript = player.GetComponentInChildren<Movement>();
        slider = player.GetComponent<SliderJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gScript.sticky) if (Input.GetKeyDown(KeyCode.F)) inCar = !inCar;


        if (inCar)
        {
            Movement();
            slider.enabled = true;
            mainScript.enabled = false;
        }
        else {
            mainScript.enabled = true;
            slider.enabled = false;
        }

        //transform.Translate(speed_x * Time.deltaTime, speed_y * Time.deltaTime, 0);
    }


    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            maMomsCar.velocity = new Vector2(speed_x, maMomsCar.velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            maMomsCar.velocity = new Vector2(-speed_x, maMomsCar.velocity.y);
        }
        if (Input.GetKey(KeyCode.W))
        {
            maMomsCar.velocity = new Vector2(maMomsCar.velocity.x, speed_y);
        }
        if (Input.GetKey(KeyCode.S))
        {
            maMomsCar.velocity = new Vector2(maMomsCar.velocity.x, -speed_y);
        }
    }
}
