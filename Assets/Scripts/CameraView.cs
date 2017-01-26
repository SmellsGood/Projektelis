using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    private Transform target;
    public float maxSize, minSize, step, fix = 20;
    private float size, ok, smallAdd = 1.3f;
    public Camera cam;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        ok = Input.GetAxis("ScrollWheel");

    }

    void LateUpdate()
    {
        size = cam.orthographicSize;
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax) + fix, -10);
        if (ok >= 0.1f)//up
        {
            if (size > minSize)
            {
                cam.GetComponent<Camera>().orthographicSize = cam.GetComponent<Camera>().orthographicSize - step;
                fix = fix - smallAdd;
            }
        }
        else if (ok <= -0.1f)//down
        {
            if (size < maxSize)
            {
                cam.GetComponent<Camera>().orthographicSize = cam.GetComponent<Camera>().orthographicSize + step;
                if (fix <= 20) fix = fix + smallAdd;
            }
        }
        
    }
}
