using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the quiz UI, including displaying questions, handling answer selections,
/// updating the progress bar, and showing results.
/// </summary>
public class QuizUIManager : MonoBehaviour
{
	/// <summary>
	/// Text component to display the current question.
	/// </summary>
	[SerializeField] private TMP_Text questionText;

	/// <summary>
	/// Array of buttons for answer choices.
	/// </summary>
	[SerializeField] private Button[] answerButtons;

	/// <summary>
	/// Image component to represent the progress bar.
	/// </summary>
	[SerializeField] private Image progressBar;

	/// <summary>
	/// UI component to display the quiz results.
	/// </summary>
	[SerializeField] private QuizResultUIController resultUI;

	/// <summary>
	/// Action invoked when an answer button is clicked.
	/// </summary>
	public System.Action<int> OnAnswerButtonClicked;

	private Color defaultColor;

	/// <summary>
	/// Stores the default color of the answer buttons.
	/// </summary>
	private void Awake ()
	{
		if (answerButtons.Length > 0)
		{
			defaultColor = answerButtons[0].GetComponent<Image>().color;
		}
	}

	/// <summary>
	/// Adds listeners to the answer buttons when the object is enabled.
	/// </summary>
	private void OnEnable ()
	{
		for (int i = 0; i < answerButtons.Length; i++)
		{
			int index = i; // Capture the current index for the lambda
			answerButtons[i].onClick.AddListener(() => OnAnswerButtonClicked?.Invoke(index));
		}
	}

	/// <summary>
	/// Removes all listeners from the answer buttons when the object is disabled.
	/// </summary>
	private void OnDisable ()
	{
		foreach (var button in answerButtons)
		{
			button.onClick.RemoveAllListeners();
		}
	}

	/// <summary>
	/// Sets the text of the question display.
	/// </summary>
	/// <param name="question">The question text to display.</param>
	public void SetQuestionText (string question)
	{
		questionText.text = question;
	}

	/// <summary>
	/// Sets the text of the answer choice buttons.
	/// </summary>
	/// <param name="answers">Array of answer choice texts.</param>
	public void SetChoiceButtonsText (string[] answers)
	{
		for (int i = 0; i < answerButtons.Length; i++)
		{
			answerButtons[i].GetComponentInChildren<TMP_Text>().text = answers[i];
		}
	}

	/// <summary>
	/// Highlights the selected answer and the correct answer.
	/// </summary>
	/// <param name="selectedIndex">Index of the selected answer.</param>
	/// <param name="isCorrect">Whether the selected answer is correct.</param>
	/// <param name="correctOption">The correct answer text.</param>
	public void HighlightAnswer (int selectedIndex, bool isCorrect, string correctOption)
	{
		for (int i = 0; i < answerButtons.Length; i++)
		{
			if (i == selectedIndex)
			{
				SetButtonColor(i, isCorrect ? Color.green : Color.red);
			}
			else if (answerButtons[i].GetComponentInChildren<TMP_Text>().text == correctOption)
			{
				SetButtonColor(i, Color.green);
			}
		}
	}

	/// <summary>
	/// Sets the color of a specified answer button.
	/// </summary>
	/// <param name="index">Index of the answer button.</param>
	/// <param name="newColor">New color to apply.</param>
	public void SetButtonColor (int index, Color newColor)
	{
		answerButtons[index].GetComponent<Image>().color = newColor;
	}

	/// <summary>
	/// Resets the color of all answer buttons to their default color.
	/// </summary>
	public void ResetButtonColors ()
	{
		foreach (var button in answerButtons)
		{
			button.GetComponent<Image>().color = defaultColor;
		}
	}

	/// <summary>
	/// Updates the progress bar fill amount.
	/// </summary>
	/// <param name="value">Progress value between 0 and 1.</param>
	public void UpdateProgressBar (float value)
	{
		progressBar.fillAmount = value;
	}

	/// <summary>
	/// Shows or hides the result panel.
	/// </summary>
	/// <param name="show">Whether to show the result panel.</param>
	public void ShowInfoPanel (bool show)
	{
		resultUI.ShowResults(show);
	}

	/// <summary>
	/// Sets the result data in the result UI.
	/// </summary>
	/// <param name="correct">Number of correct answers.</param>
	/// <param name="total">Total number of questions.</param>
	public void SetResultData (int correct, int total)
	{
		resultUI.SetResults(correct, total);
	}
}
