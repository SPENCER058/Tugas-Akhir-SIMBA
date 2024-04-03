using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
	[SerializeField] private VideoPlayer _videoPlayer;

	[SerializeField] private UIVideoPlayButton _videoPlayButton;
	[SerializeField] private VideoTimeLineController _videoTimeLine;
	[SerializeField] private UIVideoVolumeController _videoVolumeController;

	[SerializeField] private RenderTexture _videoRenderTexture;

	private bool isPlayingVideo = false;

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
		_videoPlayButton.OnPlayRequested += Play;
		_videoPlayButton.OnPauseRequested += Pause;

		_videoVolumeController.OnVolumeChanged += (value) => _videoPlayer.SetDirectAudioVolume(0, value);

		_videoTimeLine.OnTimeLineChanged += (value) => _videoPlayer.time = value;
	}

	private void Update ()
	{
		_videoTimeLine.UpdateCurrentTime((float)_videoPlayer.time);
	}

	private void OnDisable ()
	{

		_videoRenderTexture.Release();
		_videoPlayer.clip = null;

		_videoPlayButton.OnPlayRequested -= Play;
		_videoPlayButton.OnPauseRequested -= Pause;
		_videoPlayButton.CleanUp();

		_videoVolumeController.OnVolumeChanged -= (value) => _videoPlayer.SetDirectAudioVolume(0, value);
		_videoVolumeController.CleanUp();

		_videoTimeLine.OnTimeLineChanged -= (value) => _videoPlayer.time = value;
		_videoTimeLine.CleanUp();
	}

	public void SetAndPlayClip (VideoClip clip)
	{
		_videoPlayer.clip = clip;
		SetupComponent();
		_videoPlayButton.ManualPlay();
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

}
