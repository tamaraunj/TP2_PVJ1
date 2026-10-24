using UnityEngine;

public class ToxicGasZone : MonoBehaviour
{
    [Header("Toxic Gas Settings")]
    [SerializeField] private float damageInterval = 0.2f;
    private float damageTimer = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        HealthSystem health = other.GetComponent<HealthSystem>();

        if (health == null)
            return;

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            health.TakeDamage(1);
            damageTimer -= damageInterval;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }

}
