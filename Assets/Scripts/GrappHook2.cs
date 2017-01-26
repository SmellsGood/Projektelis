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
        else { SwingTimer -= Time.deltaTime; if (KaTikPaleido) { HandleFall(); } }
        if (SwingTimer <= 0f)
        {
            loopCounter = loopCounterMin;
            loopCounterAdd = loopCounterMin;
            SwingTimer = 1.5f;
            momentine = swingPower;
        }
    }

    private int supimosiPuse = 0, loopCounter = 15, oldSupimosiPuse, loopCounterMin = 15, loopCounterMax = 42, loopCounterAdd = 15;
    private float SwingTimer = 1.5f, swingPower = 3000f;
    public float momentine = 3000f;
    void HandleSwing() //supimasis
    {
        KaTikPaleido = false;

        loopCounter--;
        Debug.Log(loopCounter);
        if (Input.GetKey(KeyCode.A))
        {
            supimosiPuse = -1;
            if (loopCounter >= 1 && player.transform.position.y + 10 < transform.position.y) playerPhysics.AddForce(transform.right * (-momentine)); player.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            supimosiPuse = 1;
            if (loopCounter >= 1 && player.transform.position.y + 10 < transform.position.y) playerPhysics.AddForce(transform.right * momentine); player.transform.localScale = new Vector3(1, 1, 1);
        }

        if (supimosiPuse != oldSupimosiPuse)
        {
            if (loopCounter <= loopCounterMax)
            {
                if (loopCounterAdd < loopCounterMax) loopCounterAdd += 4; if (momentine <= 4000f) momentine += 100f;
            }
            loopCounter = loopCounterAdd;
        }
        oldSupimosiPuse = supimosiPuse;

        KaTikPaleido = true;
    }

    void HandleFall() {
    playerPhysics.AddForce(transform.right* supimosiPuse * momentine); player.transform.localScale = new Vector3(1, 1, 1);
       if(momentine>0) momentine = momentine - 20f;
       else KaTikPaleido=false;
}
}
