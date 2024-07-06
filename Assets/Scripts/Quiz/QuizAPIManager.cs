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
		UnityWebRequest request = UnityWebRequest.Get(apiUrl);
		yield return request.SendWebRequest();

		// Check for errors immediately after the request completes
		if (request.result == UnityWebRequest.Result.ConnectionError ||
			request.result == UnityWebRequest.Result.DataProcessingError ||
			request.result == UnityWebRequest.Result.ProtocolError)
		{
			//Debug.Log($"Error fetching quiz data: {request.error}");
			callback?.Invoke(null);
			yield break;
		}

		// Uncomment the following line to debug the received JSON
		//Debug.Log("Received JSON: " + request.downloadHandler.text);
		try
		{
			QuizData quizData = JsonUtility.FromJson<QuizData>(request.downloadHandler.text);
			callback?.Invoke(quizData);
		}
		catch (System.Exception e)
		{
			Debug.LogWarning("Error parsing quiz data: " + e.Message);
		}
	}
}
