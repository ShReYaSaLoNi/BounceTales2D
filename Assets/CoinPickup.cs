using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    public int coin = 0;

    [SerializeField] private TMP_Text cointext;
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Collectable"){
            Destroy(other.gameObject);
            coin += 1;
        }
        cointext.text = "Points:" + coin;
    }

}
