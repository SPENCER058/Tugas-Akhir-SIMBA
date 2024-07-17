using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Manages the display of quiz results, including percentage, correct and incorrect answers.
/// </summary>
public class QuizResultUIController : MonoBehaviour
{
	/// <summary>
	/// Text component for displaying the percentage score.
	/// </summary>
	[SerializeField] private TMP_Text percentageText;

	/// <summary>
	/// Text component for displaying the number of correct answers.
	/// </summary>
	[SerializeField] private TMP_Text correctText;

	/// <summary>
	/// Text component for displaying the number of incorrect answers.
	/// </summary>
	[SerializeField] private TMP_Text incorrectText;

	/// <summary>
	/// Button for restarting the quiz.
	/// </summary>
	[SerializeField] private Button restartButton;

	/// <summary>
	/// Event invoked when the restart button is clicked.
	/// </summary>
	public System.Action OnRestartRequested;

	private void Awake ()
	{
		restartButton.onClick.AddListener(OnRestartButtonClicked);
	}

	/// <summary>
	/// Sets the results of the quiz.
	/// </summary>
	/// <param name="correct">The number of correct answers.</param>
	/// <param name="total">The total number of questions.</param>
	public void SetResults (int correct, int total)
	{
		int incorrect = total - correct;
		float percentage = ((float)correct / total) * 100f;

		percentageText.text = $"Nilai: {percentage:0.##}";
		correctText.text = $"Benar: {correct}";
		incorrectText.text = $"Salah: {incorrect}";
	}

	/// <summary>
	/// Shows or hides the result UI.
	/// </summary>
	/// <param name="show">If true, shows the result UI; otherwise, hides it.</param>
	public void ShowResults (bool show)
	{
		gameObject.SetActive(show);
	}

	/// <summary>
	/// Handles the restart button click event.
	/// </summary>
	private void OnRestartButtonClicked ()
	{
		OnRestartRequested?.Invoke();
	}
}
