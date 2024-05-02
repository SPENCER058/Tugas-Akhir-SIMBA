using UnityEngine;

public class Quit : MonoBehaviour
{
	public void QuitApp ()
	{
		#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
		#else
			AndroidQuitAppUtility.MoveAppToBack();
		#endif
	}
}
