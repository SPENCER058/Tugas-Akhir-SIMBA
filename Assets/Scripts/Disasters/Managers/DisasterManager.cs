using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DisasterManager : MonoBehaviour
{

	[Header("Managers")]
	[SerializeField] private DisasterUIManager disasterUIManager;

	[Header("Registration")]
	[SerializeField] private List<ARTrackerListener> trackerListeners = new List<ARTrackerListener>();
	[SerializeField] private List<DisasterKeyIndex> disasterControllerRegist;

	// Database
	private Dictionary<string, BaseDisasterController> disasterControllerDictionary;
	private List<string> trackedMarker;

	private BaseDisasterController currentDisasterController;
	// End Database

	#region UNITY LIFECYCLE
	private void Awake ()
	{
		disasterControllerDictionary = new Dictionary<string, BaseDisasterController>();
		trackedMarker = new List<string>();

		foreach (DisasterKeyIndex disaster in disasterControllerRegist)
		{
			disasterControllerDictionary.Add(disaster.DisasterKey, disaster.Controller);
		}

		disasterUIManager.Initialize ();
	}

	private void OnEnable ()
	{
		disasterUIManager.notifySliderChange += ChangeSimulation;

		if (trackerListeners.Count > 0)
		{
			foreach (ARTrackerListener listener in trackerListeners)
			{
				listener.notifyImageFound += ImageTrackedFound;
				listener.notifyImageLost += ImageTrackedLost;
			}
		}

		if (disasterControllerRegist != null)
		{
			foreach (DisasterKeyIndex index in disasterControllerRegist)
			{
				Debug.Log("Regist"+index.DisasterKey);

				if (!disasterControllerDictionary.ContainsKey(index.DisasterKey))
				{
					disasterControllerDictionary.Add(index.DisasterKey, index.Controller);
				}
			}
		}
	}

	private void OnDisable ()
	{
		if (trackerListeners.Count > 0)
		{
			foreach (ARTrackerListener listener in trackerListeners)
			{
				listener.notifyImageFound -= ImageTrackedFound;
				listener.notifyImageLost -= ImageTrackedLost;
			}
		}

		disasterUIManager.Release();
		disasterUIManager.notifySliderChange -= ChangeSimulation;
	}
	#endregion

	#region AR HANDLER
	private void ImageTrackedFound (string targetName)
	{
		//Debug.Log("On Target Found:" + targetName);

		// Search in Dictionary
		BaseDisasterController ctrl = disasterControllerDictionary[targetName];

		if (!trackedMarker.Contains(targetName))
		{
			trackedMarker.Add(targetName);

			if (trackedMarker.Count == 1)
			{
				UpdateGeneralDisasterInfo(targetName);
				currentDisasterController = ctrl;
				ChangeSimulationInfo(0);
				ctrl.HandleFound();
			}
		}

		// Update UI
		SwitchARUI();

		// Print Tracked Marker
		DebugTracked();
	}

	private void ImageTrackedLost (string targetName)
	{
		//Debug.Log("On Target Lost:" + targetName);

		if (trackedMarker.Contains(targetName))
		{
			if (targetName == trackedMarker[0])
			{

				DeactivateCurrentDisaster();

				if (trackedMarker.Count != 0)
				{
					ActivateNextDisaster();
				}

				SwitchARUI();
			}
			else
			{
				trackedMarker.Remove(targetName);
			}
		}

		// Print Tracked Marker
		DebugTracked();
	}
	#endregion

	#region DEBUG
	private void DebugTracked ()
	{
		string debugString = "Tracked List: ";
		if (trackedMarker != null)
		{
			foreach (string tracked in trackedMarker)
			{
				debugString += "\n" + tracked;
			}
		}
		Debug.Log(debugString);
	}
	#endregion

	#region SIMULATIONS & INFO
	public void ChangeSimulation (float value)
	{
		if (currentDisasterController != null)
		{
			currentDisasterController.ChangeDisasterLevel(value);
			ChangeSimulationInfo(value);
		}
	}

	private void ChangeSimulationInfo (float index)
	{
		if (currentDisasterController != null)
		{
			SO_ARDisasterProfiles profiles = currentDisasterController.GetProfiles;
			float strength = profiles.GetDisasterStreght((int)index);

			disasterUIManager.ChangeSimulationInfo(index, strength);
		}
	}

	private void UpdateGeneralDisasterInfo (string keyname)
	{
		BaseDisasterController controller = disasterControllerDictionary[keyname];
		disasterUIManager.UpdateGeneralDisasterInfo(controller.GetProfiles);
	}
	#endregion

	#region GENERAL UI
	private void SwitchARUI ()
	{
		if (trackedMarker.Count == 0)
		{
			disasterUIManager.IdleStateUI();
		}
		else
		{
			disasterUIManager.SimulationStateUI();
		}
	}
	#endregion

	#region ACTIVATION
	private void DeactivateCurrentDisaster ()
	{
		currentDisasterController.HandleLost();
		currentDisasterController = null;

		disasterUIManager.HandleDeactivation();
		trackedMarker.RemoveAt(0);
	}

	private void ActivateNextDisaster ()
	{
		string newKeyName = trackedMarker[0];
		BaseDisasterController ctrl = disasterControllerDictionary[newKeyName];

		ctrl.ChangeDisasterLevel(0);

		UpdateGeneralDisasterInfo(newKeyName);
		ChangeSimulationInfo(0);

		currentDisasterController = ctrl;
		ctrl.HandleFound();
	}

	#endregion

}

[System.Serializable]
public class DisasterKeyIndex
{
	public ImageTargetBehaviour imageTarget;

	public string DisasterKey { get => imageTarget.TargetName; }

	public BaseDisasterController controller;

	public BaseDisasterController Controller { get => controller; }
}