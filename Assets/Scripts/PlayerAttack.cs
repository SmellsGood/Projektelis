using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public int kiekKerta;
    public bool canAttack;
    public float chillTimer, timer;
    public AudioSource audio;
    private Collider2D enemis;
    //public string name;
	void Start () {
        timer = chillTimer;
	}
	

	void Update () {
        if (timer <= 0)
        {           
            if (canAttack && Input.GetKeyDown(KeyCode.Mouse0)) //pult
            {
                timer = chillTimer;
                enemis.SendMessageUpwards("Damage", kiekKerta);
                audio.Play();
            }
        }
        if (timer > 0) timer -= Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            canAttack = true;
            enemis = col;
            //name = col.gameObject.name;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            canAttack = true;
            enemis = col;
            //name = col.gameObject.name;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        canAttack = false;
    }
}
