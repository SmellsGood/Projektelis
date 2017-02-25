using UnityEngine;
using System.Collections;

public partial class GrappHook {

    void unHook()
    {
        {
            if (hook.enabled && (Input.GetKey(KeyCode.Space)))
            {
                player.GetComponent<Rigidbody2D>().velocity = (new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpHeight));
                mScript.canDoubleJ = false;
            }                               
            spring.enabled = false; hook.enabled = false; line.enabled = false;  isHooked = false; pasiHookino = false; //viska atjungia
            springTimerAtm = springTimer;
            GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled = false;
            playerPhysics.gravityScale = 50;
            mScript.graplinghook = false;
        }
    }

    void checkIfTouches()
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

    void mouseClick()
    {
        {   // suranda peles koord., atema is ju zaidejo vieta ir taip suranda sovimo trajektorija.
            GameObject.Find("hookLook").GetComponent<SpriteRenderer>().enabled = true;
            playerPhysics.gravityScale = 20;
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
            if (!gScript.ground) playerPhysics.gravityScale = 0.01f;
        }
    }

    void aukstyn(bool a)
    {
        springOff = true;
        if (a)
        {
            hook.enabled = true;
            hook.distance = Vector2.Distance(gameObject.transform.position, location.transform.position);
            if (hook.distance > 1) hook.distance = hook.distance - 1f;
            pasikeiteDistance();
        }
        else
        {
            hook.distance = Vector2.Distance(gameObject.transform.position, location.transform.position);
            float gap = gameObject.transform.position.y - location.transform.position.y;
            if (hook.distance < 100 && (player.transform.position.y < gameObject.transform.position.y)) hook.distance = hook.distance + 1f;
            pasikeiteDistance();
        }
    }
}
