using UnityEngine;
using UnityEngine.Audio;

public class AudioPrefSave : MonoBehaviour
{
	[Header("Save Settings")]
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private string savePrefKey;
	[SerializeField] private string audioParameterName;

	bool isOn;

	private void Awake ()
	{
		isOn = PlayerPrefs.GetInt(savePrefKey, 1) == 1;
	}

	private void Start ()
	{
		audioMixer.SetFloat(audioParameterName, isOn ? 0 : -80);
	}

	public bool IsOn ()
	{
		return isOn;
	}

	public void SetValue (bool value)
	{
		isOn = value;
		audioMixer.SetFloat(audioParameterName, value ? 0 : -80);
		PlayerPrefs.SetInt(savePrefKey, value ? 1 : 0);
		PlayerPrefs.Save();
	}
}
