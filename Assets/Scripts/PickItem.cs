using UnityEngine;

public class PickItem : MonoBehaviour
{
    [SerializeField] private int points = 10;

    private bool collected = false;

    public void Collect()
    {
        if (collected) return;

        ScoreSystem scoreSystem = FindFirstObjectByType<ScoreSystem>();

        if (scoreSystem == null)
        {
            Debug.LogWarning("No se encontró un ScoreSystem.");
            return;
        }

        collected = true;
        scoreSystem.AddPoints(points);

        Debug.Log("Objeto recolectado.");

        Destroy(gameObject);
    }
}
