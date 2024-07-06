using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages quiz data, including fetching from an API, loading from local storage, and providing default questions.
/// </summary>
public class QuizDataManager : MonoBehaviour
{
	/// <summary>
	/// Reference to the QuizAPIManager for fetching data from the API.
	/// </summary>
	public QuizAPIManager apiManager;

	/// <summary>
	/// List of default questions to be used if no data is found or fetched.
	/// </summary>
	public List<QuizQuestion> defaultQuestions;

	private QuizData quizData;
	private const string fileName = "quizData";

	/// <summary>
	/// Initializes the quiz data by attempting to fetch from the API or loading from local storage.
	/// </summary>
	public void Initialize (System.Action onTaskCompleted)
	{
		StartCoroutine(InitializeQuizData(onTaskCompleted));
	}

	/// <summary>
	/// Coroutine that initializes the quiz data by checking for internet connectivity.
	/// </summary>
	private IEnumerator InitializeQuizData (System.Action onTaskCompleted)
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			//Debug.Log("Internet Reachable");
			yield return StartCoroutine(apiManager.FetchQuizData(OnFetchComplete));
		}
		else
		{
			//Debug.Log("Internet Not Reachable");
			LoadQuizData();
		}

		onTaskCompleted?.Invoke();
	}

	/// <summary>
	/// Callback function for when the API fetch is complete.
	/// </summary>
	/// <param name="newData">The fetched quiz data.</param>
	private void OnFetchComplete (QuizData newData)
	{

		//Debug.Log($"OnFetchComplete called with data: {newData}");

		if (newData != null && newData.questions.Count > 0)
		{
			SetQuizData(newData);
			AssignLocalIDs(quizData);
			SaveLoadSystem.SaveData(newData, fileName);
		}
		else
		{
			//Debug.LogWarning("Quiz data is null or has no questions. Loading existing quiz data.");
			LoadQuizData();
		}
	}

	/// <summary>
	/// Assigns unique local IDs to the fetched quiz questions.
	/// </summary>
	/// <param name="quizData">The fetched quiz data.</param>
	private void AssignLocalIDs (QuizData quizData)
	{
		for (int i = 0; i < quizData.questions.Count; i++)
		{
			quizData.questions[i].id = i + 1; // Assign local IDs starting from 1
		}
	}

	/// <summary>
	/// Loads quiz data from local storage or uses default questions if no data is found.
	/// </summary>
	/// <returns>The loaded or default quiz data.</returns>
	public QuizData LoadQuizData ()
	{
		// First, try to load the quiz data if it's not already loaded
		if (quizData == null)
		{
			SetQuizData(SaveLoadSystem.LoadData<QuizData>(fileName));
		}

		// If still null or empty, use default questions
		if (quizData == null || quizData.questions.Count == 0)
		{
			SetQuizData(new QuizData { questions = new List<QuizQuestion>(defaultQuestions) });
			SaveLoadSystem.SaveData(quizData, fileName);
		}

		// Return the loaded or default quiz data
		return quizData;
	}

	private void SetQuizData (QuizData newData)
	{
		quizData = newData;
	}
}