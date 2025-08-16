using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GameData
{
    public int _playerScore;
    public Vector3 _playerPosition;
    public bool _playerHasHat;
}
public class PlayerData : MonoBehaviour
{
    public int Score { get; private set; }
    public bool HasHat { get; private set; }
    public Vector3 PlayerPosition => playerTransform.position;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject hatObject;

    private void Awake()
    {
        GameData savedData = SaveManager.Instance.LoadGame();
        if (savedData != null)
        {
            SetPlayerData(savedData);
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        UI.Instance.UpdateScoreUI(Score);
    }

    public void EquipHat()
    {
        HasHat = true;
        if(hatObject != null)
            hatObject.SetActive(true);
    }

    public void UnequipHat()
    {
        HasHat = false;
        if(hatObject != null)
            hatObject.SetActive(false);
    }

    public void SavePlayerData()
    {
        GameData data = new GameData()
        {
            _playerScore = Score,
            _playerPosition = PlayerPosition,
            _playerHasHat = HasHat
        };

        SaveManager.Instance.SaveGame(data);
    }

    public void SetPlayerData(GameData data)
    {
        Score = data._playerScore;
        transform.position = data._playerPosition;

        if (data._playerHasHat)
            EquipHat();
        else
            UnequipHat();

        UI.Instance.UpdateScoreUI(Score); //Update the UI
    }
}
