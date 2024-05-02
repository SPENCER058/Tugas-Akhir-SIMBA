using UnityEngine;
using UnityEngine.UI;

public class UIVideoVolumeController : MonoBehaviour
{
	[Header("Volume Setting")]
	[SerializeField] private Sprite _volumeOnIcon;
	[SerializeField] private Sprite _volumeOffIcon;
	[SerializeField] private Button _volumeButton;
	[SerializeField] private Slider _volumeSlider;

	public System.Action<float> OnVolumeChanged;

	private float currentVolume = 0;
	private float savedVolume = 0;
	private bool isMuted = false;

	public void Initialize (float videoPlayerVolume)
	{
		_volumeSlider.value = videoPlayerVolume;
		currentVolume = videoPlayerVolume;

		if (currentVolume == 0)
		{
			isMuted = true;
		}
	}

	public void SubscribeEvents ()
	{
		_volumeButton.onClick.AddListener(HandleMuteButton);
		_volumeSlider.onValueChanged.AddListener(HandleVolumeChanged);
	}

	private void HandleVolumeChanged (float value)
	{
		currentVolume = value;

		if (currentVolume == 0)
		{
			isMuted = true; 
		}
		else
		{
			isMuted = false;
		}

		UpdateUI();

		OnVolumeChanged?.Invoke(value);
	}

	private void HandleMuteButton ()
	{

		if (isMuted)
		{
			currentVolume = savedVolume;
			isMuted = false;
		}
		else
		{
			savedVolume = currentVolume;
			isMuted = true;
		}

		UpdateUI();
	}

	private void UpdateUI ()
	{
		if (isMuted)
		{
			_volumeButton.image.sprite = _volumeOffIcon;
			_volumeSlider.value = 0;
		}
		else
		{
			_volumeButton.image.sprite = _volumeOnIcon;
			_volumeSlider.value = currentVolume;
		}
	}

	public void CleanUp ()
	{
		_volumeButton.onClick.RemoveAllListeners();
		_volumeSlider.onValueChanged.RemoveAllListeners();
	}
}
