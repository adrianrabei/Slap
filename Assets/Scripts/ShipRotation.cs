using DG.Tweening;
 using UnityEngine;
 
 namespace Ramen_Repair.Scripts.UI
 {
 	public class ShipRotation : MonoBehaviour
 	{
	    [SerializeField] private float angleRotation;
	    [SerializeField] private float durationTime;
	    [SerializeField] private Ease easeRotation = Ease.InBounce;
 
 		private void OnEnable()
 		{
	        transform.DOLocalRotate(new Vector3(transform.rotation.x + angleRotation, transform.rotation.y, 0), durationTime,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
 		}
 	}
 }