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
	[SerializeField] private QuizAPIManager apiManager;


	/// <summary>
	/// List of default questions to be used if no data is found or fetched.
	/// </summary>
	[SerializeField] private string defaultVersion;

	/// <summary>
	/// List of default questions to be used if no data is found or fetched.
	/// </summary>
	[SerializeField] private List<QuizQuestion> defaultQuestions;

	public QuizData CurrentQuizData { get; private set; }
	public QuizData NewestQuizData { get; private set; }

	private const string fileName = "quizData";

	/// <summary>
	/// Initializes the quiz data by attempting to fetch from the API or loading from local storage.
	/// </summary>
	public void Initialize (System.Action<bool> onTaskCompleted)
	{
		LoadFromSave();
		StartCoroutine(FetchFromAPI(onTaskCompleted));
	}

	public void UpdateData ()
	{
		SaveQuestionData(NewestQuizData, fileName);
		CurrentQuizData = GetSaveData();
	}


	/// <summary>
	/// Coroutine that initializes the quiz data by attempting to fetch from the API.
	/// </summary>
	private IEnumerator FetchFromAPI (System.Action<bool> onTaskCompleted)
	{
		yield return StartCoroutine(apiManager.FetchQuizData(OnFetchComplete));

		if (CurrentQuizData == null) { LoadFromSave(); }

		bool hasUpdates = CompareSaveData(CurrentQuizData, NewestQuizData);

		onTaskCompleted?.Invoke(hasUpdates);
	}

	/// <summary>
	/// Callback function for when the API fetch is complete.
	/// </summary>
	/// <param name="newData">The fetched quiz data.</param>
	private void OnFetchComplete (QuizData newData)
	{

		if (newData != null && newData.questions.Count > 0)
		{
			NewestQuizData = newData;
			AssignLocalIDs(NewestQuizData);
		}

		//LoadLocalQuizData();
	}

	/// <summary>
	/// Loads quiz data from local storage or uses default questions if no data is found.
	/// </summary>
	/// <returns>The loaded or default quiz data.</returns>
	public QuizData LoadFromSave ()
	{
		QuizData quizFromSave = GetSaveData();

		if (quizFromSave == null || quizFromSave.questions.Count == 0)
		{
			QuizData defaultData = new QuizData { version = defaultVersion, questions = new List<QuizQuestion>(defaultQuestions) };
			SaveQuestionData(defaultData, fileName);
		}

		CurrentQuizData = quizFromSave;

		return CurrentQuizData;
	}

	/// <summary>
	/// Compares the saved quiz data with the newly fetched data.
	/// </summary>
	/// <param name="newData">The new quiz data to compare.</param>
	/// <returns>True if the data is different, otherwise false.</returns>
	private bool CompareSaveData (QuizData currentData, QuizData newData)
	{

		if (currentData == null || newData == null)
		{
			return false;
		}

		// Implement your comparison logic here. For simplicity, we're just checking if the question count is different.
		return currentData.version != newData.version;
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

	private void SaveQuestionData (QuizData newQuizData, string fileName)
	{
		SaveLoadSystem.SaveData(newQuizData, fileName);
	}

	private QuizData GetSaveData ()
	{
		return SaveLoadSystem.LoadData<QuizData>(fileName);
	}
}
