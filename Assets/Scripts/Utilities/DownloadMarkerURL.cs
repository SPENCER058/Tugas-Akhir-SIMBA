using UnityEngine;

public class DownloadMarkerURL : MonoBehaviour
{
    public string Url;

    public void Open()
    {
        Application.OpenURL(Url);
    }


}
