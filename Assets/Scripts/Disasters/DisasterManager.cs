using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DisasterManager : MonoBehaviour
{

	[SerializeField] private ARScanUISwitch arUISwitch;
	[SerializeField] private ARSimulationPanelController simulationPanelController;
	[SerializeField] private ARInfoPanelController infoPanelController;

	[SerializeField] private ARTrackerListener trackerListener;
	[SerializeField] private List<DisasterKeyIndex> disasterList;

	private Dictionary<string, BaseDisasterController> disasterDictionary;
	private Queue<string> trackedDisasters;

	private void Awake ()
	{
		disasterDictionary = new Dictionary<string, BaseDisasterController> ();
		trackedDisasters = new Queue<string>();

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

	public void HandleAddedImage (string keyName, Vector3 position)
	{
		BaseDisasterController ctrl = disasterDictionary[keyName];

		if (trackedDisasters.Count != 0 && trackedDisasters.Peek() == keyName)
		{
			ctrl.ChangePosition(position);
		}

		if (!trackedDisasters.Contains(keyName))
		{
			trackedDisasters.Enqueue(keyName);

			if (trackedDisasters.Count == 1)
			{
				SetGeneralInfo(keyName);
				ctrl.Activate();
				ctrl.ChangePosition(position);
				UpdateARUI();
			}

		}
	}

	public void HandleRemovedImage (string keyName)
	{
		if (trackedDisasters.Contains(keyName))
		{
			disasterDictionary[keyName].Deactivate();
			trackedDisasters.Dequeue();

			if (trackedDisasters.Count != 0)
			{
				BaseDisasterController ctrl = disasterDictionary[trackedDisasters.Peek()];
				SetGeneralInfo(keyName);
				ctrl.Activate();
			}

			UpdateARUI();
		}
	}

	private void UpdateARUI()
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

	private void SetGeneralInfo (string keyname)
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

}

[System.Serializable]
public class DisasterKeyIndex
{
	public string key;

    public string DisasterKey { get => key; }

	public BaseDisasterController controller;

    public BaseDisasterController Controller { get => controller; }
}
