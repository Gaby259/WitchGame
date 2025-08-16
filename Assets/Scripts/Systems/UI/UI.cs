using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI :Singleton<UI>
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text scoreText;
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

    public void UpdateScoreUI(int currentScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }
}
