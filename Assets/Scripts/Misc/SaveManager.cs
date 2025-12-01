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
        public List<string> collectedItems = new List<string>();
        public List<string> inventoryItems = new List<string>();
        public List<string> consumedItems = new List<string>();
        public int receiverCount = 0;
    }

    public SaveData Data = new SaveData();
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        LoadGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Data = new SaveData();
            return;
        }

        string json = File.ReadAllText(savePath);
        Data = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
    }

    public void DeleteSaveAndReloadScene()
    {
        if (File.Exists(savePath)) File.Delete(savePath);
        Data = new SaveData();
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public bool IsItemCollected(string id) => Data.collectedItems.Contains(id);

    public void CollectSceneItem(string id)
    {
        if (!Data.collectedItems.Contains(id)) Data.collectedItems.Add(id);
        SaveGame();
    }

    public List<string> GetSavedInventory() => Data.inventoryItems;

    public void AddInventoryItem(string itemName)
    {
        Data.inventoryItems.Add(itemName);
        SaveGame();
    }

    public void RemoveInventoryItem(string itemName)
    {
        if (Data.inventoryItems.Contains(itemName)) Data.inventoryItems.Remove(itemName);
        SaveGame();
    }

    public void ClearInventorySave()
    {
        Data.inventoryItems.Clear();
        SaveGame();
    }

    public int GetReceiverCount() => Data.receiverCount;

    public void AddReceiverCount(int amount = 1)
    {
        Data.receiverCount += amount;
        SaveGame();
    }

    public void ConsumeItem(string itemName)
    {
        if (!Data.consumedItems.Contains(itemName)) Data.consumedItems.Add(itemName);
        SaveGame();
    }

    public bool IsConsumed(string itemName) => Data.consumedItems.Contains(itemName);
}
