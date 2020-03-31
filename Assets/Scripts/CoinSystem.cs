using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] private Button buyHealthBtn, buyPowerBtn;

    [SerializeField] private Text totalCoinsText,
        healthPriceText,
        powerPriceText,
        bonusHealthText,
        bonusPowerText,
        playerPowerText,
        playerHealthText,
        enemyHealthText;

    [SerializeField] private Image unableHealth, unablePower;

    public int totalCoins,
        healthPrice,
        powerPrice,
        bonusHealthValue,
        bonusPowerValue,
        playerPower,
        playerHealth,
        enemyHealth,
        enemyPower,
        currentLevel,
        dmgPwr;

    [SerializeField] private HealthManager healthManager;
    [SerializeField] private HitPower hitPower;

    public bool playerWin, playerLoose;

    private void Awake()
    {
        Initialization();
        CheckHealthButton();
        CheckPowerButton();

        buyHealthBtn.onClick.AddListener(BuyHealth);
        buyPowerBtn.onClick.AddListener(BuyPower);

        playerWin = false;
        playerLoose = false;

        dmgPwr = 0;

        currentLevel = PlayerPrefs.GetInt(StringKeys.level, 1);
        enemyHealth = Random.Range(5 * currentLevel + 95, 10 * currentLevel + 101);
        healthManager.HealthEnemy.MyMaxValue = enemyHealth;
        healthManager.HealthEnemy.MyCurrentValue = enemyHealth;
        enemyHealthText.text = enemyHealth.ToString();
    }

    private void EnemyAttackPower()
    {
        int lowestHit = 6 * currentLevel + 17;
        int strongestHit = 9 * currentLevel + 21;
        enemyPower = Random.Range(lowestHit, strongestHit);
        dmgPwr = enemyPower;
        if (dmgPwr >= strongestHit - currentLevel * 2)
        {
            SoundManager.Instance.PlaySound("wow");
        }
    }

    private bool CheckHealthButton()
    {
        if (int.Parse(totalCoinsText.text) < int.Parse(healthPriceText.text))
        {
            unableHealth.GetComponent<Image>().color = Color.gray;
            unableHealth.GetComponent<Button>().enabled = false;
            return false;
        }

        return true;
    }

    private bool CheckPowerButton()
    {
        if (int.Parse(totalCoinsText.text) < int.Parse(powerPriceText.text))
        {
            unablePower.GetComponent<Image>().color = Color.gray;
            unablePower.GetComponent<Button>().enabled = false;
            return false;
        }

        return true;
    }

    public void PlayerGetDamage()
    {
        EnemyAttackPower();
        if (dmgPwr >= playerHealth)
        {
            playerHealth -= dmgPwr;
            playerHealthText.text = "0";
            healthManager.HealthPlayer.MyCurrentValue = 0;
            playerHealth = 0;
            playerLoose = true;
        }
        else
        {
            playerHealth -= dmgPwr;
            healthManager.HealthPlayer.MyCurrentValue = playerHealth;
            playerHealthText.text = playerHealth.ToString();
        }
    }

    public IEnumerator EnemyGetDamage()
    {
        var PlayerHit = (int) Math.Round(playerPower * hitPower.CheckHitPowerSection(), 0);
        if (hitPower.CheckHitPowerSection() == 1f)
        {
            SoundManager.Instance.PlaySound("wow");
        }

        if (PlayerHit >= enemyHealth)
        {
            enemyHealthText.text = "0";
            healthManager.HealthEnemy.MyCurrentValue = 0;
            enemyHealth = 0;
            playerWin = true;
            var nextLevel = 1 + PlayerPrefs.GetInt(StringKeys.level, 1);
            PlayerPrefs.SetInt(StringKeys.level, nextLevel);
        }
        else
        {
            playerPowerText.text = PlayerHit.ToString();
            enemyHealth -= PlayerHit;
            healthManager.HealthEnemy.MyCurrentValue = enemyHealth;
            enemyHealthText.text = enemyHealth.ToString();
            yield return new WaitForSeconds(5);
            playerPowerText.text = playerPower.ToString();
        }
    }

    private void Initialization()
    {
        totalCoins = PlayerPrefs.GetInt(StringKeys.totalCoins, 51);
        totalCoinsText.text = totalCoins.ToString();

        healthPrice = PlayerPrefs.GetInt(StringKeys.healthPrice, 25);
        healthPriceText.text = healthPrice.ToString();

        powerPrice = PlayerPrefs.GetInt(StringKeys.powerPrice, 25);
        powerPriceText.text = powerPrice.ToString();

        playerHealth = PlayerPrefs.GetInt(StringKeys.playerMaxHealth, 100);
        healthManager.HealthPlayer.MyMaxValue = playerHealth;
        healthManager.HealthPlayer.MyCurrentValue = playerHealth;

        playerHealthText.text = playerHealth.ToString();

        playerPower = PlayerPrefs.GetInt(StringKeys.playerMaxPower, 35);
        playerPowerText.text = playerPower.ToString();

        bonusHealthValue = PlayerPrefs.GetInt(StringKeys.bonusHealth, 5);
        bonusHealthText.text = "HEALTH(" + bonusHealthValue.ToString() + ")";

        bonusPowerValue = PlayerPrefs.GetInt(StringKeys.bonusPower, 10);
        bonusPowerText.text = "POWER(" + bonusPowerValue.ToString() + ")";
    }

    private void BuyHealth()
    {
        if (CheckHealthButton())
        {
            totalCoins -= healthPrice;
            PlayerPrefs.SetInt(StringKeys.totalCoins, totalCoins);
            playerHealth += bonusHealthValue;
            PlayerPrefs.SetInt(StringKeys.playerMaxHealth, playerHealth);
            bonusHealthValue += 1;
            PlayerPrefs.SetInt(StringKeys.bonusHealth, bonusHealthValue);
            healthPrice += 50;
            PlayerPrefs.SetInt(StringKeys.healthPrice, healthPrice);
            Initialization();
            CheckHealthButton();
        }
    }

    private void BuyPower()
    {
        if (CheckPowerButton())
        {
            totalCoins -= powerPrice;
            PlayerPrefs.SetInt(StringKeys.totalCoins, totalCoins);
            playerPower += bonusPowerValue;
            PlayerPrefs.SetInt(StringKeys.playerMaxPower, playerPower);
            bonusHealthValue += 1;
            PlayerPrefs.SetInt(StringKeys.bonusPower, bonusPowerValue);
            powerPrice += 50;
            PlayerPrefs.SetInt(StringKeys.powerPrice, powerPrice);
            Initialization();
            CheckPowerButton();
        }
    }
}