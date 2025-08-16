using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour
{
    [FormerlySerializedAs("_rotationSpeed")]
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);
    
    [Header("Movement")]
    [SerializeField] private float movementAmplitude = 0.5f;
    private Vector3 _startPosition;
    
    [Header("Value")]
    [SerializeField] private int scoreValue = 10;
    
    [Header ("Victory Collectable")]
    [SerializeField] private bool isVictoryCollectable = false;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
        float _amplitude = Mathf.PingPong(Time.time, movementAmplitude);
        transform.position = _startPosition + Vector3.up * _amplitude; //float movement
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected");
            if (isVictoryCollectable == true)
            {
                GameManager.Instance.WinGame();
                
            }
            UI.Instance.AddScore(scoreValue);
            transform.DOScale(Vector3.zero, 0.3f)
                .OnComplete(() => Destroy(gameObject));
        }
        
    }
    
}
