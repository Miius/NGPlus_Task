using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    private static SaveManager instance;
    public static SaveManager Instance => instance ? instance : FindFirstObjectByType<SaveManager>();
    #endregion

    [Serializable]
    public class SaveData
    {
        [HideInInspector]public List<string> collectedItems = new List<string>();
        [HideInInspector]public List<string> inventoryItems = new List<string>();
        [HideInInspector]public List<string> consumedItems = new List<string>();
        [HideInInspector]public int receiverCount = 0; 
    }

    public SaveData Data = new SaveData();
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/save.json";
        LoadGame();
        DontDestroyOnLoad(this.gameObject);
    }
    public void SaveGame()
    {
        try
        {
            string json = JsonUtility.ToJson(Data, true);
            File.WriteAllText(savePath, json);
            Debug.Log("Save: " + savePath);
        }
        catch (Exception e)
        {}  
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Data = new SaveData();
            return;
        }

        try
        {
            string json = File.ReadAllText(savePath);
            Data = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
        }
        catch (Exception e)
        {
            Data = new SaveData();
        }
    }
    public void DeleteSaveAndReloadScene()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);

        Data = new SaveData();
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
    public bool IsItemCollected(string id)
    {
        return Data.collectedItems.Contains(id);
    }
    public void CollectSceneItem(string id)
    {
        if (!Data.collectedItems.Contains(id))
            Data.collectedItems.Add(id);

        SaveGame();
    }
    public List<string> GetSavedInventory()
    {
        return Data.inventoryItems;
    }
    public void AddInventoryItem(string itemName)
    {
        Data.inventoryItems.Add(itemName);
        SaveGame();
    }
    public void RemoveInventoryItem(string itemName)
    {
        if (Data.inventoryItems.Contains(itemName))
            Data.inventoryItems.Remove(itemName);

        SaveGame();
    }
    public void ClearInventorySave()
    {
        Data.inventoryItems.Clear();
        SaveGame();
    }
    public int GetReceiverCount()
    {
        return Data.receiverCount;
    }
    public void AddReceiverCount(int amount = 1)
    {
        Data.receiverCount += amount;
        SaveGame();
    }

    public void ConsumeItem(string itemName)
    {
        if (!Data.consumedItems.Contains(itemName))
            Data.consumedItems.Add(itemName);

        SaveGame();
    }
    public bool IsConsumed(string itemName)
    {
        return Data.consumedItems.Contains(itemName);
    }
}
