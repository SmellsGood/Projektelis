using UnityEngine;
using System.Collections;

public partial class GrappHook : MonoBehaviour
{
    float highestPoint = 1000, angle;
    void getHighestPoint()
    {
        //jei nespaus A\D tai suveik jega, suveiks kai krenta
         // nustato kai zmogus auksciausiai pakiles hooko atzvildgiu
        
            highestPoint = location.transform.position.y - player.transform.position.y; //note: kuo mazesne tuo auksciau pakyla
         float angle =highestPoint / hook.distance;
        
        //Debug.Log("kampas: " + highestPoint);

            //beta/90*distance*x     ---jegos formulė
        

    }
}
