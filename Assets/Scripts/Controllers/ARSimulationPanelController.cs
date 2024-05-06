using UnityEngine;

public class ARSimulationPanelController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] private UnityEngine.UI.Image disasterIconImage;
	[SerializeField] private TMPro.TextMeshProUGUI disasterNameText;
	[SerializeField] private TMPro.TextMeshProUGUI disasterLocationText;
	[SerializeField] private TMPro.TextMeshProUGUI disasterStrengthText;
	[SerializeField] private TMPro.TextMeshProUGUI disasterStrengthScaleText;
	[SerializeField] private UnityEngine.UI.Image disasterRiskImage;

	[Header("RiskImages")]
	[SerializeField] private Sprite lowRiskImage;
	[SerializeField] private Sprite mediumRiskImage;
	[SerializeField] private Sprite highRiskImage;

	public void SetDisasterInfo (Sprite icon, string name, string location, string strengthScale)
	{
		disasterIconImage.sprite = icon;
		disasterNameText.text = name;
		disasterLocationText.text = location;
		disasterStrengthScaleText.text = strengthScale;
	}

	public void ChangeStrengthText(string strength) { disasterStrengthText.text = strength; }

	public void ChangeRiskUI(int index)
	{
		switch (index)
		{
			case 0:
				ChangeDisasterRiskImage(lowRiskImage);
				break;
			case 1:
				ChangeDisasterRiskImage(mediumRiskImage);
				break;
			case 2:
				ChangeDisasterRiskImage(highRiskImage);
				break;
		}
	}

	private void ChangeDisasterRiskImage (Sprite image)
	{
		disasterRiskImage.sprite = image;
	}

}
