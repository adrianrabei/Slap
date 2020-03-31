using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{

    void Start()
    {
        int currentPower = 25 + PlayerPrefs.GetInt("PlayerDamage", 0);
        this.GetComponent<Text>().text = currentPower.ToString();
        if (int.Parse(this.GetComponent<Text>().text) > 999) 
        {
            this.GetComponent<Text>().fontSize = 6;
        }
        else this.GetComponent<Text>().fontSize = 8;

    }

    void Update()
    {
        
    }
}
