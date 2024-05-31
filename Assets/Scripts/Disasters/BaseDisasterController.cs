using UnityEngine;

public class BaseDisasterController : MonoBehaviour
{
	[Header("Model & Anchor")]
	[SerializeField] private GameObject model;
	[SerializeField] Transform positionAnchor;

	[Header("Animations & Trigger Name")]
	[SerializeField] private Animator animator;
	[SerializeField] private string lowTrigger = "Low";
	[SerializeField] private string mediumTrigger = "Medium";
	[SerializeField] private string highTrigger = "High";

	[SerializeField] private SO_ARDisasterProfiles profile;
	public SO_ARDisasterProfiles GetProfiles { get { return profile; } }

	private bool isActive = false;

	#region UPDATE POSITION
	private void Update ()
	{
		if (isActive)
		{
			AlignObject();
		}
	}

	private void AlignObject ()
	{
		Transform thisObj = gameObject.transform;

		thisObj.position = positionAnchor.position;

		// Get your targets right vector in world space
		var right = positionAnchor.right;

		// If not anyway the case ensure that your objects up vector equals the world up vector
		thisObj.up = Vector3.up;

		// Align your objects right vector with the image target's right vector
		// projected down onto the global XZ plane => erasing its Y component
		thisObj.right = Vector3.ProjectOnPlane(right, Vector3.up);
	}
	#endregion

	#region AR EVENT HANDLER
	public void HandleFound ()
	{
		ChangeDisasterLevel(0);
		isActive = true;
		ModelActivation();
	}

	public void HandleLost ()
	{
		isActive = false;
		ModelActivation();
	}

	private void ModelActivation ()
	{
		model.SetActive(isActive);
	}

	#endregion

	#region ANIMATIONS

	public void ChangeDisasterLevel (float value)
	{
		switch (value)
		{
			case 0:
				TriggerAnimation(lowTrigger);
				break;
			case 1:
				TriggerAnimation(mediumTrigger);
				break;
			case 2:
				TriggerAnimation(highTrigger);
				break;
			default:
				TriggerAnimation(lowTrigger);
				break;
		}
	}

	public void TriggerAnimation (string triggerName)
	{
		animator.SetTrigger(triggerName);
	}

	#endregion
}
