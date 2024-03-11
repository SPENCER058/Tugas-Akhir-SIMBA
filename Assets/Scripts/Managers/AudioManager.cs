using UnityEngine;

namespace SIMBA.Managers
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioSource _bgmAudioSource;
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
