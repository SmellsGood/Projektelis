using UnityEngine;
using System.Collections;

public partial class GrappHook : MonoBehaviour
{
    Rigidbody2D rb2d, playerPhysics;
    public float speed = 300f, pulling = 100f, jumpHeight;
    private float delay = 0.3f, timer = 0.3f;
    GameObject location, player, groundDet;
    DistanceJoint2D hook;
    GroundDetection gScript;
    LineRenderer line;
    Movement mScript;      
    Vector2 target;
    public Transform linijosPradzia;
    public bool isHooked, isShot;

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
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        mScript = player.GetComponent<Movement>();
        playerPhysics = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {   // suranda peles koord., atema is ju zaidejo vieta ir taip suranda sovimo trajektorija.
                GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled = true;
                try { gameObject.AddComponent<Rigidbody2D>(); } catch { };
                hook.enabled = false;
                isHooked = false;
                rb2d = gameObject.GetComponent<Rigidbody2D>();
                Transform tr = location.transform;
                gameObject.transform.position = tr.position;
                target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                Vector2 trajectory = target - new Vector2(location.transform.position.x, location.transform.position.y);
                trajectory.Normalize();
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg + 270);
                transform.rotation = rotation; //transform.rotation = rotation;
                rb2d.velocity = trajectory * speed;
                isShot = true;
                timer = delay;
                if(!gScript.ground)  playerPhysics.gravityScale = 0.01f;
            }
        }
        else timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q))
        {
            isHooked = false;
            line.enabled = false;
            if (hook.enabled && (Input.GetKey(KeyCode.Space)))
            {
                player.GetComponent<Rigidbody2D>().velocity = (new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpHeight));
                mScript.canDoubleJ = false;
            }
            hook.enabled = false;
            GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled = false;
            playerPhysics.gravityScale = 50;
        }

        if (isShot)
        { //jeigu nieko nepasiekia (atstumas tarp sovinio ir zaidejo)
            float distance = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
            if (distance > 100 || distance < -100)
            {
                GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled = false;
                line.enabled = false;
                hook.enabled = false;
                isShot = false;
                Destroy(gameObject.GetComponentInChildren<Rigidbody2D>());
            }
        }

        if (GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled)
        {  //linijos grafikai
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, new Vector2(linijosPradzia.position.x, linijosPradzia.position.y));
        } 


    }

    void FixedUpdate()
    {
        if (GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled) //kai ijungta textura
        {
            if (Input.GetKey(KeyCode.W) && isHooked)
            {
                hook.enabled = true;
                hook.distance = Vector2.Distance(gameObject.transform.position, location.transform.position);
                if (hook.distance > 1) hook.distance = hook.distance - 1f;
            }

            else if (Input.GetKey(KeyCode.S) && !gScript.ground)
            {

                hook.distance = Vector2.Distance(gameObject.transform.position, location.transform.position);
                float gap = gameObject.transform.position.y - location.transform.position.y;
                if (hook.distance < 100 && (player.transform.position.y < gameObject.transform.position.y)) hook.distance = hook.distance + 1f;
            }

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
            hook.enabled = true;
            hook.connectedAnchor = gameObject.transform.position;
            isHooked = true;
            playerPhysics.gravityScale = 300;
            float distance = Vector2.Distance(gameObject.transform.position, location.transform.position);
            hook.distance = distance;
            isShot = false;
            Quaternion goodOne = transform.rotation; //kad galetu normaliai suptis ant hooko
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject.Find("hookLook").transform.rotation = goodOne; // end
            loopCounter = loopCounterMax*2/3;
            momentine = swingPower;
        }
    }

    
}

