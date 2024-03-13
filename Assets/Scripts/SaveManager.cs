using UnityEngine;

public static class SaveManager
{
    // private const string MONEY = "money";
    // private static GameData gameData = new GameData();
    private const string SAVE_DATA = "saveData";

    public static void Save<T>(string key, T value)
    {
        Debug.Log("Saved " + key + " " + value);
        var jsonDataString = JsonUtility.ToJson(value, true);
        Debug.Log(jsonDataString);
        PlayerPrefs.SetString(key, jsonDataString);
    }

    public static T Load<T>(string key) where T : new()
    {
        Debug.Log("Loaded " + key);
        if (!PlayerPrefs.HasKey(key)) return new T();
        string loadedString = PlayerPrefs.GetString(key);
        Debug.Log(loadedString);
        return JsonUtility.FromJson<T>(loadedString);
    }

    public static void SaveData()
    {
        var data = new GameData();
        Save(SAVE_DATA, data);
    }

    public static GameData LoadData()
    {
        var data = Load<GameData>(SAVE_DATA);
        return data;
    }
}