using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// A class for building enemies. 
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Default speed.")]
    [SerializeField]
    private float startSpeed;

    [Header("Default health.")]
    [SerializeField]
    private float startHealth;

    [Header("Credits awarded on death.")]
    [SerializeField]
    private int value = 50;

    [Header("Particle effect on death.")]
    [SerializeField]
    private GameObject deathEffect;

    [Header("The enemy's health bar.")]
    [SerializeField]
    private Image healthBar;
    
    private bool isDead = false;
    public float speed { get; private set; }
    public float currentMaximumHealth { get; private set; }
    public float currentHealth { get; private set; }
    
    /// <summary>
    /// Resets the enemy's speed to its original value.
    /// </summary>
    public void ResetSpeed() { speed = startSpeed; }

    /// <summary>
    /// Resets the enemy's health to its starting value. 
    /// </summary>
    private void ResetHealth() { currentHealth = startHealth; }

    /// <summary>
    /// Sets the enemy's health bar fill amount according to its current health.
    /// If this method is used often enough, consider moving it to the Update method.
    /// </summary>
    private void SetHealthBarFillAmount() { healthBar.fillAmount = currentHealth / startHealth; }

    /// <summary>
    /// Slows the enemy by a given percentage. Enter a number between 0 and 1. 
    /// </summary>
    public void Slow(float slowPct)
    {
        if(slowPct > 1)
        {
            slowPct = 1;
        }
        else if (slowPct < 0)
        {
            slowPct = 0;
        }

        speed = startSpeed * (1 - slowPct);
    }

    private void Start()
    {
        ResetSpeed();
        ResetHealth();
    }

    /// <summary>
    /// The enemy takes damage. Its health value is reduced, 
    /// and the health bar fill amount is changed to reflect this.
    /// If the health value of the enemy drops below zero, the enemy dies. 
    /// </summary>
    public void TakeDamage (float amount)
    {
        currentHealth -= amount;

        SetHealthBarFillAmount();

        if(currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    /// <summary>
    /// The enemy dies. A death effect plays, and is then destroyed. 
    /// The player earns credits according to the value of the enemy, 
    /// the number of enemies alive in the stage is reduced, 
    /// and the enemy object is destroyed. 
    /// </summary>
    private void Die ()
    {
        isDead = true;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.5F);
        PlayerStats.EarnMoney(value);
        DeductAndDestroy();
    }    

    public void DeductAndDestroy ()
    {
        WaveSpawner.ReduceEnemyCount();
        Destroy(gameObject);
    }
}