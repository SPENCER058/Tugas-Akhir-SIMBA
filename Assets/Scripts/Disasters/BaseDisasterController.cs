using UnityEngine;

public class BaseDisasterController : MonoBehaviour
{
	[SerializeField] private GameObject model;

	[Header("Animations & Trigger Name")]
	[SerializeField] private Animator animator;
	[SerializeField] private string lowTrigger = "Low";
	[SerializeField] private string mediumTrigger = "Medium";
	[SerializeField] private string highTrigger = "High";

	public void Activate ()
	{
		model.SetActive(true);
		ChangeDisasterLevel(0);
	}

	public void Deactivate ()
	{
		model.SetActive (false);
	}

	public virtual void ChangePosition(Vector3 position)
	{
		transform.position = position;
	}

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

	public void TriggerAnimation(string triggerName)
	{
		animator.SetTrigger(triggerName);
	}

}
