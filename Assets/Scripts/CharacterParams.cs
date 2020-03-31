using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParams : MonoBehaviour
{
    private static int playerHealth = 100, playerDamage, enemyHealth = 100, enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        int playerBonusHealth = PlayerPrefs.GetInt("PlayerHealth", 0);
        int playerBonusDamage = PlayerPrefs.GetInt("PlayerDamage", 0);
        playerHealth += playerBonusHealth;
        playerDamage += playerBonusDamage;

    }

    private void OnApplicationPause(bool pause)
    {

        PlayerPrefs.SetInt("PlayerHealth", PlayerPrefs.GetInt("PlayerHealth", 0));
        PlayerPrefs.SetInt("PlayerDamage", PlayerPrefs.GetInt("PlayerDamage", 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
