using System.Collections.Generic;
using UnityEngine;

public class ARScanUISwitch : MonoBehaviour
{
	[SerializeField] private List<GameObject> idleObjetcsUI = new List<GameObject>();
	[SerializeField] private List<GameObject> simulationObjetcsUI = new List<GameObject>();

	public void SetIdleUI ()
	{
		foreach (GameObject go in simulationObjetcsUI) { go.SetActive(false); }
		foreach (GameObject go in idleObjetcsUI) { go.SetActive(true); }
	}

	public void SetSimulationUI ()
	{
		foreach (GameObject go in simulationObjetcsUI) { go.SetActive(true); }
		foreach (GameObject go in idleObjetcsUI) { go.SetActive(false); }
	}
}
