using UnityEngine;

public class SoldierStats : MonoBehaviour
{
    public float maxHealth = 100f; // Adjust the max health as per your requirements

    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Ouch");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform death logic (e.g., play death animation, disable the game object, etc.)
        Destroy(gameObject);
    }
}
