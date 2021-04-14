using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [HideInInspector]
    public List<PlayerData> playerDatabase = new List<PlayerData>();
    [HideInInspector]
    public int currentPlayerData;

    public PlayerData loadedData;

    void Start()
    {
        CreateData();
        Save();
        Load();
    }

    public void CreateData()
    {
        var playerData = new PlayerData
        {
            itemIds = new List<int> { 1, 4, 7 },
            currentCheckpoint = 123,
            experience = 1999,
            currentHealth = 75.5f,
            currentMana = 100f,
            characterName = "Krogor",
            characterSkin = Skin.Dwarf
        };

        playerDatabase.Add(playerData);
        currentPlayerData = playerDatabase.Count - 1;
    }

    public void Save()
    {
        var p = playerDatabase[currentPlayerData];
        var json = JsonUtility.ToJson(p);
        PlayerPrefs.SetString("Player" + currentPlayerData, json);
        //Debug.Log()
    }

    public void Load()
    {
        var json = PlayerPrefs.GetString("Player" + currentPlayerData);
        loadedData = JsonUtility.FromJson<PlayerData>(json);
    }
}
