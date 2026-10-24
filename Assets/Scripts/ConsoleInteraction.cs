using UnityEngine;

public class ConsoleInteraction : MonoBehaviour
{
    [Header("Console Settings")]
    [SerializeField] private Color activatedColor = Color.green;
    [SerializeField] private int requiredItems = 7;
    public bool isActivated = false;
    

    public void Activate()
    {
        if (isActivated)
        {
            Debug.Log("La consola ya está activada.");
            return;
        }

        ScoreSystem scoreSystem = FindFirstObjectByType<ScoreSystem>();

        if (scoreSystem == null)
        {
            Debug.LogWarning("No se encontró un ScoreSystem.");
            return;
        }

        if (scoreSystem.CollectedItems < requiredItems)
        {
            Debug.Log(
                "Datos insuficientes: " +
                scoreSystem.CollectedItems + "/" + requiredItems
            );
            return;
        }

        isActivated = true;

        GetComponent<Renderer>().material.color = activatedColor;

        Debug.Log("¡Consola activada! Ya podés abrir el búnker.");
    }
}
