using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SlapController player;
    [SerializeField] private CoinSystem coinSystem;
    void Start()
    {
       
    }

    public void EnemySlap()
    {
        StartCoroutine(player.EnemySlapping());
    }

    public void PlayerGetHit()
    {
        player.GetHit("player");
    }

    public void StayDown()
    {
        player.StayDown("enemy");

    }
}
