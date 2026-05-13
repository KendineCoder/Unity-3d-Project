using UnityEngine;

public abstract class AttackSystem : ScriptableObject
{
    [Header("Base Settings")]
    public float attackDuration = 1.0f;
    public float attackSpeed = 1.0f;
    public float spineSmoothSpeed = 20f;

    // This class holds variables that change per character
    public class WeaponState
    {
        public Vector2 wandOffset;
        public Transform characterTransform;
        public Vector3 startPosition;
        public bool hasHitTarget = false;
        public bool isHoldingAttack; 
        public float attackTimer;
        public bool isTimerActive;
        public int currentDirection;
        public bool isBlocking;
        public Quaternion lastSpineRotation;
        public float currentSpineX;

        public Transform wandIKTarget;
    }
    public abstract void ProcessHit(WeaponState state, Animator attackerAnim,Transform attackerPos ,Collider attackerCollider, GameObject victim);
    public abstract void Initialize(WeaponState state, Animator anim);
    public abstract void OnAttackStart(WeaponState state, Animator anim, float mx, float my);
    public abstract void OnAttackUpdate(WeaponState state, Animator anim, Collider weaponCollider);
    public abstract void OnAttackEnd(WeaponState state, Animator anim);
    public abstract void OnBlockStart(WeaponState state, Animator anim, float mx, float my);
    public abstract void OnBlockEnd(WeaponState state, Animator anim);
    public abstract void UpdateSpineRotation(WeaponState state, Animator anim, Transform spine, float angle, float mousePitch, float mouseYaw);
}