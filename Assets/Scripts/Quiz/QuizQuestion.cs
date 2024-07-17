/// <summary>
/// Represents a single quiz question, including the question text, possible options, and the correct answer.
/// </summary>
[System.Serializable]
public class QuizQuestion
{
	/// <summary>
	/// The unique identifier for the question.
	/// </summary>
	public int id;

	/// <summary>
	/// The text of the question.
	/// </summary>
	public string question;

	/// <summary>
	/// The array of possible answer options.
	/// </summary>
	public string[] options;

	/// <summary>
	/// The correct answer option.
	/// </summary>
	public string correct_option;
}