using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinePlacementPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject panel;
    [SerializeField] Transform content;
    ContentSizeFitter contentSizeFitter;
    [SerializeField] MachinePlacementUI machinePlacementUIPrefab;
    List<MachinePlacementUI> machinesUI = new List<MachinePlacementUI>();

    [Header("Input")]
    [SerializeField] RSE_SetupMachinePlacementPanelContent rseSetupContent;
    [SerializeField] RSE_UpdateMachinePlacementPanelContent rseUpdateContent;

    [Header("Input")]
    [SerializeField] RSE_OpenMachinePlacementPanel rseOpenPanel;
    [SerializeField] RSE_CloseMachinePlacementPanel rseClosePanel;

    private void OnEnable()
    {
        rseOpenPanel.action += OpenPanel;
        rseClosePanel.action += ClosePanel;

        rseUpdateContent.action += UpdateContent;
        rseSetupContent.action += SetupContent;
    }
    private void OnDisable()
    {
        rseOpenPanel.action -= OpenPanel;
        rseClosePanel.action -= ClosePanel;

        rseUpdateContent.action -= UpdateContent;
        rseSetupContent.action -= SetupContent;
    }

    private void Awake()
    {
        contentSizeFitter = content.GetComponent<ContentSizeFitter>();
    }

    void SetupContent(List<SSO_MachinePlacementData> machines)
    {
        foreach (SSO_MachinePlacementData machine in machines)
        {
            MachinePlacementUI current = Instantiate(machinePlacementUIPrefab, content);
            current.Setup(machine);
            machinesUI.Add(current);
        }
    }

    void UpdateContent(MachineType machineType)
    {
        foreach (var machine in machinesUI)
        {
            if(machine.machineData.machineType == machineType) machine.gameObject.SetActive(true);
            else machine.gameObject.SetActive(false);
        }
    }

    void OpenPanel()
    {
        panel.SetActive(true);
        StartCoroutine(UpdateContentSize());
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    IEnumerator UpdateContentSize()
    {
        yield return new WaitForSeconds(.01f);
        contentSizeFitter.SetLayoutVertical();
        contentSizeFitter.SetLayoutHorizontal();
    }
}
