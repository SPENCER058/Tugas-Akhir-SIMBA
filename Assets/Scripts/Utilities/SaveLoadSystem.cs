using System.IO;
using UnityEngine;

/// <summary>
/// Provides functionality to save and load data to and from JSON files.
/// </summary>
public static class SaveLoadSystem
{
	/// <summary>
	/// Saves data to a JSON file.
	/// </summary>
	/// <typeparam name="T">The type of data to save.</typeparam>
	/// <param name="data">The data to save.</param>
	/// <param name="fileName">The name of the file to save the data in.</param>
	public static void SaveData<T> (T data, string fileName)
	{
		string json = JsonUtility.ToJson(data, true);
		string path = GetPath(fileName);
		File.WriteAllText(path, json);
	}

	/// <summary>
	/// Loads data from a JSON file.
	/// </summary>
	/// <typeparam name="T">The type of data to load.</typeparam>
	/// <param name="fileName">The name of the file to load the data from.</param>
	/// <returns>The loaded data, or the default value of type T if the file does not exist.</returns>
	public static T LoadData<T> (string fileName)
	{
		string path = GetPath(fileName);
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			return JsonUtility.FromJson<T>(json);
		}
		return default(T);
	}

	/// <summary>
	/// Gets the full path to the specified file within the appropriate directory.
	/// </summary>
	/// <param name="fileName">The name of the file.</param>
	/// <returns>The full path to the file.</returns>
	private static string GetPath (string fileName)
	{
#if UNITY_EDITOR
		string directory = Path.Combine(Application.dataPath, "Saves");
#else
				string directory = Path.Combine(Application.persistentDataPath, "Saves");
#endif

		// Create the directory if it doesn't exist
		if (!Directory.Exists(directory))
		{
			Directory.CreateDirectory(directory);
		}

		return Path.Combine(directory, fileName + ".json");
	}
}
