using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(MonsterStats), typeof(NavMeshAgent))]
public class MonsterAIController : MonoBehaviour
{
    public enum AIState { Patrolling, Chasing, Attacking }

    [Header("AI Configuration")]
    public AIState currentState = AIState.Patrolling;
    [Tooltip("The base range within which the monster detects the player.")]
    public float baseDetectionRadius = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Night Behavior")]
    [Tooltip("Multiplier for detection radius and speed at night.")]
    public float nightAggressionMultiplier = 1.5f;

    [Header("Patrol Settings")]
    public Vector3[] patrolPoints;

    private int currentPatrolIndex = 0;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private MonsterStats monsterStats;

    // Dynamic variables that change with time of day
    private float currentDetectionRadius;
    private float currentChaseSpeed;
    private bool isNight = false;

    void OnEnable()
    {
        GameManager.OnTimeOfDayChanged += HandleTimeOfDayChange;
    }

    void OnDisable()
    {
        GameManager.OnTimeOfDayChanged -= HandleTimeOfDayChange;
    }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterStats = GetComponent<MonsterStats>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Set initial dynamic values
        currentDetectionRadius = baseDetectionRadius;
        currentChaseSpeed = chaseSpeed;
    }

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject has the 'Player' tag.");
            this.enabled = false;
            return;
        }

        if (patrolPoints.Length == 0)
        {
            patrolPoints = new Vector3[] { transform.position };
        }

        navMeshAgent.SetPosition(transform.position);
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case AIState.Patrolling:
                Patrol(distanceToPlayer);
                break;
            case AIState.Chasing:
                Chase(distanceToPlayer);
                break;
            case AIState.Attacking:
                Attack(distanceToPlayer);
                break;
        }
    }

    private void HandleTimeOfDayChange(GameManager.TimeOfDay newTimeOfDay)
    {
        isNight = newTimeOfDay == GameManager.TimeOfDay.Night;
        if (isNight)
        {
            currentDetectionRadius = baseDetectionRadius * nightAggressionMultiplier;
            currentChaseSpeed = chaseSpeed * nightAggressionMultiplier;
            Debug.Log($"{monsterStats.monsterName} becomes more aggressive!");
        }
        else
        {
            currentDetectionRadius = baseDetectionRadius;
            currentChaseSpeed = chaseSpeed;
            Debug.Log($"{monsterStats.monsterName} calms down.");
        }
    }

    private void Patrol(float distanceToPlayer)
    {
        navMeshAgent.speed = patrolSpeed;
        if (distanceToPlayer <= currentDetectionRadius)
        {
            currentState = AIState.Chasing;
            return;
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void Chase(float distanceToPlayer)
    {
        navMeshAgent.speed = currentChaseSpeed;
        if (distanceToPlayer > currentDetectionRadius)
        {
            currentState = AIState.Patrolling;
            GoToNextPatrolPoint();
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            currentState = AIState.Attacking;
            return;
        }

        navMeshAgent.SetDestination(playerTransform.position);
    }

    private void Attack(float distanceToPlayer)
    {
        navMeshAgent.isStopped = true;
        // TODO: Implement attack logic
        Debug.Log($"{monsterStats.monsterName} is attacking the player!");

        if (distanceToPlayer > attackRange)
        {
            navMeshAgent.isStopped = false;
            currentState = AIState.Chasing;
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex]);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // Use the dynamic radius for the gizmo
        float radius = isNight ? baseDetectionRadius * nightAggressionMultiplier : baseDetectionRadius;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
