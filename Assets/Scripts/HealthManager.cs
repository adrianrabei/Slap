using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Stat healthPlayer, healthEnemy;
    public Stat HealthPlayer => healthPlayer;

    public Stat HealthEnemy => healthEnemy;

    void Start()
    {
        int playerHealth = PlayerPrefs.GetInt("PlayerHealth", 100);
        int enemyHealth = PlayerPrefs.GetInt("EnemyHealth", 100);

        healthPlayer.Initialize(playerHealth, playerHealth);
        healthEnemy.Initialize(enemyHealth, enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
