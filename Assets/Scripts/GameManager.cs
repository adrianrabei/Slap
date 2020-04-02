using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject game;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject fail;
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private Text levelTextUI, vsTextUI;
    [SerializeField] private Button nextLevelButton, retryLevelButton;
    [SerializeField] private GameObject[] defaultEnemies, bonusEnemies;

    public static bool bonusLevel, wasReseted;

    protected override void Awake()
    {
        base.Awake();
        int checkBonus = PlayerPrefs.GetInt(StringKeys.level, 1);
        if ((checkBonus % 4) == 0)
        {
            bonusLevel = true;
        }
        else
        {
            bonusLevel = false;
        }
    }

    void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        retryLevelButton.onClick.AddListener(RetryLevel);

        PlayerPrefs.GetInt(StringKeys.enemyIndex, 0);
        

        if (bonusLevel)
        {
            vsTextUI.fontSize = 60;
            vsTextUI.text = "BONUS";
            levelTextUI.text = "LEVEL";
        }
        else
        {
            levelTextUI.text = "LEVEL " + PlayerPrefs.GetInt(StringKeys.level, 1).ToString();
            vsTextUI.text = "VS";
            vsTextUI.fontSize = 100;
        }
    }

    void FixedUpdate()
    {
        if (coinSystem.playerWin)
        {
            coinSystem.playerWin = false;
            Win();
        }
        else if (coinSystem.playerLoose)
        {
            coinSystem.playerLoose = false;
            Fail();
        }
    }

    private void NextLevel()
    {
        int currLevel = 1+ PlayerPrefs.GetInt(StringKeys.level, 1);
        PlayerPrefs.SetInt(StringKeys.level, currLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RetryLevel()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Vibration()
    {
        if(PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            Handheld.Vibrate();
        }
    }

    public void Win()
    {
        int currentLevel = PlayerPrefs.GetInt(StringKeys.level, 1);
        game.SetActive(false);
        win.SetActive(true);
        SoundManager.Instance.PlaySound("win");
        int winCoins = coinSystem.totalCoins + 25 * currentLevel;
        PlayerPrefs.GetInt(StringKeys.totalCoins, winCoins);
    }

    public void Fail()
    {
        game.SetActive(false);
        fail.SetActive(true);
        SoundManager.Instance.PlaySound("fail");
    }
}