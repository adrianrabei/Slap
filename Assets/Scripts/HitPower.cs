using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitPower : MonoBehaviour
{
    [SerializeField] private GameObject startPos, endPos;
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private SlapController slapController;

    private Vector3 targetPos;
    public Sequence sequence;


    public bool wasClickedHit;

    private void Start()
    {
        sequence = DOTween.Sequence();
        this.transform.position = startPos.transform.position;
        targetPos = endPos.transform.position;
        MoveFunction();
    }

    private void MoveFunction()
    {
        int currentLevel = PlayerPrefs.GetInt(StringKeys.level, 1);
        float duration = 1 - 0.01f*currentLevel;
        if (duration < 0.25f)
        {
            duration = 0.25f;
        }
        sequence.Append(transform.DOMove(targetPos, duration)
            .SetEase(Ease.Linear));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }

    public float CheckHitPowerSection()
    {
        var endPoint = endPos.transform.position.x;
        var startPoint = startPos.transform.position.x;
        var totalPath = endPoint - startPoint;
        var currentPosition = transform.position.x;
        var pathOneCell = totalPath / 9;

        if (currentPosition >= startPoint && currentPosition < startPoint + pathOneCell ||
            currentPosition <= endPoint && currentPosition > endPoint - pathOneCell)
        {
            return 0.5f;
        }
        else if (currentPosition >= startPoint + pathOneCell && currentPosition < startPoint + pathOneCell * 2 ||
                 currentPosition > endPoint - pathOneCell * 2 && currentPosition <= endPoint - pathOneCell)
        {
            return 0.625f;
        }
        else if (currentPosition >= startPoint + pathOneCell * 2 && currentPosition < startPoint + pathOneCell * 3 ||
                 currentPosition > endPoint - pathOneCell * 3 && currentPosition <= endPoint - pathOneCell * 2)
        {
            return 0.75f;
        }
        else if (currentPosition >= startPoint + pathOneCell * 3 && currentPosition < startPoint + pathOneCell * 4 ||
                 currentPosition > endPoint - pathOneCell * 4 && currentPosition <= endPoint - pathOneCell * 3)
        {
            return 0.85f;
        }
        else
        {
            return 1f;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}