using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        HealthSystem health = other.GetComponent<HealthSystem>();

        if (health != null)
        {
            health.TakeDamage(health.CurrentHealth);
            Debug.Log("El jugador tocó el láser");
        }
    }
}
