using UnityEngine;
using System.Collections;

public partial class GrappHook : MonoBehaviour
{
    Rigidbody2D rb2d, playerPhysics;
    public float speed = 800f, pulling = 100f, jumpHeight;
    private float delay = 0.3f, timer = 0.3f;
    GameObject location, player, groundDet;
    DistanceJoint2D hook;
    SpringJoint2D spring;
    GroundDetection gScript;
    LineRenderer line;
    Movement mScript;      
    Vector2 target;
    public Transform linijosPradzia;
    public bool isHooked, isShot, pasiHookino,hookbool;

    // Use this for initialization
    void Start()
    {

        GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled = false;
        rb2d = gameObject.GetComponentInChildren<Rigidbody2D>();
        location = GameObject.Find("grappHook");
        player = GameObject.Find("Player");
        groundDet = GameObject.Find("GroundDetector");
        gScript = groundDet.GetComponent<GroundDetection>();
        hook = player.GetComponent<DistanceJoint2D>();
        spring = player.GetComponent<SpringJoint2D>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        mScript = player.GetComponent<Movement>();
        playerPhysics = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) { if (Input.GetKeyDown(KeyCode.Mouse1)) mouseClick(); }      
        else timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q)) unHook();


        if (isShot) checkIfTouches();

        if (pasiHookino) HookoPerjunginejimas();
        

        if (GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled)
        {  //linijos grafikai
            line.enabled = true;
            mScript.graplinghook = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, new Vector2(linijosPradzia.position.x, linijosPradzia.position.y));
        } 


    }

    void FixedUpdate()
    {
        if (GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled) //kai ijungta textura
        {
            if (Input.GetKey(KeyCode.W) && isHooked) aukstyn(true);          
            else if (Input.GetKey(KeyCode.S) && !gScript.ground) aukstyn(false);
           

            SwingMovement();

            if (location.transform.position.y - transform.position.y > 0) hook.enabled = false; //jei zmogus auksciu uz hooka
            else if (!gScript.ground && isHooked) hook.enabled = true;         
        } // pabaiga judejimo
    }

    void OnTriggerEnter2D(Collider2D col) //paliecia pavirsiu
    {
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "softGround")
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            // hook.enabled = true;
            hook.connectedAnchor = gameObject.transform.position;                  
            spring.connectedAnchor = gameObject.transform.position; spring.enabled = true; springOff = false; pasiHookino = true ;  //0.1 tamping 2.5 frequency       
           // isHooked = true;
            pasikeiteDistance(); // normaliam supimuisi
            playerPhysics.gravityScale = 300;
            float distance = Vector2.Distance(gameObject.transform.position, location.transform.position); 
            spring.distance = distance;       
            isShot = false; //pataike, tai ijungia ifa update
            Quaternion goodOne = transform.rotation; //kad galetu normaliai suptis ant hooko
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("hookLook").transform.rotation = goodOne; // end
            loopCounter = loopCounterAddMax*2/3;
            momentine = swingPower;
        }
    }


    public float springTimer = 1.2f, springTimerAtm = 1.2f;
    bool springOff = true, atsijungiaHooks = false;
    void HookoPerjunginejimas()
    {
        // ar sptinginas ar normaliai
        if (springTimerAtm >= 0 && springOff==false) springTimerAtm -= Time.deltaTime;                   
        else 
        {
            hook.distance = Vector2.Distance(gameObject.transform.position, location.transform.position);
            spring.enabled = false;
            hook.enabled = true;
            isHooked = true;
            pasiHookino = false;
        }
        }
    }


 

