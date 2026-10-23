using UnityEngine;

public class ConsoleInteraction : MonoBehaviour
{
    [SerializeField] private Color activatedColor = Color.green;
    public bool isActivated = false;

    public void Activate()
    {
        if (isActivated == false)
        {
            isActivated = true;

            GetComponent<Renderer>().material.color = activatedColor;

            Debug.Log("Console activated!");
        }
    }
}
