using UnityEngine;

public class ARInfoPanelController : MonoBehaviour
{
	[SerializeField] private UnityEngine.UI.Image disasterHeaderImage;
	[SerializeField] private UnityEngine.UI.Image disasterImage;
	[SerializeField] private TMPro.TextMeshProUGUI disasterExplanationText;

	public void SetDisasterDetails (Sprite headerImage, Sprite disasterImageSprite, string explanation)
	{
		disasterHeaderImage.sprite = headerImage;
		disasterImage.sprite = disasterImageSprite;
		disasterExplanationText.text = explanation;
	}


}
