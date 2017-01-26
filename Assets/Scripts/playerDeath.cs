using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class playerDeath : MonoBehaviour {

    private bool  canZoom;
    public Camera camera;
    public CameraView cScript;
    public ImageEffects BlacknWhite;
    public float fixCamera;
    float originalSize;
    public AudioSource a;
	// Use this for initialization
	void Start () {
        camera = Camera.main;
        cScript = camera.GetComponent<CameraView>();
        originalSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
      

        if (canZoom)
        {
            if (a.isPlaying)
            {
               camera.orthographicSize = camera.orthographicSize - 0.1f;
                cScript.fix = 10;
                camera.GetComponent<Grayscale>().enabled = true;
                try { Destroy(GameObject.Find("Player").GetComponent<Movement>()); }
                catch { };
            }
            else
            {
                cScript.fix = fixCamera; 
                canZoom = false;
                camera.orthographicSize = originalSize;
                camera.GetComponent<Grayscale>().enabled = false;
                Application.LoadLevel(Application.loadedLevelName);
            }
        }

        if (gameObject.tag == "Finish")
        {
            if (!a.isPlaying)
            {
                a.enabled = true;
                a.Play();
                canZoom = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!a.isPlaying)
            {
                a.enabled = true;
                a.Play();
                canZoom = true;
            }
        }
    }
}
