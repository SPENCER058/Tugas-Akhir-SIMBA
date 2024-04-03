using UnityEngine;

public class VideoTimeLineController : MonoBehaviour
{
	[Header("Time Line")]
	[SerializeField] private UnityEngine.UI.Slider _currentTimeLineSlider;
	[SerializeField] private UnityEngine.UI.Slider _interactiveTimeLineSlider;
	[SerializeField] private TMPro.TextMeshProUGUI _currentDurationText;
	[SerializeField] private TMPro.TextMeshProUGUI _maxDurationText;

	public System.Action<float> OnTimeLineChanged;

	public void Initialize (float maxTime)
	{
		_maxDurationText.text = FormatTime(maxTime);
		_currentTimeLineSlider.maxValue = maxTime;

		_interactiveTimeLineSlider.maxValue = maxTime;
		_interactiveTimeLineSlider.value = 0;
	}

	public void SubscribeEvents ()
	{
		_interactiveTimeLineSlider.onValueChanged.AddListener((value) => OnTimeLineChanged?.Invoke(value));
	}

	public void ResetInteractiveSlider ()
	{
		_interactiveTimeLineSlider.value = 0;
	}

	public void UpdateCurrentTime (float time)
	{
		_currentDurationText.text = FormatTime(time);

		_currentTimeLineSlider.value = time;
	}


	private string FormatTime (double timeInSeconds)
	{
		System.TimeSpan time = System.TimeSpan.FromSeconds(timeInSeconds);
		return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
	}

	public void CleanUp () 
	{
		_interactiveTimeLineSlider.onValueChanged.RemoveAllListeners();
	}

	public bool IsReachMax ()
	{
		float n = _currentTimeLineSlider.maxValue - 1;

		Debug.Log("Current time value " + _currentTimeLineSlider.value);
		Debug.Log("Max time value " + _currentTimeLineSlider.maxValue +" "+ n);

		return _currentTimeLineSlider.value <= _currentTimeLineSlider.maxValue - 1;
	}
}
