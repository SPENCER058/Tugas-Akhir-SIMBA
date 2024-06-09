using System;
using UnityEngine;
using Vuforia;

/// <summary>
/// A custom script to bridge Default Observer Event Handler and Object Manager
/// also to notify others script which one marker is found based on marker database
/// 
/// This script is made because event in  Default Observer Event Handler is non overrideable
/// 
/// This will always reference to Image Target Behaviour in runtime
/// Beware when change the Image Target Behaviour image target
/// </summary>

public class ARTrackerListener : MonoBehaviour
{

	[SerializeField] private ImageTargetBehaviour m_ImageTarget;

	public Action<string> notifyImageFound;
	public Action<string> notifyImageLost;

	private void Start ()
	{
		// ImageTargetBehaviour is used to automaticly get image name
		// This will automatically get the script from gameobject if null
		// For better use, consider put this script in same gameobject as Image Target Behaviour
		if (m_ImageTarget == null)
		{
			m_ImageTarget = GetComponent<ImageTargetBehaviour>();
		}

	}

	// Call this in DefaultObserverEventHandler from the inspector
	// Function is used if image is found, so assign this in On Target Found
	// This will invoke event with target name to ObjectManager
	public void HandleImageFound ()
	{
		if (m_ImageTarget != null)
		{
			notifyImageFound?.Invoke(m_ImageTarget.TargetName);
		}
		else
		{
			//Debug.LogWarning("WARNING! Function called but Image Target Behaviour is null. Abandon event!");
		}
	}
	// Call this in DefaultObserverEventHandler from the inspector
	// Function is used if image is lost, so assign this in On Target Lost
	// This will invoke event with target name to ObjectManager
	public void HandleImageLost ()
	{
		if (m_ImageTarget != null)
		{
			notifyImageLost?.Invoke(m_ImageTarget.TargetName);
		}
		else
		{
			//Debug.LogWarning("WARNING! Function called but Image Target Behaviour is null. Abandon event!");
		}
	}

}
