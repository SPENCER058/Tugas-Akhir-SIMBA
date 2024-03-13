using System.Collections.Generic;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{
	[SerializeField] private ARTrackerListener trackerListener;
	[SerializeField] private List<DisasterKeyIndex> disasterList;

	private Dictionary<string, BaseDisasterController> disasterDictionary;

	private void Awake ()
	{
		disasterDictionary = new Dictionary<string, BaseDisasterController> ();

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
		ctrl.ChangePosition (position);
		ctrl.Activate ();

	}

	public void HandleRemovedImage (string keyName)
	{
		disasterDictionary[keyName].Deactivate ();
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
