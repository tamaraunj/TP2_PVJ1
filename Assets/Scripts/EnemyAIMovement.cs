using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMovement : MonoBehaviour
{
    public enum AIState { Patrol, Chase, Attack }

    [Header("State")]
    [SerializeField] private AIState currentState = AIState.Patrol;

    [Header("Detection")]
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private float attackRadius = 2f;

    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTimeAtPoint = 2f;
    private bool isAttacking = false;
    private NavMeshAgent agent;
    private Transform playerTransform;
    private int currentPatrolIndex;
    private bool isWaitingAtPoint;
    private HealthSystem healthSystemPlayer;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 1f;

    private float nextDamageTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        if (agent == null)
        {
            Debug.LogError("El enemigo necesita un NavMeshAgent.");
            return;
        }
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            healthSystemPlayer = playerObj.GetComponent<HealthSystem>();
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag Player");
            return;
        }

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            GoToNextPatrolPoint();
        }
        else
        {
            Debug.LogWarning("No hay puntos de patrulla asignados.");
        }
    }
    public void Update()
    {
        if (playerTransform == null || agent == null || !agent.isOnNavMesh)
            return;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        switch (currentState)
        {
            case AIState.Patrol:
                UpdatePatrolState(distanceToPlayer);
                break;

            case AIState.Chase:
                UpdateChaseState(distanceToPlayer);
                break;

            case AIState.Attack:
                UpdateAttackState(distanceToPlayer);
                break;
        }
    }
    private void UpdatePatrolState(float distanceToPlayer)
    {
        agent.isStopped = false;

        if (distanceToPlayer <= detectionRadius)
        {
            currentState = AIState.Chase;
            return;
        }
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isWaitingAtPoint)
            {
                StartCoroutine(WaitAndMoveToNextPoint());
            }
        }
    }
    private void UpdateChaseState(float distanceToPlayer)
    {
        agent.isStopped = false;
        if (distanceToPlayer <= attackRadius)
        {
            currentState = AIState.Attack;
            return;
        }

        agent.SetDestination(playerTransform.position);

        // Si el jugador se escapa del radio de deteccion, vuelve a patrullar
        if (distanceToPlayer > detectionRadius * 1.2f)
        {
            currentState = AIState.Patrol;
            if (patrolPoints != null && patrolPoints.Length > 0)
            {
                GoToNextPatrolPoint();
            }
        }
    }
    private void UpdateAttackState(float distanceToPlayer)
    {
        
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));

        if (distanceToPlayer > attackRadius)
        {
            currentState = AIState.Chase;
            isAttacking = false;
            return;
        }

        
        if (!isAttacking)
        {
            isAttacking = true;
            nextDamageTime = Time.time;
        }

        if (Time.time >= nextDamageTime)
        {
            if (healthSystemPlayer != null)
            {
                healthSystemPlayer.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageInterval;
                
            }
        }
    }
    private IEnumerator WaitAndMoveToNextPoint()
    {
        isWaitingAtPoint = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTimeAtPoint);

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            GoToNextPatrolPoint();
        }
        agent.isStopped = false;
        isWaitingAtPoint = false;
    }
    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0 || patrolPoints == null) return;
        if (patrolPoints[currentPatrolIndex] == null) return;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
