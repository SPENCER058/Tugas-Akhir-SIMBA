using System.Collections.Generic;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{

	[SerializeField] private ARScanUISwitch arUISwitch;

	[SerializeField] private ARTrackerListener trackerListener;
	[SerializeField] private List<DisasterKeyIndex> disasterList;

	private Dictionary<string, BaseDisasterController> disasterDictionary;
	private HashSet<string> trackedDisasters;

	private void Awake ()
	{
		disasterDictionary = new Dictionary<string, BaseDisasterController> ();
		trackedDisasters = new HashSet<string>();

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
		Debug.Log("Detected = " + keyName);

		BaseDisasterController ctrl = disasterDictionary[keyName];
		ctrl.ChangePosition (position);
		ctrl.Activate ();

		if (!trackedDisasters.Contains(keyName)) { trackedDisasters.Add(keyName); }

		UpdateARUI ();

	}

	public void HandleRemovedImage (string keyName)
	{
		if (trackedDisasters.Contains(keyName))
		{
			disasterDictionary[keyName].Deactivate();
			trackedDisasters.Remove(keyName);
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

}

[System.Serializable]
public class DisasterKeyIndex
{
	public string key;

    public string DisasterKey { get => key; }

	public BaseDisasterController controller;

    public BaseDisasterController Controller { get => controller; }
}
