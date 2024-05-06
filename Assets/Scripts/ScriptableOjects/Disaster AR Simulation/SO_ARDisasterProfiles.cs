using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DisasterARProfiles", menuName = "ScriptableObjects/ARDisasterProfiles", order = 1)]
public class SO_ARDisasterProfiles : ScriptableObject
{
	[Header("Base Disaster Simulation Info")]

	[SerializeField] private Sprite _disasterIcon;

	public Sprite GetDisasterIcon { get { return _disasterIcon; } }

	[SerializeField] private string _disasterName;

	public string GetDisasterName { get { return _disasterName; } }

	[SerializeField] private string _disasterLocation;

	public string GetDisasterLocation { get { return _disasterLocation; } }

	[SerializeField] private string _disasterScaleUnit;

	public string GetDisasterScaleUnit { get { return _disasterScaleUnit; } }

	[Header("Disaster Strenght")]

	[SerializeField] private List<float> _disasterStreghtList;

	public float GetDisasterStreght (int index) { return _disasterStreghtList[index]; }

	[Header("Disaster Information")]

	[SerializeField] private Sprite _infoPanelTitleImage;

	public Sprite GetPanelTitleImage { get { return _infoPanelTitleImage; } }

	[SerializeField] private Sprite _headerImage;

	public Sprite GetHeaderImage { get { return _headerImage; } }

	[SerializeField, TextArea(3, 100)] private string _contentText;

	public string GetContentText { get { return _contentText; } }
}