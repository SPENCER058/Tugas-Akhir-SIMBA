using UnityEngine;

public class BaseDisasterController : MonoBehaviour
{
	[Header("Position Anchor")]
	[SerializeField] Transform positionAnchor;

	[Header("Models & Animations")]
	[SerializeField] private GameObject lowModel;
	[SerializeField] private GameObject mediumModel;
	[SerializeField] private GameObject highModel;

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

		// Get your target's right vector in world space
		var right = positionAnchor.right;

		// Ensure that your object's up vector equals the world up vector
		thisObj.up = Vector3.up;

		// Align your object's right vector with the image target's right vector
		// projected down onto the global XZ plane => erasing its Y component
		thisObj.right = Vector3.ProjectOnPlane(right, Vector3.up);
	}
	#endregion

	#region AR EVENT HANDLER
	public virtual void HandleFound ()
	{
		isActive = true;
		ChangeDisasterLevel(0);
	}

	public virtual void HandleLost ()
	{
		isActive = false;
		DeactivateAll();
	}
	#endregion

	#region ANIMATIONS
	public virtual void ChangeDisasterLevel (float value)
	{
		switch (value)
		{
			case 0:
				ActivateModel(lowModel);
				break;
			case 1:
				ActivateModel(mediumModel);
				break;
			case 2:
				ActivateModel(highModel);
				break;
			default:
				ActivateModel(lowModel);
				break;
		}
	}

	private void ActivateModel (GameObject model)
	{
		DeactivateAll();
		if (model != null)
		{
			model.SetActive(true);
			ActivateChildComponents(model);
		}
	}

	private void DeactivateAll ()
	{
		DeactivateModel(lowModel);
		DeactivateModel(mediumModel);
		DeactivateModel(highModel);
	}

	private void DeactivateModel (GameObject model)
	{
		if (model != null)
		{
			model.SetActive(false);
			DeactivateChildComponents(model);
		}
	}

	public virtual void ActivateChildComponents (GameObject model)
	{
		// Activate animator
		var animator = model.GetComponentInChildren<Animator>();
		if (animator != null)
		{
			animator.Play(0, -1, 0);
		}
	}

	public virtual void DeactivateChildComponents (GameObject model)
	{
		// Deactivate animator
		var animator = model.GetComponentInChildren<Animator>();
		if (animator != null)
		{
			animator.StopPlayback();
		}
	}
	#endregion
}
