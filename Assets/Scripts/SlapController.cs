using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SlapController : MonoBehaviour
{
    [SerializeField] private GameObject enemy, powerBtn, healthBtn, hitIndicator;
    [SerializeField] private HitPower hitPower;
    [SerializeField] private CoinSystem coinSystem;

    private Animator playerAnimator;
    private Animator enemyAnimator;
    public bool playerTurn;
    public bool canclick;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        enemyAnimator = enemy.GetComponent<Animator>();
        playerTurn = true;
        canclick = true;

        powerBtn.SetActive(true);
        healthBtn.SetActive(true);
    }


    void Update()
    {
        if (playerTurn)
        {
            PrepareToSlap("player");
            enemyAnimator.SetBool("preparing", false);

            if (Input.GetMouseButtonDown(0) && !CheckUIClick.IsPointerOverUIObject() && canclick)
            {
                PlayerSlapping();
                hitPower.sequence.Pause();
                hitPower.CheckHitPowerSection();
                powerBtn.SetActive(false);
                healthBtn.SetActive(false);
            }
        }
        else
        {
            PrepareToSlap("enemy");
            playerAnimator.SetBool("preparing", false);
        }
    }

    public void PrepareToSlap(string character)
    {
        if (character == "player")
        {
            playerAnimator.SetBool("preparing", true);
        }
        else enemyAnimator.SetBool("preparing", true);
    }


    public void PlayerSlapping()
    {
        playerAnimator.SetBool("preparing", false);

        Hit("player");
        StartCoroutine(DisableIndicator(false, 1));
        canclick = false;
    }

    public IEnumerator EnemySlapping()
    {
        yield return new WaitForSeconds(2);
        enemyAnimator.SetBool("preparing", false);

        Hit("enemy");
        StartCoroutine(DisableIndicator(true, 1));
    }

    public void Hit(string character)
    {
        if (character == "player")
        {
            playerAnimator.SetTrigger("slap");
        }
        else enemyAnimator.SetTrigger("slap");
    }

    public void GetHit(string character)
    {
        if (character == "player")
        {
            coinSystem.PlayerGetDamage();
            SoundManager.Instance.PlaySound("slap");
            GameManager.Instance.Vibration();
            hitPower.sequence.Play();
            playerAnimator.SetTrigger("knocked");
            playerAnimator.SetTrigger("standUp");
            playerAnimator.SetTrigger("isUp");
            playerTurn = true;
        }
        else
        {
            StartCoroutine(coinSystem.EnemyGetDamage());
            SoundManager.Instance.PlaySound("slap");
            GameManager.Instance.Vibration();
            enemyAnimator.SetTrigger("knocked");
            enemyAnimator.SetTrigger("standUp");
            enemyAnimator.SetTrigger("isUp");
            playerTurn = false;
        }
    }

    public void StayDown(string character)
    {
        if (character == "player")
        {
            if (coinSystem.playerHealth <= 0)
            {
                playerAnimator.enabled = false;
            }
        }
        else if (character == "enemy")
        {
            if (coinSystem.enemyHealth <= 0)
            {
                enemyAnimator.enabled = false;
                Debug.Log("smarti");
            }
        }
    }

    IEnumerator CanClick()
    {
        yield return new WaitForSeconds(0.25f);
        canclick = true;
    }

    IEnumerator DisableIndicator(bool activeState, float time)
    {
        yield return new WaitForSeconds(time);
        hitIndicator.SetActive(activeState);
        hitPower.wasClickedHit = false;
        canclick = false;
    }
}