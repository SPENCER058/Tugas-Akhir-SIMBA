using UnityEngine;

public class FloodDisasterController : BaseDisasterController
{
	[Header("Particles System")]
	[SerializeField] private ParticleSystem _particleSystem;
	[SerializeField] private float _lowSpeed;
	[SerializeField] private float _mediumSpeed;
	[SerializeField] private float _highSpeed;

	public override void HandleFound ()
	{
		base.HandleFound();
		_particleSystem.Play();
	}

	public override void HandleLost ()
	{
		base.HandleLost();
		_particleSystem.Stop();
	}

	public override void ChangeDisasterLevel (float value)
	{
		base.ChangeDisasterLevel(value);
		ChangeParticlesSpeed(value);
	}

	private void ChangeParticlesSpeed (float value)
	{
		var main = _particleSystem.main;
		switch (value)
		{
			case 0:
				main.simulationSpeed = _lowSpeed;
				break;
			case 1:
				main.simulationSpeed = _mediumSpeed;
				break;
			case 2:
				main.simulationSpeed = _highSpeed;
				break;
			default:
				main.simulationSpeed = _lowSpeed;
				break;
		}
	}
}
