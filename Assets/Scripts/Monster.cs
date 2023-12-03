using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterState
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

    protected int hp = 100;
    protected float speed = 4f;
    protected float attackRange = 5f;
    protected float chaseRange = 10f;
    protected float patrolRange = 5f;
    public MonsterState monsterState;
    public GameObject player;
    protected Vector2 targetPosition;

    void Start()
    {
        monsterState = MonsterState.Patrol;
        StartCoroutine(UpdatePatrolTarget());
    }

    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Patrol:
                Patrol();
                break;
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Chase:
                Chase();
                break;
            case MonsterState.Dead:
                break;
        }
    }

    void Patrol()
    {
        Vector2 playerPosition = player.transform.position;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

        if (distanceToPlayer < chaseRange)
        {
            // If the player is within the attack range, start attacking
            monsterState = MonsterState.Chase;
        }
        // Move towards the target position
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        // Check if the monster has reached the target position
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        if (distanceToTarget < 0.1f)
        {
            // If reached the target, get a new random target position
            StartCoroutine(UpdatePatrolTarget());
        }
    }

    void Chase()
    {
        targetPosition = player.transform.position;
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
        // Check the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer < attackRange)
        {
            // If the player is within the attack range, start attacking
            monsterState = MonsterState.Attack;
        }
        else if (distanceToPlayer > chaseRange)
        {
            // If the player is outside the chase range, go back to patrolling
            monsterState = MonsterState.Patrol;
            StartCoroutine(UpdatePatrolTarget());
        }
    }

    void Attack()
    {
        Vector2 playerPosition = player.transform.position;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

        if (distanceToPlayer > attackRange)
        {
            monsterState = MonsterState.Chase;
            StartCoroutine(UpdatePatrolTarget());
        }
    }

    void DelegateAttack() { }

    IEnumerator UpdatePatrolTarget()
    {
        // Generate a new random target position within the patrol range
        targetPosition = new Vector2(
            transform.position.x + Random.Range(-patrolRange, patrolRange),
            transform.position.y + Random.Range(-patrolRange, patrolRange)
        );

        // Wait for a random amount of time before generating the next target
        float patrolWaitTime = Random.Range(1f, 3f);
        yield return new WaitForSeconds(patrolWaitTime);

        // If the current state is still Patrol, update the target and continue patrolling
        if (monsterState == MonsterState.Patrol)
        {
            StartCoroutine(UpdatePatrolTarget());
        }
    }
}
