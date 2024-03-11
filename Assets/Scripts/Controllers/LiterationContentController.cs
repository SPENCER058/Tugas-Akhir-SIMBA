using UnityEngine;
using UnityEngine.UI;

public class LiterationContentController : MonoBehaviour
{
	[SerializeField] private Image _panelTitleImage;
	[SerializeField] private Image _headerImage;
	[SerializeField] private TMPro.TextMeshProUGUI _contentText;

	public void SetContent(SO_LiterationContent content)
	{
		_panelTitleImage.sprite = content.GetPanelTitleImage;
		_headerImage.sprite = content.GetHeaderImage;
		_contentText.text = content.GetContentText;
	}
}
