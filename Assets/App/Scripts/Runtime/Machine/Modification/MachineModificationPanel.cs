using UnityEngine;
public class MachineModificationPanel : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] GameObject panel;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_OpenMachineModificationPanel rseOpenPanel;
    [SerializeField] RSE_CloseMachineModificationPanel rseClosePanel;

    //[Header("Output")]

    private void OnEnable()
    {
        rseOpenPanel.action += OpenPanel;
        rseClosePanel.action += ClosePanel;
    }
    private void OnDisable()
    {
        rseOpenPanel.action -= OpenPanel;
        rseClosePanel.action -= ClosePanel;
    }

    void OpenPanel()
    {
        panel.SetActive(true);
    }
    void ClosePanel()
    {
        panel.SetActive(false);
    }
}