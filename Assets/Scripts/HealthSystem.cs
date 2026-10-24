using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private TMP_Text healthText;
    private float currentHealth;
    public float CurrentHealth => currentHealth;//usamos getter y setter para el gameManager - solo lectura

    [SerializeField] private bool isDead;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        UpdateHealthText();
    }
  
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthText();

        //Debug.Log("<color=red>❤️ Salud actual: " + currentHealth + "</color>"); 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.NotifyPlayerStatusChanged();
        }

        if (currentHealth <= 0f)
        {
            Dead();
        }
    }
    private void Dead()
    {
        isDead = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.NotifyPlayerDead();
        }
    }
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Salud: " + currentHealth + " / " + maxHealth;
        }
    }
}
