using System.Collections.Generic;

/// <summary>
/// Represents the data for a quiz, including a list of quiz questions.
/// </summary>
[System.Serializable]
public class QuizData
{
	/// <summary>
	/// The version of the quiz data.
	/// </summary>
	public string version;

	/// <summary>
	/// List of quiz questions.
	/// </summary>
	public List<QuizQuestion> questions;
}