using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        PlayerHealth player = FindAnyObjectByType<PlayerHealth>();
        UpdateHealthUI(player.GetHealthPercentage());
        player.OnPlayerTakeDamage.AddListener(UpdateHealthUI);
    }

    private void UpdateHealthUI(float percentage)
    {
        healthBar.value = percentage;
    }
}
