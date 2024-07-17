using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

	public QuizData CurrentQuizData { get; private set; }

	private const string fileName = "quizData";

	/// <summary>
	/// Initializes the quiz data by attempting to fetch from the API or loading from local storage.
	/// </summary>
	public void Initialize (System.Action onTaskCompleted)
	{
		StartCoroutine(FetchFromAPI(onTaskCompleted));
	}

	/// <summary>
	/// Coroutine that initializes the quiz data by attempting to fetch from the API.
	/// </summary>
	private IEnumerator FetchFromAPI (System.Action onTaskCompleted)
	{
		yield return StartCoroutine(apiManager.FetchQuizData(OnFetchComplete));
		onTaskCompleted?.Invoke();
	}

	/// <summary>
	/// Callback function for when the API fetch is complete.
	/// </summary>
	/// <param name="newData">The fetched quiz data.</param>
	private void OnFetchComplete (QuizData newData)
	{
		if (newData != null && newData.questions.Count > 0)
		{
			CurrentQuizData = newData;
			AssignLocalIDs(CurrentQuizData);
			SaveQuestionData(newData, fileName);
		}

		LoadLocalQuizData();
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
	public QuizData LoadLocalQuizData ()
	{
		CurrentQuizData = GetSaveData();

		if (CurrentQuizData == null || CurrentQuizData.questions.Count == 0)
		{
			QuizData defaultData = new QuizData { questions = new List<QuizQuestion>(defaultQuestions) };
			SaveQuestionData(defaultData, fileName);
			CurrentQuizData = GetSaveData();
		}

		return CurrentQuizData;
	}

	private void SaveQuestionData (QuizData newQuizData, string fileName)
	{
		SaveLoadSystem.SaveData(newQuizData, fileName);
	}

	private QuizData GetSaveData ()
	{
		return SaveLoadSystem.LoadData<QuizData>(fileName);
	}

}
