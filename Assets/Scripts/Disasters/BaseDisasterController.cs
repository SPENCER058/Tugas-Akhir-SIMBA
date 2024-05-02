using UnityEngine;

public class BaseDisasterController : MonoBehaviour
{
	[SerializeField] private GameObject model;

	public void Activate ()
	{
		model.SetActive(true);
	}

	public void Deactivate ()
	{
		model.SetActive (false);
	}

	public virtual void ChangePosition(Vector3 position)
	{
		transform.position = position;
	}

	public virtual void ChangeDisasterLevel (float value)
	{

	}

}
