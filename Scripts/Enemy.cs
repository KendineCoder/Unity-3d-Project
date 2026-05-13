using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 50f;
    public Transform playerPos;
    private PlayerController playerController;
    [SerializeField] private Animator animator;

    private NavMeshAgent agent;
    public float attackRange = 2.5f;

    public AttackSystem currentStrategy;
    private AttackSystem.WeaponState weaponState = new AttackSystem.WeaponState();
    public Collider weaponCollider;
    public WeaponHitbox hitbox;

    public float minAttackDelay = 1.5f;
    public float maxAttackDelay = 3.0f;
    private float nextAttackTime = 0f;

    [Range(0, 100)] public float blockChance = 60f;

    private bool isExecutingAction = false;
    private bool isReactingToPlayer = false;

    private readonly Vector2[] directions = {
        new Vector2(1, 0), new Vector2(-1, 0),
        new Vector2(0, 1), new Vector2(0, -1)
    };

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (playerPos != null)
        {
            playerController = playerPos.GetComponent<PlayerController>();
        }

        if (currentStrategy != null) currentStrategy.Initialize(weaponState, animator);
        if (hitbox != null)
        {
            hitbox.system = currentStrategy;
            hitbox.ownerState = weaponState;
            hitbox.ownerAnimator = animator;
            hitbox.myCollider = weaponCollider;
            hitbox.myTransform = transform;
        }

        nextAttackTime = Time.time + Random.Range(1f, 2f);
    }

    void Update()
    {
        if (playerPos == null || playerController == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerPos.position);

        if (!isReactingToPlayer)
        {
            FacePlayer();
        }

        if (distanceToPlayer <= attackRange + 1.0f && !isExecutingAction && !isReactingToPlayer)
        {
            if (playerController.IsAttacking())
            {
                StartCoroutine(DefendRoutine(playerController.GetCurrentAttackDirection()));
                return;
            }
        }

        if (!isReactingToPlayer && !isExecutingAction)
        {
            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                StopAndAttack();
            }
        }

        if (weaponState.isTimerActive)
        {
            currentStrategy.OnAttackUpdate(weaponState, animator, weaponCollider);
        }
    }

    private void FacePlayer()
    {
        Vector3 directionToPlayer = (playerPos.position - transform.position).normalized;
        directionToPlayer.y = 0;
        if (directionToPlayer != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToPlayer), Time.deltaTime * 10f);
        }
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerPos.position);

        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        UpdateAnimator(localVelocity.x, localVelocity.z);
    }

    private void StopAndAttack()
    {
        agent.isStopped = true;
        UpdateAnimator(0f, 0f);

        if (Time.time >= nextAttackTime)
        {
            StartCoroutine(CombatDecisionRoutine());
        }
    }

    private IEnumerator CombatDecisionRoutine()
    {
        isExecutingAction = true;
        agent.isStopped = true;
        UpdateAnimator(0, 0);

        yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));

        int randomAction = Random.Range(0, 100);

        if (randomAction < 33) yield return StartCoroutine(HoldAttack());
        else if (randomAction < 66) yield return StartCoroutine(FeintAttack());
        else yield return StartCoroutine(NormalAttack());

        nextAttackTime = Time.time + Random.Range(minAttackDelay, maxAttackDelay);
        isExecutingAction = false;
    }

    private IEnumerator NormalAttack()
    {
        Vector2 dir = GetRandomDirection();
        currentStrategy.OnAttackStart(weaponState, animator, dir.x, dir.y);
        while (weaponState.isTimerActive) yield return null;
        currentStrategy.OnAttackEnd(weaponState, animator);
    }

    private IEnumerator HoldAttack()
    {
        Vector2 dir = GetRandomDirection();
        weaponState.isHoldingAttack = true;
        currentStrategy.OnAttackStart(weaponState, animator, dir.x, dir.y);

        yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));

        weaponState.isHoldingAttack = false;
        while (weaponState.isTimerActive) yield return null;
        currentStrategy.OnAttackEnd(weaponState, animator);
    }

    private IEnumerator FeintAttack()
    {
        Vector2 fakeDir = GetRandomDirection();
        currentStrategy.OnBlockStart(weaponState, animator, fakeDir.x, fakeDir.y);
        yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
        currentStrategy.OnBlockEnd(weaponState, animator);

        Vector2 realDir = GetRandomDirection();
        currentStrategy.OnAttackStart(weaponState, animator, realDir.x, realDir.y);
        while (weaponState.isTimerActive) yield return null;
        currentStrategy.OnAttackEnd(weaponState, animator);
    }

    private IEnumerator DefendRoutine(int incomingDirection)
    {
        isReactingToPlayer = true;
        isExecutingAction = true;
        agent.isStopped = true;
        UpdateAnimator(0, 0);

        if (Random.Range(0, 100) <= blockChance)
        {
            Vector2 blockInput = GetInputForDirection(incomingDirection);
            currentStrategy.OnBlockStart(weaponState, animator, blockInput.x, blockInput.y);

            while (playerController != null && playerController.IsAttacking()) yield return null;

            currentStrategy.OnBlockEnd(weaponState, animator);
        }
        else
        {
            yield return new WaitForSeconds(0.4f);
        }

        isExecutingAction = false;
        yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));
        isReactingToPlayer = false;
    }

    private Vector2 GetRandomDirection() { return directions[Random.Range(0, directions.Length)]; }

    private Vector2 GetInputForDirection(int dir)
    {
        switch (dir)
        {
            case 1: return new Vector2(1, 0);
            case 2: return new Vector2(-1, 0);
            case 3: return new Vector2(0, 1);
            case 0: return new Vector2(0, -1);
            default: return new Vector2(1, 0);
        }
    }

    private void UpdateAnimator(float x, float z)
    {
        animator.SetFloat("InputX", x, 0.15f, Time.deltaTime);
        animator.SetFloat("InputZ", z, 0.15f, Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }

    public int IsBlocking()
    {
        if (weaponState.isBlocking) return weaponState.currentDirection;
        return -1;
    }

    public Transform GetTransform() { return transform; }
}