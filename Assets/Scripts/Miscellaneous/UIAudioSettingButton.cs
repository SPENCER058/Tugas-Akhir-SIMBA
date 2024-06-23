using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIAudioSettingButton : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private AudioPrefSave _prefSave;

	[Header("UI")]
	[SerializeField] private Button _button;
	[SerializeField] private Sprite _enabledIcon;
	[SerializeField] private Sprite _disabledIcon;

	[Header("Events")]
	public UnityEvent OnPlay;
	public UnityEvent OnMute;

	private bool isOn;

	private void Awake ()
	{
		isOn = _prefSave.IsOn();
		UpdateButton();
	}

	public void OnClickButton ()
	{
		isOn = !isOn;
		UpdateButton();

		if (isOn)
		{
			OnPlay.Invoke();
		}
		else
		{
			OnMute.Invoke();
		}

		_prefSave.SetValue(isOn);
	}

	private void UpdateButton ()
	{
		_button.image.sprite = isOn ? _enabledIcon : _disabledIcon;
	}

}
