using UnityEngine;

public class AndroidQuitAppUtility
{
	public static void MoveAppToBack ()
	{
		AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call<bool>("moveTaskToBack", true);
	}
}