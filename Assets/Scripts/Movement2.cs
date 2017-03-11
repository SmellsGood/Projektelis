using UnityEngine;
using System.Collections;

public partial class Movement {


    //MAIN SHIT GOES HERE
    void FixedUpdate()
    {



        liftUp();
        fallDamage();
        handleShift();
        Accelaration();
        

        if (gScript.ground == true)
        {
            canDoubleJ = true;
            canWJump = true;
            alreadyTouchedDaWall = false;
        }

        if (forWjump)
        {
            loopCounter++;
            if (loopCounter > 10)
            {
                dude.velocity = new Vector2(dude.velocity.x, jumpheight * 0.8f);
                forWjump = false;
                loopCounter = 0;
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            }
        }
        //"Very good"    MAX SPEED
        if (dude.velocity.x > maxSpeed)
        {
            dude.velocity = new Vector2(maxSpeed, dude.velocity.y);
        }
        if (dude.velocity.x < -maxSpeed)
        {
            dude.velocity = new Vector2(-maxSpeed, dude.velocity.y);
        }
        
        //isrunning and iswalking
        if (player_speed != 0 && player_speed < 40 && player_speed > -40)
        {
            isrunning = false;
            iswalking = true;
        }
        else
        {
            iswalking = false;
            if (player_speed == 0) isrunning = false;
        }

        if (Input.GetKeyUp(KeyCode.D) && isrunning == true) Debug.Log("yes");

        //drifting
        //Debug.Log(player_speed);
        if (player_speed > 40 || player_speed < -40)
        {
            isrunning = true;
            drifttimer = drifttimer - 1;
        }
        else { drifttimer = 50; }

        if (drifttimer < 0) drifting = true;
        else drifting = false;

        if (drifting == true && dude.velocity.x > 0 && Input.GetKeyUp(KeyCode.D) || drifting == true && dude.velocity.x < 0 && Input.GetKeyUp(KeyCode.A))
        {
            swaptimer = 5;
            swap = true;
        }
        if (swap == true)
        {
            if (Input.GetKeyDown(KeyCode.A) && player_speed > 0) dude.AddForce(new Vector2 (3000f - driftspeed, 0f));
            else player_speed = dude.velocity.x + acceleration;
            if (dude.velocity.x == 0) swap = false;
        }
        else walking();
        //IDLE TIMER
        if (player_speed == 0 && speed_y == 0)
        { idletimer = idletimer - 1; }
        else
        {
            idletimer = 3;
            idle_anim = false;
        }
        if (idletimer <= 0)
        { idle_anim = true; }
        
        }
    }


