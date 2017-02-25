using UnityEngine;
using System.Collections;

public partial class  GrappHook  {

    bool KaTikPaleido;

    void SwingMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // judejimas ant zemes ir swinginimasis
        {
            springOff = true; springTimerAtm = springTimer;
            if (gScript.ground) { hook.enabled = false; playerPhysics.gravityScale = 50; } //
            else
            {
                HandleSwing(); playerPhysics.gravityScale = 150;    // SITAS KENKEJAS YRA <=========================================================================================================================================

            }
        }
        else { SwingTimer -= Time.deltaTime; }
        if (SwingTimer <= 0f)
        {
            minusminusMomentine = 5;
            loopCounter = loopCounterMin;
            loopCounterAdd = loopCounterMin;
            SwingTimer = 1.5f;
            momentine = swingPower;
        }
    }

    private int supimosiPuse = 0,  oldSupimosiPuse, loopCounterMin = 10, loopCounterAdd = 10, minusMomentine = 100, minusminusMomentine = 10;
    private float SwingTimer = 1.5f, loopCounter = 10, swingPower = 3000f, sulaikimoTimer, atvirkstine = 3000f, loopCounterMax = 42, maxJega = 4000f, kiekAddintLoop = 4, kiekPridetJegos = 100;
    public float momentine = 3000f;
    void HandleSwing() //supimasis
    { 

        loopCounter--;
        Debug.Log(loopCounter);
        if (Input.GetKey(KeyCode.A))
        {          
            supimosiPuse = -1;
            if (loopCounter >= 1 && player.transform.position.y + 10 < transform.position.y) { atvirkstine = momentine; playerPhysics.AddForce(transform.right * (-momentine)); player.transform.localScale = new Vector3(-1, 1, 1); }
            else { if (atvirkstine > 2500) { atvirkstine -= minusminusMomentine; if (minusminusMomentine <= minusMomentine) minusminusMomentine += 5;  ; playerPhysics.AddForce(transform.right * (-atvirkstine)); player.transform.localScale = new Vector3(-1, 1, 1); } }       
        }
        else if (Input.GetKey(KeyCode.D))
        {
            supimosiPuse = 1;
            if (loopCounter >= 1 && player.transform.position.y + 10 < transform.position.y) { atvirkstine = momentine;  playerPhysics.AddForce(transform.right * momentine); player.transform.localScale = new Vector3(1, 1, 1); }
            else { if (atvirkstine > 2500) { atvirkstine -= minusminusMomentine; if (minusminusMomentine <= minusMomentine) minusminusMomentine += 5; playerPhysics.AddForce(transform.right *atvirkstine); player.transform.localScale = new Vector3(1, 1, 1); } }
        }

        if (supimosiPuse != oldSupimosiPuse)
        {
            if (loopCounter <= loopCounterMax)
            {
                if (loopCounterAdd < loopCounterMax) loopCounterAdd += 4; if (momentine <= maxJega) { momentine += kiekPridetJegos; }
            }
            loopCounter = loopCounterAdd;
        }
        oldSupimosiPuse = supimosiPuse;

        
    }

    void pasikeiteDistance()
    {
        //23 distance idealus; 42 loopCounterMax 
        // 23 distance; 4000f maxjega;

        loopCounterMax = (100 * 42) / hook.distance;
        maxJega = Mathf.Clamp((100 * 4000) / hook.distance, 0, 20000f);
        kiekAddintLoop = (100 * 5) / hook.distance; //+=4 ant 23 distance
        if (hook.distance > 30f) kiekAddintLoop = 1.5f * kiekAddintLoop;
        kiekPridetJegos = Mathf.Clamp((100 * 100) / hook.distance, 0, 50);

    }

}
