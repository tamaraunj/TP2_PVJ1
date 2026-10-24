using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] private ConsoleInteraction console;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (console == null)
        {
            Debug.LogWarning("No se asignó la consola a GoalZone.");
            return;
        }

        if (!console.isActivated)
        {
            Debug.Log("Primero tenés que activar la consola.");
            return;
        }

        Debug.Log("¡Completaste el nivel!");
        GameManager.Instance.NotifyGoalZone();
    }
}
