using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 3f;
    [SerializeField] protected ParticleSystem impactParticles;
    [SerializeField] protected float damage = 10f;

    private void Start()
    {
        Destroy(gameObject, projectileLifeTime);
    }
}
