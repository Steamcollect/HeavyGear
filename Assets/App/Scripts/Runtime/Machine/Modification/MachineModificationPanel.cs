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

    [Header("Output")]
    [SerializeField] RSE_DestroyMachine rseDestroyMachine;

    void OpenPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void DestroyButton()
    {
        rseDestroyMachine.Call();
        ClosePanel();
    }
}