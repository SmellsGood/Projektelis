using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinDetection : MonoBehaviour {

    public int coin;
    public Text coincount;

    // Use this for initialization
    void Start () {
        coin = 0;
        CoinCount();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            coin = coin + 1;
            CoinCount();
        }

    }
    
    void CoinCount ()
    {
        coincount.text = "lmao :" + coin.ToString();
    }
    
}
