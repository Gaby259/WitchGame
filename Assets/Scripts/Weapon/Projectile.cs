using UnityEngine;

public class Projectile : MonoBehaviour
{
   /* [SerializeField] private float _speed;
    [SerializeField] private LayerMask _environmentLayerMask;
    [SerializeField] private float _lifeTime;
    [SerializeField] private LayerMask _enemyLayerMask;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (enemy != null)
        {
            Destroy(gameObject);
            return;
        }

        if ((_environmentLayerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
    }*/
}