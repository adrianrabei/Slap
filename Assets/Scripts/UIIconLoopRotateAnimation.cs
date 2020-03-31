using DG.Tweening;
using UnityEngine;

namespace Ramen_Repair.Scripts.UI
{
	public class UIIconLoopRotateAnimation : MonoBehaviour
	{
		[SerializeField] private RectTransform rectTransform;
		[SerializeField] private Transform iconTransform;
		[SerializeField] private float angleRotation;

		[SerializeField] private Ease easeScale = Ease.InBounce;
		[SerializeField] private Ease easeRotation = Ease.InBounce;

		private void OnEnable()
		{
			//rectTransform.localScale = Vector3.one;
			

			//rectTransform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1f).SetEase(easeScale, 0.95f).SetLoops(-1, LoopType.Yoyo);
			iconTransform.DOShakeRotation(1.3f, new Vector3(0, 0, angleRotation)).SetEase(easeRotation, 0f).SetLoops(-1, LoopType.Yoyo);
		}
	}
}