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
	private string triggerToRemove;

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
	public virtual void HandleFound ()
	{
		isActive = true;
		ModelActivation();
		animator.Play("Low Disaster");
		ChangeDisasterLevel(0);
	}

	public virtual void HandleLost ()
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

	public virtual void ChangeDisasterLevel (float value)
	{
		switch (value)
		{
			case 0:
				ChangeAnimation(lowTrigger);
				break;
			case 1:
				ChangeAnimation(mediumTrigger);
				break;
			case 2:
				ChangeAnimation(highTrigger);
				break;
			default:
				ChangeAnimation(lowTrigger);
				break;
		}
	}

	private void ChangeAnimation (string triggerName)
	{
		if (triggerToRemove != null) 
		{
			animator.ResetTrigger(triggerName);
		}
		
		animator.SetTrigger(triggerName);
		triggerToRemove = triggerName;
	}

	#endregion
}
