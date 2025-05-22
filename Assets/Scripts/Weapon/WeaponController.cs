using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzlePoint;

    private void HandleFire()
    {
        if (_bulletPrefab != null && _muzzlePoint != null)
        {
            Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
        }
    }
}
