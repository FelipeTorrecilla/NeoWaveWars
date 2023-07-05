using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving,
        Chase,
        ChaseEnemyCore,
        Attack,
        AttackEnemyCore,
    }

    private SoldierAttack soldierAttack;
    //private SoldierPathfindingMovement pathfindingMovement;
    
    
    private Vector3 startingPosition;
    public State state;
    public float searchRadius = 10f; // Adjust the radius as per your requirements
    public LayerMask enemyLayer; // Assign the enemy layer in the Inspector
    public LayerMask enemyCoreLayer;
    public float chaseSpeed = 5f; // Adjust the chase speed as per your requirements
    public float attackRange = 2f; // Adjust the attack range as per your requirements
    public float attackDamage = 10f; // Adjust the attack damage as per your requirements
    public float rotationSpeed = 5f;
    private float attackCooldown = 1f; // Adjust the attack cooldown duration as per your requirements
    private float nextAttackTime;

    [SerializeField] private Transform _banner;

    private Transform targetEnemy;
    private Transform targetEnemyCore;
    private SoldierStats targetEnemyStats;

    public NavMeshAgent _agent;

    private void Awake()
    {
        //pathfindingMovement = GetComponent<SoldierPathfindingMovement>();
        state = State.Idle;
    }

    private void Start()
    {
        startingPosition = transform.position;
    }


    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                SearchForEnemy();
                break;

            /*case State.Moving: 
                MovingToBanner();
                break;
            */    
            case State.Chase:
                ChaseEnemy();
                break;
            
            case State.ChaseEnemyCore:
                ChaseEnemyCore();
                break;

            case State.Attack:
                AttackEnemy();
                break;
            
            case State.AttackEnemyCore:
                AttackEnemyCore();
                break;
        }
    }

    private void SearchForEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);

        bool foundEnemy = false;
        bool foundEnemyCore = false;

        _agent.SetDestination(_banner.position);

        foreach (Collider collider in hitColliders)
        {
            if (!foundEnemy)
            {
                // Check if the collider's layer matches the enemyLayer
                if (((1 << collider.gameObject.layer) & enemyLayer) != 0)
                {
                    // Enemy found!
                    targetEnemy = collider.transform;
                    targetEnemyStats = collider.GetComponent<SoldierStats>();
                    foundEnemy = true;
                }
            }

            if (!foundEnemyCore)
            {
                // Check if the collider's layer matches the enemyCoreLayer
                if (((1 << collider.gameObject.layer) & enemyCoreLayer) != 0)
                {
                    // Enemy core found!
                    targetEnemyCore = collider.transform;
                    targetEnemyStats = collider.GetComponent<SoldierStats>();
                    foundEnemyCore = true;
                }
            }

            if (foundEnemy)
            {
                // If enemy found, transition to Chase state
                state = State.Chase;
                return;
            }
        }

        // Only enemy core found, transition to ChaseEnemyCore state
        if (foundEnemyCore && !foundEnemy)
        {
            state = State.ChaseEnemyCore;
            return;
        }

    // No enemies or enemy core found, stay in the Idle state
    }

    private void ChaseEnemy()
    {
        if (targetEnemy == null || targetEnemyStats == null)
        {
            // Enemy or enemy's stats lost, transition back to Idle state
            state = State.Idle;
            return;
        }

        // Move towards the enemy
        _agent.SetDestination(targetEnemy.position);

        // Use the pathfinding movement component to move towards the enemy
        //pathfindingMovement.Move(direction * chaseSpeed);

        // Check if the enemy is within attack range
        float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);
        if (distanceToEnemy <= attackRange)
        {
            // Transition to Attack state
            state = State.Attack;
        }
    }

    private void ChaseEnemyCore()
    {
        if (targetEnemyCore == null || targetEnemyStats == null)
        {
            // Enemy or enemy's stats lost, transition back to Idle state
            state = State.Idle;
            return;
        }

        // Move towards the enemy
        _agent.SetDestination(targetEnemyCore.position);

        // Use the pathfinding movement component to move towards the enemy
        //pathfindingMovement.Move(direction * chaseSpeed);

        // Check if the enemy is within attack range
        float distanceToEnemyCore = Vector3.Distance(transform.position, targetEnemyCore.position);
        if (distanceToEnemyCore <= attackRange)
        {
            // Transition to Attack state
            state = State.AttackEnemyCore;
        }
    }
    
    private void AttackEnemy()
{
    if (targetEnemy == null || targetEnemyStats == null)
    {
        // Enemy or enemy's stats lost, transition back to Idle state
        state = State.Idle;
        return;
    }

    // Rotate towards the enemy
    Vector3 direction = targetEnemy.position - transform.position;
    direction.y = 0f;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    if (Time.time >= nextAttackTime)
    {
        // Deal damage to the enemy
        targetEnemyStats.TakeDamage(attackDamage);

        // Reset the attack cooldown
        nextAttackTime = Time.time + attackCooldown;
    }

    // Check if the enemy is out of attack range
    float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);
    if (distanceToEnemy > attackRange)
    {
        // Enemy out of range, transition back to Chase state
        state = State.Chase;
    }
}

    private void AttackEnemyCore()
    {
        if (targetEnemyCore == null || targetEnemyStats == null)
        {
            // Enemy or enemy's stats lost, transition back to Idle state
            state = State.Idle;
            return;
        }

        // Rotate towards the enemy
        Vector3 direction = targetEnemyCore.position - transform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (Time.time >= nextAttackTime)
        {
            // Deal damage to the enemy
            targetEnemyStats.TakeDamage(attackDamage);

            // Reset the attack cooldown
            nextAttackTime = Time.time + attackCooldown;
        }

        // Check if the enemy is out of attack range
        float distanceToEnemyCore = Vector3.Distance(transform.position, targetEnemyCore.position);
        if (distanceToEnemyCore > attackRange)
        {
            // Enemy out of range, transition back to Chase state
            state = State.Chase;
        }
    }
}


