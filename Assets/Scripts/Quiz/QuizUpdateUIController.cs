using UnityEngine;
using TMPro;
public class QuizUpdateUIController : MonoBehaviour
{
    [SerializeField] private GameObject updateTriggerButton;
    [SerializeField] private TextMeshProUGUI m_CurrentVersion;
	[SerializeField] private TextMeshProUGUI m_NewestVersion;
	[SerializeField] private GameObject updatePanel;

    public void ActivateUpdatePanel (string currentVersion, string newestVersion)
    {
        updateTriggerButton.SetActive (true);
        UpdateText(m_CurrentVersion, $"Current Version:{currentVersion}");
		UpdateText(m_NewestVersion, $"Newest Version:{newestVersion}");
		updatePanel.SetActive (true);
    }

    public void DeactivateUpdatePanel ()
    {
        updatePanel.SetActive(false);
    }

    private void UpdateText (TextMeshProUGUI TMP, string text)
    {
        TMP.text = text;
    }
}
