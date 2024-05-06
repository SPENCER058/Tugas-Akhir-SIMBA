using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisasterManager : MonoBehaviour
{

	[SerializeField] private ARScanUISwitch arUISwitch;
	[SerializeField] private ARSimulationPanelController simulationPanelController;
	[SerializeField] private ARInfoPanelController infoPanelController;

	[SerializeField] private Slider simulationSlider;

	[SerializeField] private ARTrackerListener trackerListener;
	[SerializeField] private List<DisasterKeyIndex> disasterList;

	private Dictionary<string, BaseDisasterController> disasterDictionary;
	private List<string> trackedDisasters;

	private BaseDisasterController currentDisasterController;

	private void Awake ()
	{
		disasterDictionary = new Dictionary<string, BaseDisasterController> ();
		trackedDisasters = new List<string>();

		foreach (DisasterKeyIndex disaster in disasterList)
		{
			disasterDictionary.Add (disaster.DisasterKey, disaster.Controller);
		}
	}

	private void OnEnable ()
	{
		trackerListener.OnImageAdded += HandleAddedImage;
		trackerListener.OnImageRemoved += HandleRemovedImage;
	}

	private void OnDisable ()
	{
		trackerListener.OnImageAdded -= HandleAddedImage;
		trackerListener.OnImageRemoved -= HandleRemovedImage;
	}

	private void HandleAddedImage (string keyName, Vector3 position)
	{

		BaseDisasterController ctrl = disasterDictionary[keyName];

		if (!trackedDisasters.Contains(keyName))
		{
			ctrl.ChangePosition(position);
			trackedDisasters.Add(keyName);

			if (trackedDisasters.Count == 1)
			{
				UpdateGeneralDisasterInfo(keyName);
				currentDisasterController = ctrl;
				ChangeSimulationInfo(0);
				ctrl.Activate();
			}

		}

		SwitchARUI();
	}

	private void HandleRemovedImage (string disasterKeyName)
	{
		if (trackedDisasters.Contains(disasterKeyName))
		{
			if (disasterKeyName == trackedDisasters[0])
			{

				DeactivateCurrentDisaster();

				if (trackedDisasters.Count != 0)
				{
					ActivateNextDisaster();
				}

				SwitchARUI();
			} 
			else
			{
				trackedDisasters.Remove(disasterKeyName);
			}
		}
	}

	public void OnSliderValueChange (float sliderValue)
	{
		if (currentDisasterController != null)
		{
			currentDisasterController.ChangeDisasterLevel(sliderValue);
			ChangeSimulationInfo(sliderValue);
		}
	}

	private void ResetSliderValue ()
	{
		simulationSlider.value = 0;
	}

	private void SwitchARUI()
	{
		if (trackedDisasters.Count == 0)
		{
			arUISwitch.SetIdleUI();
		}
		else
		{
			arUISwitch.SetSimulationUI();
		}
	}

	private void DeactivateCurrentDisaster ()
	{
		simulationPanelController.ChangeStrengthText(0.ToString());
		currentDisasterController.Deactivate();
		ResetSliderValue();
		currentDisasterController = null;
		trackedDisasters.RemoveAt(0);

	}

	private void ActivateNextDisaster ()
	{
		string newKeyName = trackedDisasters[0];
		BaseDisasterController ctrl = disasterDictionary[newKeyName];
		ctrl.ChangeDisasterLevel(0);
		UpdateGeneralDisasterInfo(newKeyName);
		ChangeSimulationInfo(0);
		currentDisasterController = ctrl;
		ctrl.Activate();
	}

	private void UpdateGeneralDisasterInfo (string keyname)
	{
		BaseDisasterController controller = disasterDictionary[keyname];
		SO_ARDisasterProfiles profiles = controller.GetProfiles;

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

	private void ChangeSimulationInfo (float index)
	{
		if (currentDisasterController != null)
		{
			SO_ARDisasterProfiles profiles = currentDisasterController.GetProfiles;
			float strength = profiles.GetDisasterStreght((int)index);
			simulationPanelController.ChangeStrengthText(strength.ToString());
			simulationPanelController.ChangeRiskUI((int)index);
		}
	}

}

[System.Serializable]
public class DisasterKeyIndex
{
	public string key;

    public string DisasterKey { get => key; }

	public BaseDisasterController controller;

    public BaseDisasterController Controller { get => controller; }
}
