using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar; // Reference to the health bar UI
    public float healthAmount = 100f; // Player's health

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Clamp between 0 and 100
        healthBar.fillAmount = healthAmount / 100f; // Update the health bar
        Debug.Log("Player took damage. Current health: " + healthAmount);
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Clamp between 0 and 100
        healthBar.fillAmount = healthAmount / 100f; // Update the health bar
        Debug.Log("Player healed. Current health: " + healthAmount);
    }
}

