using UnityEngine;

public class UIVideoPlayButton : MonoBehaviour
{
	[Header("Play/Pause Button")]
	[SerializeField] private UnityEngine.UI.Button _playPauseButton;
	[SerializeField] private Sprite _playIcon;
	[SerializeField] private Sprite _pauseIcon;
	[SerializeField] private Sprite _replayIcon;

	public delegate void OnPlayPauseButtonClicked ();

	public event OnPlayPauseButtonClicked OnPlayRequested;
	public event OnPlayPauseButtonClicked OnReplayRequested;
	public event OnPlayPauseButtonClicked OnPauseRequested;

	private bool isPlayingVideo = false;

	public void Initialize (bool value)
	{
		ChangeUIState(value);
	}

	public void SubscribeEvents ()
	{
		_playPauseButton.onClick.AddListener(HandleButtonClicked);
	}

	public void ManualPlay ()
	{
		isPlayingVideo = false;
		HandleButtonClicked();
	}

	public void ManualPause ()
	{
		isPlayingVideo = true;
		HandleButtonClicked();
	}

	public void HandleButtonClicked ()
	{
		bool newState;

		if (_playPauseButton.image.sprite == _replayIcon)
		{
			OnReplayRequested?.Invoke();
		}
		else
		{
			if (isPlayingVideo)
			{
				newState = false;
				OnPauseRequested?.Invoke();
			}
			else
			{
				newState = true;
				OnPlayRequested?.Invoke();
			}

			ChangeUIState(newState);
		}

	}

	public void ChangeUIState (bool value)
	{
		isPlayingVideo = value;
		_playPauseButton.image.sprite = isPlayingVideo ? _pauseIcon : _playIcon;
	}

	public void ChangeIconToReplay ()
	{
		_playPauseButton.image.sprite = _replayIcon;
	}

	public void CleanUp ()
	{
		_playPauseButton.onClick.RemoveAllListeners();
	}
}
