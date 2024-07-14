using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Manages fetching quiz data from a remote API.
/// </summary>
public class QuizAPIManager : MonoBehaviour
{

	private const string apiUrl = "http://127.0.0.1:8000/api/quiz";

	/// <summary>
	/// Fetches quiz data from the API and invokes the callback with the data.
	/// </summary>
	/// <param name="callback">Callback to invoke with the fetched quiz data.</param>
	/// <returns>An IEnumerator for use with StartCoroutine.</returns>
	public IEnumerator FetchQuizData (System.Action<QuizData> callback)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			callback?.Invoke(null);
			yield break;
		}

		using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError ||
				request.result == UnityWebRequest.Result.DataProcessingError ||
				request.result == UnityWebRequest.Result.ProtocolError)
			{

/*				// Specific handling for Android network issues
				if (request.result == UnityWebRequest.Result.ConnectionError)
				{
					//Debug.Log("\nCheck if your Android device has internet access and the correct permissions.");
				}*/

				callback?.Invoke(null);
				yield break;
			}


			try
			{
				QuizData quizData = JsonUtility.FromJson<QuizData>(request.downloadHandler.text);
				callback?.Invoke(quizData);
			}
			catch (System.Exception e)
			{
				Debug.Log("Error parsing quiz data: " + e.Message);
				callback?.Invoke(null);
			}
		}
	}
}
