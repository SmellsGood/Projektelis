using UnityEngine;
using System.Collections;


public class Skeleton_AI : MonoBehaviour
{
    private Rigidbody2D larry;
    private bool allowTimer, allowChase = true;
    private float detTimer = 0.2f, AfterKnockTimer=0.35f;
    private int health = 100;
    public float speed,  jumpheight, KnockDuration, KnockPower, maxSpeed,  DoSmthTimer, timer, dirTimer, followTimer=5f;
    public string mode;
    public bool canMove, direction;
    public WallDetection wScript, platform;
    public GroundDetection gScript;
    public PlayerDetection dScript, attackScript;
    public Transform player;
    // Use this for initialization
    void Start()
    {
        larry = gameObject.GetComponent<Rigidbody2D>();
        timer = DoSmthTimer;
        dirTimer = DoSmthTimer * 2;
        mode = "idle";
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowChase)
        {
            AfterKnockTimer -= Time.deltaTime;
            if (AfterKnockTimer <= 0)
            {
                AfterKnockTimer = 0.35f;
                allowChase = true;
            }}

        if (larry.velocity.x > maxSpeed)
        {
            larry.velocity = new Vector2(maxSpeed, larry.velocity.y);
        }

        else if (larry.velocity.x < -maxSpeed)
        {
            larry.velocity = new Vector2(-maxSpeed, larry.velocity.y);
        }

        if (mode == "idle") idle();
        else if (mode == "aggressive") goAfter();
        else if (mode == "fight") attack();
    }

   
    

    void idle()
    {
        if (timer <= 0)
        {
            canMove = !canMove;
            timer = DoSmthTimer;
        }
        if (dirTimer <= 0)
        {
            direction = !direction;
            dirTimer = DoSmthTimer; //*2 kad eitu maziau
        }
        if (detTimer <= 0)
        {
            if (!platform.walling || wScript.walling) direction = !direction;
            detTimer = 0.2f;
        }


        if (canMove)
        {
            if (direction)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                larry.AddForce(transform.right * (-speed));
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                larry.AddForce(transform.right * speed);
            }

        }

        if (dScript.iSeeIt) mode = "aggressive";
        speed = 450f;
        timer -= Time.deltaTime;
        dirTimer -= Time.deltaTime;
        detTimer -= Time.deltaTime;
    }


    void goAfter()
    {
        speed = 900f;       
        if(gScript.ground && wScript.walling && allowChase) { larry.velocity = (new Vector2(larry.velocity.x, jumpheight));     
        }
        if (player.position.x > transform.position.x)
        {
            if (player.position.x - transform.position.x > 1f && allowChase==true)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                larry.AddForce(transform.right * speed);
        }}
        else
        {
            if ((transform.position.x  - player.position.x > 1f) && allowChase == true)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                larry.AddForce(transform.right * (-speed));
            }}
        if (!dScript.iSeeIt)  allowTimer = true; 
        if(allowTimer)
        {
            followTimer -= Time.deltaTime;
            if(followTimer<=0)
            {
                    followTimer = 5f;
                    mode = "idle";
                    allowTimer = false;
            }
        }
        else if (attackScript.iSeeIt) mode = "fight";
    }

    void attack()
    {
        if(!attackScript.iSeeIt)
        {
            mode = "aggressive";
        }

    }

    void Damage(int hitpoints)
    {
        health -= hitpoints;
        Debug.Log(health);
        if (health <= 0) Destroy(gameObject);
        StartCoroutine(KnockBack());//IEnumeratoriu pradeda 
    }
    public IEnumerator KnockBack()
    {
        float timeris=0, z;
        if (player.position.x >= gameObject.transform.position.x) z = -1;
        else z = 1;
        allowChase = false;
        while (timeris <= KnockDuration)
        {
            timeris += Time.deltaTime;
            larry.AddForce(new Vector2(2*z, 1)* KnockPower);          
        }
        yield return 0;
    }
}
