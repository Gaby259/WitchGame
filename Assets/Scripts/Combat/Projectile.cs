using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 10f;
    [SerializeField] private float projectileLifeTime = 3f;
    [SerializeField] protected ParticleSystem impactParticles;

    private void Start()
    {
        Destroy(gameObject, projectileLifeTime);
    }
}
