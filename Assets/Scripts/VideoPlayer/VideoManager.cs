using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
	[SerializeField] private VideoPlayer _videoPlayer;
	[SerializeField] private UIVideoPlayButton _videoPlayButton;
	[SerializeField] private VideoTimeLineController _videoTimeLine;
	[SerializeField] private UIVideoVolumeController _videoVolumeController;
	[SerializeField] private RenderTexture _videoRenderTexture;

	private bool isPlayingVideo = false;
	private bool hasVideoEnded = false;

	private void Awake ()
	{
		_videoRenderTexture.Release();
		_videoPlayer.clip = null;

		_videoPlayButton.SubscribeEvents();
		_videoVolumeController.SubscribeEvents();
		_videoTimeLine.SubscribeEvents();
	}

	private void SetupComponent ()
	{
		isPlayingVideo = _videoPlayer.isPlaying;

		_videoTimeLine.Initialize((float)_videoPlayer.clip.length);
		_videoPlayButton.Initialize(isPlayingVideo);
		_videoVolumeController.Initialize(_videoPlayer.GetDirectAudioVolume(0));
	}

	private void OnEnable ()
	{
		_videoPlayer.loopPointReached += VideoEndReached;

		_videoPlayButton.OnPlayRequested += Play;
		_videoPlayButton.OnReplayRequested += Replay;
		_videoPlayButton.OnPauseRequested += Pause;

		_videoVolumeController.OnVolumeChanged += (value) => _videoPlayer.SetDirectAudioVolume(0, value);

		_videoTimeLine.OnTimeLineChanged += HandleTimeLineChanged;
	}

	private void Update ()
	{
		_videoTimeLine.UpdateCurrentTime((float)_videoPlayer.time);
	}

	private void OnDisable ()
	{
		_videoPlayer.loopPointReached -= VideoEndReached;

		_videoRenderTexture.Release();
		_videoPlayer.clip = null;

		_videoPlayButton.OnPlayRequested -= Play;
		_videoPlayButton.OnReplayRequested -= Replay;
		_videoPlayButton.OnPauseRequested -= Pause;
		_videoPlayButton.CleanUp();

		_videoVolumeController.OnVolumeChanged -= (value) => _videoPlayer.SetDirectAudioVolume(0, value);
		_videoVolumeController.CleanUp();

		_videoTimeLine.OnTimeLineChanged -= HandleTimeLineChanged;
		_videoTimeLine.CleanUp();
	}

	public void SetAndPlayClip (VideoClip clip)
	{
		_videoPlayer.clip = clip;
		hasVideoEnded = false;
		SetupComponent();
		_videoPlayButton.ManualPlay();
	}

	private void HandleTimeLineChanged (float value)
	{
		_videoPlayer.time = value;
		_videoPlayButton.ChangeUIState(_videoPlayer.isPlaying);

	}

	public void Play ()
	{
		isPlayingVideo = true;
		_videoPlayer.Play();
	}

	public void Pause ()
	{
		isPlayingVideo = false;
		_videoPlayer.Pause();
	}

	public void Replay ()
	{
		_videoPlayer.Stop();
		_videoTimeLine.ResetInteractiveSlider();
		_videoPlayButton.ChangeUIState(true);
		_videoPlayButton.ManualPlay();
		hasVideoEnded = false; // Reset the flag when replaying
	}

	private void VideoEndReached (VideoPlayer vp)
	{

		_videoPlayButton.ManualPause();

		StartCoroutine(IsReachEndAgain());

		if (!hasVideoEnded)
		{
			_videoPlayButton.ChangeIconToReplay();
			hasVideoEnded = true;

		}
	}

	private IEnumerator IsReachEndAgain ()
	{
		yield return new WaitForSeconds(0.1f);
		bool isSliderReachMax = _videoTimeLine.IsReachMax();

		if (isSliderReachMax)
		{
			hasVideoEnded = false;
		}
	}
}
