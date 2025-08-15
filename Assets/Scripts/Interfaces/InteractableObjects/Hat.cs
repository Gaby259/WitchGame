using UnityEngine;

public class Hat : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float movementAmplitude = 0.5f;
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);
    private Vector3 _startPosition;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
        float _amplitude = Mathf.PingPong(Time.time, movementAmplitude);
        transform.position = _startPosition + Vector3.up * _amplitude; //float movement
    }

}