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

		if (isOn)
		{
			_button.image.sprite = _enabledIcon;
		}
		else
		{
			_button.image.sprite = _disabledIcon;
		}
	}

	public void OnClickButton ()
	{
		if (isOn)
		{
			isOn = false;
			_button.image.sprite = _disabledIcon;
			OnMute.Invoke();
		}
		else
		{
			isOn = true;
			_button.image.sprite = _enabledIcon;
			OnPlay.Invoke();
		}

		_prefSave.SetValue(isOn);
	}

}
