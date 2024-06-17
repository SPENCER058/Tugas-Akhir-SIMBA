using UnityEngine;

namespace SIMBA.Managers
{
	public class SFXAudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _sfxAudioSource;

		[Header("SFX Audio")]
		[SerializeField] private AudioClip _sfxEnableClip;
		[SerializeField] private AudioClip _sfxDisableClip;

		public void PlayEnableSFX ()
		{
			_sfxAudioSource.PlayOneShot(_sfxEnableClip);
		}

		public void PlayDisableSFX ()
		{
			_sfxAudioSource.PlayOneShot(_sfxDisableClip);
		}

	}
}
