using UnityEngine;

public class AutoRotation : MonoBehaviour
{
	void Awake ()
	{
		Screen.orientation = ScreenOrientation.AutoRotation;
	}

}
