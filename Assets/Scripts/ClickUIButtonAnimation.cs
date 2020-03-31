using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ramen_Repair.Scripts.UI
{
	public class ClickUIButtonAnimation : MonoBehaviour
	{
		[SerializeField] private Transform buttonTransform;
		[SerializeField] private Button button;

		private void Awake()
		{
			button.onClick.AddListener(Click);

			if (buttonTransform == null)
			{
				buttonTransform = transform;
			}
		}

		private void Click()
		{
			buttonTransform.localScale = Vector3.one;
			buttonTransform.DOScale(Vector3.one * 1.1f, 0.1f).OnComplete(() => buttonTransform.DOScale(Vector3.one, 0.05f));
		}
	}
}