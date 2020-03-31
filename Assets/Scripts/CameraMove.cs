using DG.Tweening;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionOne, cameraPositionTwo;
    [SerializeField] private SlapController player;

    void FixedUpdate()
    {
        if (player.playerTurn)
        {
            Camera.main.transform.DOMove(cameraPositionTwo.transform.position, 2f).SetDelay(1.5f);
            Camera.main.transform.DORotateQuaternion(cameraPositionTwo.transform.rotation, 2f).SetDelay(1.5f);
        }
        else
        {
            Camera.main.transform.DOMove(cameraPositionOne.transform.position, 2f).SetDelay(1.5f);
            Camera.main.transform.DORotateQuaternion(cameraPositionOne.transform.rotation, 2f).SetDelay(1.5f);
        }
    }
}