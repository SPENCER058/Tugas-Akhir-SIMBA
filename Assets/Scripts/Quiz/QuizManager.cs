using UnityEngine;

/// <summary>
/// Manages the quiz flow including loading quiz data, handling user answers, and managing UI in general.
/// </summary>
public class QuizManager : MonoBehaviour
{
	/// <summary>
	/// Reference to the UI manager for the quiz.
	/// </summary>
	[SerializeField] private QuizUIManager quizUIManager;

	/// <summary>
	/// Reference to the data manager for the quiz.
	/// </summary>
	[SerializeField] private QuizDataManager quizdataManager;

	/// <summary>
	/// Reference to the UI for showing quiz results.
	/// </summary>
	[SerializeField] private QuizResultUIController quizResultUI;

	private QuizData quizData;
	private int currentQuestionIndex = 0;
	private int correctAnswers = 0;

	/// <summary>
	/// Subscribes to the restart event when the object is enabled.
	/// </summary>
	private void OnEnable ()
	{
		quizUIManager.OnAnswerButtonClicked += OnAnswerSelected;
		quizResultUI.OnRestartRequested += ResetQuiz;
	}

	/// <summary>
	/// Initializes the quiz by loading the data and resetting the quiz state.
	/// </summary>
	private void Start ()
	{
		quizUIManager.ButtonInteractable(false);

		quizdataManager.Initialize(() =>
		{
			LoadQuiz();
			ShowNextQuestion();
		});
	}

	/// <summary>
	/// Unsubscribes from the restart event when the object is disabled.
	/// </summary>
	private void OnDisable ()
	{
		quizUIManager.OnAnswerButtonClicked -= OnAnswerSelected;
		quizResultUI.OnRestartRequested -= ResetQuiz;
	}

	/// <summary>
	/// Loads quiz data from the data manager and resets the quiz.
	/// </summary>
	private void LoadQuiz ()
	{
		quizData = quizdataManager.LoadQuizData();
	}

	/// <summary>
	/// Resets the quiz state, including resetting the UI and progress.
	/// </summary>
	public void ResetQuiz ()
	{
		currentQuestionIndex = 0;
		correctAnswers = 0;

		quizUIManager.ResetButtonColors();
		quizUIManager.UpdateProgressBar(0);
		quizUIManager.ShowInfoPanel(false);

		ShowNextQuestion();
	}


	[SerializeField] private float nextQuestionDelay = 1.5f;

	/// <summary>
	/// Handles the user's answer selection, updates the score, and progresses to the next question.
	/// </summary>
	/// <param name="index">The index of the selected answer.</param>
	private void OnAnswerSelected (int index)
	{

		QuizQuestion question = quizData.questions[currentQuestionIndex];
		bool isCorrect = question.options[index] == question.correct_option;

		if (isCorrect)
		{
			correctAnswers++;
		}

		quizUIManager.HighlightAnswer(index, isCorrect, question.correct_option);

		currentQuestionIndex++;
		quizUIManager.UpdateProgressBar((float)currentQuestionIndex / quizData.questions.Count);


		quizUIManager.ButtonInteractable(false);
		// Show the next question after a short delay
		Invoke(nameof(ShowNextQuestion), nextQuestionDelay);
	}

	/// <summary>
	/// Displays the next question or shows the results if the quiz is complete.
	/// </summary>
	private void ShowNextQuestion ()
	{
		if (currentQuestionIndex >= quizData.questions.Count)
		{
			quizUIManager.SetResultData(correctAnswers, quizData.questions.Count);
			quizUIManager.ShowInfoPanel(true);
			return;
		}

		quizUIManager.ResetButtonColors();

		UpdateTextUI();

		quizUIManager.ButtonInteractable(true);
	}

	private void UpdateTextUI ()
	{
		// Set new or next question
		QuizQuestion newQuestion = quizData.questions[currentQuestionIndex];

		quizUIManager.SetQuestionText(newQuestion.question);
		quizUIManager.SetChoiceButtonsText(newQuestion.options);
	}
}
