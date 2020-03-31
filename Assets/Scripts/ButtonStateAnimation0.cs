using DG.Tweening;
using UnityEngine;

namespace Ramen_Repair.Scripts.UI
{
	public class ButtonStateAnimation0 : MonoBehaviour
	{
		[SerializeField] private float initScale;
		
		private Tween tween;
		
		private void OnEnable()
		{
			transform.localScale = Vector3.one * initScale;
			tween.Kill(true);
			
			PlayAnimation();
		}

		private void PlayAnimation()
		{
			transform.DOScale(Vector3.one, 0.1f).OnComplete(() => { tween = transform.DOScale(Vector3.one * 1.05f, 0.5f).SetEase(Ease.OutBounce, 1.1f).SetLoops(-1, LoopType.Yoyo); });
		}
	}
}