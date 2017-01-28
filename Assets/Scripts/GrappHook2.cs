using UnityEngine;
using System.Collections;

public partial class  GrappHook  {

    bool KaTikPaleido;

    void SwingMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // judejimas ant zemes ir swinginimasis
        {

            if (gScript.ground) { hook.enabled = false; playerPhysics.gravityScale = 50; } //
            else
            {
                HandleSwing(); playerPhysics.gravityScale = 150;

            }
        }
        else { SwingTimer -= Time.deltaTime; }
        if (SwingTimer <= 0f)
        {
            loopCounter = loopCounterMin;
            loopCounterAdd = loopCounterMin;
            SwingTimer = 1.5f;
            momentine = swingPower;
        }
    }

    private int supimosiPuse = 0, loopCounter = 15, oldSupimosiPuse, loopCounterMin = 15, loopCounterAddMax = 42, loopCounterAdd = 15, minusMomentine=90;
    private float SwingTimer = 1.5f, swingPower = 3000f, sulaikimoTimer;
    public float momentine = 3000f;
    void HandleSwing() //supimasis
    { 

        loopCounter--;
        Debug.Log(loopCounter);
        if (Input.GetKey(KeyCode.A))
        {          
            supimosiPuse = -1;
            if (loopCounter >= 1 && player.transform.position.y + 10 < transform.position.y) { if (momentine <= 3000) momentine = 3000f; playerPhysics.AddForce(transform.right * (-momentine)); player.transform.localScale = new Vector3(-1, 1, 1); }
            else { if (momentine > 2500) { momentine -= minusMomentine; playerPhysics.AddForce(transform.right * (-momentine)); player.transform.localScale = new Vector3(-1, 1, 1); } }       
        }
        else if (Input.GetKey(KeyCode.D))
        {
            supimosiPuse = 1;
            if (loopCounter >= 1 && player.transform.position.y + 10 < transform.position.y) { if (momentine <= 3000) momentine = 3000f; playerPhysics.AddForce(transform.right * momentine); player.transform.localScale = new Vector3(1, 1, 1); }
            else { if (momentine > 2500) { momentine -= minusMomentine; playerPhysics.AddForce(transform.right * momentine); player.transform.localScale = new Vector3(1, 1, 1); } }
        }

        if (supimosiPuse != oldSupimosiPuse)
        {
            if (momentine > swingPower)  momentine = swingPower;
            if (loopCounter <= loopCounterAddMax)
            {
                if (loopCounterAdd < loopCounterAddMax) loopCounterAdd += 4; if (momentine <= 4000f) momentine += 100f;
            }
            loopCounter = loopCounterAdd;
        }
        oldSupimosiPuse = supimosiPuse;

        
    }

}
