using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
	[Header("Scene Name")]
	[SerializeField] private string _mainMenu = "MainMenu";
	[SerializeField] private string _videoPlayer = "VideoPlayer";
	[SerializeField] private string _aRScan = "ARScan";

	public void LoadScene (SceneName sceneName)
	{
		string scene;

		switch (sceneName)
		{
			case SceneName.MainMenu:
				scene = _mainMenu;
				break;
			case SceneName.VideoPlayer:
				scene = _videoPlayer;
				break;
			case SceneName.ARScan:
				scene = _aRScan;
				break;
			default:
				scene = _mainMenu;
				break;
		}

		LoadScene(scene);
	}

	private void LoadScene(string sceneName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

	public void LoadMainMenu () => LoadScene(_mainMenu);
	public void LoadAR () => LoadScene(_aRScan);
	public void LoadVideo () => LoadScene(_videoPlayer);
}

public enum SceneName
{
	MainMenu,
	VideoPlayer,
	ARScan
}
