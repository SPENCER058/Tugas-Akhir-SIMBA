using UnityEngine;

[CreateAssetMenu(fileName = "LiterationContent", menuName = "ScriptableObjects/LiterationContent", order = 1)]
public class SO_LiterationContent : ScriptableObject
{
	[SerializeField] private Sprite _panelTitleImage;

	public Sprite GetPanelTitleImage { get { return _panelTitleImage; } }

	[SerializeField] private Sprite _headerImage;

	public Sprite GetHeaderImage { get { return _headerImage; } }

	[SerializeField, TextArea(3, 100)] private string _contentText;

	public string GetContentText { get { return _contentText; } }
}
