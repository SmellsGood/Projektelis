using UnityEngine;
using System.Collections;

public class grapphookarturo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

//grapplingHook.cs
/*
public class GraplingHook : MonoBehaviour {

    public float fireRate = 0;
    public LayerMask whatToHit;

    float timeToFire = 0;
    public float Graple_Distance = 100f;
    Transform firePoint;

	// Use this for initialization
	void Awake () {
        firePoint = transform.FindChild ("Graple");
        if (firePoint == null)
        {
            Debug.LogError("No Graple detected. Lmao");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (fireRate == 0)
        {

            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
            }
        }
    }
    
    void Shoot(){
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, Graple_Distance, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
        if (hit.collider != null)
        {
            Destroy(GetComponent<DistanceJoint2D>());
            //gameObject.AddComponent<DistanceJoint2D>();
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
        }
        else
        {
            Destroy(GetComponent<DistanceJoint2D>());
        }
    }

	
}
*/

//grappHookLame
/*
public class GraplingHookLame : MonoBehaviour
{

    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 10f;
    public LayerMask mask;


    // Use this for initialization
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        Debug.Log((targetPos).ToString());
        

        hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);

        if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            joint.enabled = true;

        }
    }
}
 */

