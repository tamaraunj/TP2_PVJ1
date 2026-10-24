using UnityEngine;

public class BunkerDoor : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private ConsoleInteraction console;
    private bool isOpen = false;
    public bool TryOpenDoor()
    {
        if (isOpen)
        {
            Debug.Log("La puerta ya está abierta.");
            return true;
        }

        if (console == null)
        {
            Debug.LogWarning("No se asignó la consola a la puerta.");
            return false;
        }

        if (!console.isActivated)
        {
            Debug.Log("No podés abrir la puerta. Primero activá la consola.");
            return false;
        }

        isOpen = true;

        Debug.Log("¡La puerta del búnker se abrió!");

        gameObject.SetActive(false);

        return true;
    }

}
