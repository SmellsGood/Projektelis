using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float speed, health = 5, player_speed, acceleration = 120, speed_y,  WJTime, pullDownStrenght, liftupSpeed = 1700, maxYSpeed;
    private bool iswalking, boi, Falling = true, leftshift, isrunning, forWjump, walled, walled2, facingRight, oldPosition, alreadyTouchedDaWall;
    public bool canWJump = true, canDoubleJ, FDamage;
    private int maxSpeed = 4, loopCounter;
    public int jumpheight;
    private float time_then, wallJtime, FirstCoordinates, SecondCoordinates;
    public float jdelay = 1, defSpeed, FallDamage;
    private string wallTag;
    
    private Animator anim;
    private Rigidbody2D dude;
    GameObject wdetecdtor, GroundDetector, wdetector2, LiftUpDetector;
    WallDetection fScript;
    WallDetection2 fScript2;
    GroundDetection gScript;
    LiftUpDetection lScript;
    void Start()
    {
        
        wdetecdtor = GameObject.Find("WallDetector");
        wdetector2 = GameObject.Find("WallDetector2");
        GroundDetector = GameObject.Find("GroundDetector");
        LiftUpDetector = GameObject.Find("LiftUpDetector");
        fScript = wdetecdtor.GetComponent<WallDetection>();
        fScript2 = wdetector2.GetComponent<WallDetection2>();
        gScript = GroundDetector.GetComponent<GroundDetection>();
        lScript = LiftUpDetector.GetComponent<LiftUpDetection>();
        dude = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        
    }

    void Update()
    {
        //Animator Values
        bool ground_anim = gScript.ground;
        anim.SetBool("IsWalking", iswalking);
        anim.SetBool("Walling", walled2);
        anim.SetBool("Ground", ground_anim);
        anim.SetBool("LShift", leftshift);
        anim.SetBool("IsRunning", isrunning);
        anim.SetFloat("Player_speed", player_speed);
        // anim.SetBool("Sticky", sticky);

        player_speed = dude.velocity.x;
        walled2 = fScript.walling;
        walled = fScript2.walling;
        wallTag = fScript2.wallTag;
        speed_y = dude.velocity.y;


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
                
        if (dude.velocity.x > maxSpeed)
        {
            dude.velocity = new Vector2(maxSpeed, dude.velocity.y);
        }

        if (dude.velocity.x < -maxSpeed)
        {
            dude.velocity = new Vector2(-maxSpeed, dude.velocity.y);
        }
        if (dude.velocity.y < -maxYSpeed - 10) dude.velocity = new Vector2(dude.velocity.x, -maxYSpeed);


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
    void Accelaration()
    {
        if (speed < 6000f)
        {
            speed = acceleration + speed;
            if (speed > 3500) acceleration = 400;
        }
        else speed = 6000f;
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
                    isrunning = true;
                    Accelaration();
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
                isrunning = false;
            } }
    }
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

    void FixedUpdate()
    {
        if (lScript.liftup && !walled2)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                dude.velocity = new Vector2(-dude.velocity.x, dude.velocity.y);
                dude.AddForce(transform.up * liftupSpeed);
            }
        }
                
        liftUp();
        fallDamage();
        handleShift();

        if (gScript.ground == true)
        {
            canDoubleJ = true;
            canWJump = true;
            alreadyTouchedDaWall = false;
        }

        if (forWjump)
        {
            loopCounter++;
            if (loopCounter > 10)
            {
                dude.velocity = new Vector2(dude.velocity.x, jumpheight*0.8f);
                forWjump = false;
                loopCounter = 0;
                transform.localScale =  new Vector3(-transform.localScale.x, 1, 1);
            }
        }

        if (-100 < player_speed && player_speed < 100 && player_speed != 0) iswalking = true;
        else iswalking = false;

        if (Time.time - wallJtime < 0.2f || Time.time - WJTime < 0.3f || gameObject.GetComponent<DistanceJoint2D>().enabled) return; //vaiksciojimas
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (Time.time - wallJtime > 0.2f) dude.AddForce(transform.right * (-speed));
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (Time.time - wallJtime > 0.2f) dude.AddForce(transform.right * speed);
            }
        }
    }
}