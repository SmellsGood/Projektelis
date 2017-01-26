using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float MaxHealth = 200f;
    public float CurrentHealth;
    public float HealthLeft;
    public GameObject Bar;
    GameObject Player;
    Movement mscript;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        mscript = Player.GetComponent<Movement>();
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       if (mscript.FDamage == true)
        {
            CurrentHealth = CurrentHealth - mscript.FallDamage;
            HealthLeft = CurrentHealth / MaxHealth;
            HealthBarDepletion(HealthLeft);
            mscript.FDamage = false;
        }
    }

    public void HealthBarDepletion(float PlayerHealth)
    {
        Bar.transform.localScale = new Vector3(PlayerHealth, Bar.transform.localScale.y, Bar.transform.localScale.z);
        if(CurrentHealth <= 0)
        {
            GameObject.Find("HealthBar").GetComponent<playerDeath>().enabled = true;
        }
    }
}

