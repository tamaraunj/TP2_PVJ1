using UnityEngine;

public class MudZone : MonoBehaviour
{
    [Header("Mud Settings")]
    [SerializeField] private float slowSpeed = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.SetSpeed(slowSpeed);
            Debug.Log("Entraste en el barro: movimiento lento.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.SetSpeed(player.NormalSpeed);
            Debug.Log("Saliste del barro: velocidad normal.");
        }
    }
}
