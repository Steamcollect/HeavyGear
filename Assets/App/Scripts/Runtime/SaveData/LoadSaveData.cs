using System;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

namespace BT.Save
{
    public class LoadSaveData : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RSE_LoadData rseLoadData;
        [SerializeField] private RSE_SaveData rseSaveData;
        [SerializeField] private RSE_ClearData rseClearData;
        [SerializeField] private RSO_ContentSaved rsoContentSaved;
        
        private string filepath;

        private void OnEnable()
        {
            rseLoadData.action += LoadFromJson;
            rseSaveData.action += SaveToJson;
            rseClearData.action += ClearContent;
        }

        private void OnDisable()
        {
            rseLoadData.action -= LoadFromJson;
            rseSaveData.action -= SaveToJson;
        }

        private void Awake()
        {
            filepath = Application.persistentDataPath + $"/{Application.productName}_Save.json";

            if (FileAlreadyExist()) LoadFromJson();
            else
            {
                rsoContentSaved.Value = new ContentSaved();
            }
        }

        private void SaveToJson()
        {
            string infoData = JsonUtility.ToJson(rsoContentSaved.Value,true);
            File.WriteAllText(filepath, infoData);
        }

        private void LoadFromJson()
        {
            string infoData = File.ReadAllText(filepath);
            ContentSaved value = JsonUtility.FromJson<ContentSaved>(infoData);
            rsoContentSaved.Value = value ?? new ContentSaved();
        }

        private void ClearContent()
        {
            rsoContentSaved.Value = new ContentSaved();
            SaveToJson();
        }

        private bool FileAlreadyExist()
        {
            return File.Exists(filepath);
        }
        
        
        private async void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            await Task.Delay(100);
            rseSaveData.Call();
        }

        private void OnApplicationQuit()
        {
            rseSaveData.Call();
        }

        private async void OnDestroy()
        {
            await Task.Delay(100);
            rsoContentSaved.Value = null;
        }
    }   
}
