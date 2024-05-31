using UnityEngine;
using UnityEngine.UI;

public class DisasterUIManager : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private ARScanUISwitch arUISwitch;
	[SerializeField] private ARSimulationPanelController simulationPanelController;
	[SerializeField] private ARInfoPanelController infoPanelController;
	[SerializeField] private Slider simulationSlider;

	public System.Action<float> notifySliderChange;

	#region SIMULATION SLIDER
	public void OnSliderValueChange (float sliderValue) => notifySliderChange?.Invoke(sliderValue);

	public void ResetSliderValue () { simulationSlider.value = 0; }
	#endregion

	#region GENERAL AR UI
	public void IdleStateUI () => arUISwitch.SetIdleUI();

	public void SimulationStateUI () => arUISwitch.SetSimulationUI();
	#endregion

	#region INFO & DETAILS
	public void ChangeSimulationInfo (float index, float strength)
	{
		simulationPanelController.ChangeStrengthText(strength.ToString());
		simulationPanelController.ChangeRiskUI((int)index);
	}

	public void UpdateGeneralDisasterInfo (SO_ARDisasterProfiles profiles)
	{
		simulationPanelController.SetDisasterInfo(
				profiles.GetDisasterIcon,
				profiles.GetDisasterName,
				profiles.GetDisasterLocation,
				profiles.GetDisasterScaleUnit
			);

		infoPanelController.SetDisasterDetails(
				profiles.GetPanelTitleImage,
				profiles.GetHeaderImage,
				profiles.GetContentText
			);
	}
	#endregion

	public void HandleDeactivation ()
	{
		simulationPanelController.ChangeStrengthText(0.ToString());
		ResetSliderValue();
	}
}
