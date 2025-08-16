using UnityEngine;
public class SaveManager : Singleton<SaveManager>
{
    //handles the file of where is going to be saved the info 
    private string _savePath;

   void Start()
    {
        _savePath = Application.persistentDataPath + "/gamedata.json";
    }

    public void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
        System.IO.File.WriteAllText(_savePath, json);
    }

    public GameData LoadGame()
    {
        if (System.IO.File.Exists(_savePath))
        {
            string json = System.IO.File.ReadAllText(_savePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }
}
    


