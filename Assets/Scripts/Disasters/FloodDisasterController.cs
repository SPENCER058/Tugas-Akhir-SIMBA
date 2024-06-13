using UnityEngine;

public class FloodDisasterController : BaseDisasterController
{
	public override void ActivateChildComponents (GameObject model)
	{
		base.ActivateChildComponents(model);

		// Activate particle system
		var particleSystems = model.GetComponentsInChildren<ParticleSystem>();
		foreach (var ps in particleSystems)
		{
			ps.Play();
		}
	}

	public override void DeactivateChildComponents (GameObject model)
	{
		base.DeactivateChildComponents(model);

		// Deactivate particle system
		var particleSystems = model.GetComponentsInChildren<ParticleSystem>();
		foreach (var ps in particleSystems)
		{
			ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}
}
