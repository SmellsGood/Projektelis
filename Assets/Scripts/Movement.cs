using UnityEngine;
using System.Collections;

public partial class Movement : MonoBehaviour
{
    public float speed, health = 5, player_speed, acceleration = 400, speed_y,  WJTime, pullDownStrenght, liftupSpeed = 1700, maxYSpeed;
    private bool iswalking, drifting, boi, ground_anim, landing, Falling = true, leftshift, swap, isrunning, forWjump, walled, walled2, facingRight, oldPosition, alreadyTouchedDaWall, idle_anim;
    public bool canWJump = true, canDoubleJ, FDamage, overrideA = true, overrideD = true, graplinghook;
    private int maxSpeed = 4, loopCounter, idletimer = 3, drifttimer = 50, swaptimer = 5;
    public int jumpheight;
    private float time_then, wallJtime, FirstCoordinates, SecondCoordinates, driftspeed=500f    ;
    public float jdelay = 1, defSpeed, FallDamage;
    private string wallTag;
    
    private Animator anim;
    private Rigidbody2D dude;
    GameObject wdetecdtor, GroundDetector, wdetector2, LiftUpDetector, LandingDetector;
    WallDetection fScript;
    WallDetection2 fScript2;
    GroundDetection gScript;
    LiftUpDetection lScript;
    LandingDetection landingScript;
    void Start()
    {
        
        wdetecdtor = GameObject.Find("WallDetector");
        wdetector2 = GameObject.Find("WallDetector2");
        GroundDetector = GameObject.Find("GroundDetector");
        LiftUpDetector = GameObject.Find("LiftUpDetector");
        LandingDetector = GameObject.Find("LandingDetector");
        fScript = wdetecdtor.GetComponent<WallDetection>();
        fScript2 = wdetector2.GetComponent<WallDetection2>();
        gScript = GroundDetector.GetComponent<GroundDetection>();
        lScript = LiftUpDetector.GetComponent<LiftUpDetection>();
        landingScript = LandingDetector.GetComponent<LandingDetection>();
        dude = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        
    }

    void Update()
    {
        //Animator Values
        anim.SetBool("IsWalking", iswalking);
        anim.SetBool("Hook", graplinghook);
        anim.SetBool("Walling", walled2);
        anim.SetBool("Drifting", drifting);
        anim.SetBool("Landing", landing);
        anim.SetBool("Ground", ground_anim);
        anim.SetBool("Idle", idle_anim);
        anim.SetBool("Drifting Swap", swap);
        anim.SetBool("LShift", leftshift);
        anim.SetBool("IsRunning", isrunning);
        anim.SetFloat("Player_speed", player_speed);
        // anim.SetBool("Sticky", sticky);

        ground_anim = gScript.ground;
        player_speed = dude.velocity.x;
        walled2 = fScript.walling;
        landing = landingScript.landing;
        walled = fScript2.walling;
        wallTag = fScript2.wallTag;
        speed_y = dude.velocity.y;

        //SWITCHING AXIS
        if (Input.GetAxis("Horizontal") < -0.1f && Time.time - WJTime > 0.9f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        if (Input.GetAxis("Horizontal") > 0.1f && Time.time - WJTime > 0.9f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!walled)
            {
                if (canDoubleJ && !gameObject.GetComponent<DistanceJoint2D>().enabled)
                {
                    float laiks = Time.time;
                    if (laiks - time_then > jdelay && !walled)
                    {
                        dude.velocity = (new Vector2(dude.velocity.x, jumpheight * 0.7f));
                        canDoubleJ = false;
                    }
                }
                else if (gScript.ground)
                {
                    canDoubleJ = true;
                    dude.velocity = (new Vector2(dude.velocity.x, jumpheight));
                    time_then = Time.time;
                    alreadyTouchedDaWall = true;
                }
            }
            else if (walled) walljump();
        }
                
        
        if (dude.velocity.y < -maxYSpeed - 10) dude.velocity = new Vector2(dude.velocity.x, -maxYSpeed);


    }

    //WALKING
    void walking()
    {
        if (swap == true) return;
        if (Time.time - wallJtime < 0.2f || Time.time - WJTime < 0.3f || gameObject.GetComponent<DistanceJoint2D>().enabled) return;
        else
        {
            if (Input.GetKey(KeyCode.A) && overrideA == true)
            {
                overrideD = false;
                if (Time.time - wallJtime > 0.2f) dude.AddForce(transform.right * (-speed));
            }
            else overrideD = true;
            if (Input.GetKey(KeyCode.D) && overrideD == true)
            {
                overrideA = false;
                if (Time.time - wallJtime > 0.2f) dude.AddForce(transform.right * speed);
            }
            else overrideA = true;
        }
    }

        void walljump()
    {
        if (wallTag == "softGround")
        {
            if (Time.time - wallJtime > 0.35f && Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0;
                if (facingRight) dude.AddForce(new Vector2(-6000, 8000));
                else dude.AddForce(new Vector2(6000, 8000));
                speed = 1500;
                wallJtime = Time.time;
            }
        }
        else
        {
            if (oldPosition != facingRight && walled) canWJump = true;
            if (canWJump && !gScript.ground)
            {
                if (facingRight) dude.AddForce(new Vector2(-23000, 15000));
                else dude.AddForce(new Vector2(23000, 15000));
                canWJump = false;
                oldPosition = facingRight;
                forWjump = true;
                speed = 5000;
                WJTime = Time.time;
            }
        }
    }

    void handleShift()
    {
        if (!gScript.ground && !walled && !lScript.liftup) speed = 3000f;  //ore 
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                leftshift = true;
                if (walled)
                {
                    speed = defSpeed;
                    if (wallTag != "softGround") { dude.velocity = new Vector2(dude.velocity.x, -50f); }
                    else { dude.velocity = new Vector2(dude.velocity.x, -2f); }
                }
                else if (gScript.ground)
                {
                    if (lScript.liftup) speed = 3000f;
                }
            }
            else
            {
                acceleration = 50;
                leftshift = false;
                if (walled && !gScript.ground)
                {
                    speed = defSpeed;
                    dude.velocity = new Vector2(dude.velocity.x, -80f);
                }
                else speed = defSpeed;
            } }
    }

    //ACCELERATION
    void Accelaration()
    {
        if (speed < 3000f)
        {
            speed = acceleration + speed;
            if (speed > 1500) acceleration = 200;
        }
    }

        //FALL DAMAGE
        void  fallDamage()
    {
        if (speed_y <= -79 && Falling == true)
        {
            FirstCoordinates = transform.position.y;
            Falling = false;
        }
        if (gScript.ground == true && Falling == false)
        {
            SecondCoordinates = transform.position.y;
            FallDamage = 2 * ((FirstCoordinates - SecondCoordinates) - 50);
            Falling = true;
            boi = true;
        }

        if (FallDamage >= 2 && boi == true)
        {
            FDamage = true;
            boi = false;
        }
    }
    void liftUp()
    {
        if (lScript.liftup && !walled2)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift) && gScript.ground)
                {
                    dude.velocity = new Vector2(-dude.velocity.x, dude.velocity.y);
                    dude.AddForce(transform.up * liftupSpeed * 2f);
                }
                else
                {
                    dude.velocity = new Vector2(-dude.velocity.x, dude.velocity.y);
                    dude.AddForce(transform.up * liftupSpeed);
                }
            }
        }
    }

}