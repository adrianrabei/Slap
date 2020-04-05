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
    [SerializeField] private Text levelTextUI;
    [SerializeField] private Button nextLevelButton, retryLevelButton;

    protected override void Awake()
    {
        base.Awake();
        int curLVL = PlayerPrefs.GetInt(StringKeys.level, 1);
        int curScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (curLVL != curScene)
        {
            SceneManager.LoadScene(curLVL - 1);
        }
    }

    void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        retryLevelButton.onClick.AddListener(RetryLevel);

        Debug.Log(PlayerPrefs.GetInt(StringKeys.level));

        levelTextUI.text = "LEVEL " + PlayerPrefs.GetInt(StringKeys.level, 1).ToString();
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
        int currLevel = 1 + PlayerPrefs.GetInt(StringKeys.level, 1);
        PlayerPrefs.SetInt(StringKeys.level, currLevel);
        if (PlayerPrefs.GetInt(StringKeys.level, 1) % 50 == 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void RetryLevel()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Vibration()
    {
        if (PlayerPrefs.GetInt("vibro", 1) == 1)
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
        int winCoins = coinSystem.totalCoins + 50 * currentLevel;
        PlayerPrefs.SetInt(StringKeys.totalCoins, winCoins);
    }

    public void Fail()
    {
        game.SetActive(false);
        fail.SetActive(true);
        SoundManager.Instance.PlaySound("fail");
    }
}