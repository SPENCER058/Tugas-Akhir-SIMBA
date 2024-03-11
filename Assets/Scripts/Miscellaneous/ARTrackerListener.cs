using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackerListener : MonoBehaviour
{
	[SerializeField] private ARTrackedImageManager m_trackedImageManager;

	private List<string> activeTrackingList = new List<string>();

	public System.Action<string, Vector3> OnImageAdded;
	public System.Action<string> OnImageRemoved;

	#region UNITY_LIFECYCLE

	public void Initialize ()
	{
		if (m_trackedImageManager == null)
		{
			m_trackedImageManager = GetComponent<ARTrackedImageManager>();
		}

		m_trackedImageManager.trackedImagesChanged += OnImageChanged;
	}

	public void CleanUp ()
	{
		m_trackedImageManager.trackedImagesChanged -= OnImageChanged;
	}

	#endregion

	#region IMAGE_TRACKING

	private void OnImageChanged (ARTrackedImagesChangedEventArgs args)
	{
		foreach (ARTrackedImage trackedImage in args.added)
		{
			if (IsTracking(trackedImage) && !IsOnList(trackedImage)) TrackImage(trackedImage);
		}

		foreach (ARTrackedImage trackedImage in args.updated)
		{
			if (IsTracking(trackedImage) && !IsOnList(trackedImage))
			{
				TrackImage(trackedImage);
			}
			else if (!IsTracking(trackedImage) && IsOnList(trackedImage))
			{
				UnTrackImage(trackedImage);
			}
		}
	}

	private void TrackImage (ARTrackedImage trackedImage)
	{
		//Debug.Log($"Image added or continue tracking: {GetRefName(trackedImage)}");
		OnImageAdded?.Invoke(GetRefName(trackedImage), trackedImage.transform.position);
		activeTrackingList.Add(GetRefName(trackedImage));
	}

	private void UnTrackImage (ARTrackedImage trackedImage)
	{
		//Debug.Log($"Image removed or lost tracking: {GetRefName(trackedImage)}");
		activeTrackingList.Remove(GetRefName(trackedImage));
		OnImageRemoved?.Invoke(GetRefName(trackedImage));
	}

	#endregion

	#region GETTERS

	private string GetRefName (ARTrackedImage refImage) => refImage.referenceImage.name;

	private bool IsTracking (ARTrackedImage trackedImage) => trackedImage.trackingState == TrackingState.Tracking;
	
	private bool IsOnList (ARTrackedImage trackedImage) => activeTrackingList.Contains(GetRefName(trackedImage));

	#endregion

}
