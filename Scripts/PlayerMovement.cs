using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour, IDamageable
{

    [Header("Inventory")]
    public AttackSystem[] availableWeapons; 
    private int currentWeaponIndex = 0;
    public WandTracker wandTracker;
    public float health = 100f;

    [SerializeField] private Animator animator;

    public RigBuilder RigB;

    private Rigidbody rb;
    private Vector3 inputDirection;
    public float walkSpeed = 2.5f;

    [Header("References")]
    public Transform spine;
    public Transform mnCamera;
    public Collider weaponCollider; 
    public Transform anchor;
    [Header("Attack System")]
    public AttackSystem currentStrategy;
    private AttackSystem.WeaponState weaponState = new AttackSystem.WeaponState();

    public float attackRate = 0.3f;
    private float nextAttackTime = 0f;
    public WeaponHitbox hitbox;
    // Camera Input
    private float rawMouseYaw = 0f;
    private float rawMousePitch = 0f;
    public float mouseSensitivity = 240f;
    private float rawMouseAngle;
    public Transform weaponHandSlot;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        if (currentStrategy != null)
        {
            currentStrategy.Initialize(weaponState, animator);
        }
        if (hitbox != null)
        {
            hitbox.system = currentStrategy; 
            hitbox.ownerState = weaponState; 
            hitbox.ownerAnimator = animator;
            hitbox.myCollider = weaponCollider;
            hitbox.myTransform = transform;
        }
        weaponState.characterTransform = anchor;
        weaponState.wandOffset = new Vector3(1, 0.5f, 10);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % availableWeapons.Length;
            SwitchWeapon(availableWeapons[currentWeaponIndex]);
        }
        HandleMouseLookInput();
        HandleCombatInput();
        HandleMovementInput();
    }

    private void HandleMouseLookInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rawMouseAngle += (mouseX + mouseY) * mouseSensitivity * Time.deltaTime;
        rawMouseYaw += mouseX * mouseSensitivity * Time.deltaTime;
        rawMousePitch -= mouseY * mouseSensitivity * Time.deltaTime;
        rawMousePitch = Mathf.Clamp(rawMousePitch, -80, 80);
    }

    private void HandleCombatInput()
    {
        if (currentStrategy == null) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // --- Block Logic ---
        if (Input.GetMouseButtonDown(1))
        {
           
            if (weaponState.isTimerActive)
            {
                animator.SetBool("isAttacking", false);
                weaponState.isTimerActive = false;
            }
            currentStrategy.OnBlockStart(weaponState, animator, mouseX, mouseY);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            currentStrategy.OnBlockEnd(weaponState, animator);
        }

        // --- Attack Logic ---
        if (Input.GetMouseButtonDown(0) && !animator.GetBool("isAttacking")) // hashle ya da twohandede al
        {   
            weaponState.isHoldingAttack = true;
            StartAttacking(mouseX, mouseY);
        }
        else if (Input.GetMouseButton(0) && weaponState.isBlocking && !weaponState.isTimerActive)
        {
            StartAttacking(mouseX, mouseY);
        }


        currentStrategy.OnAttackUpdate(weaponState, animator, weaponCollider);

        if (Input.GetMouseButtonUp(0))
        {
            weaponState.isHoldingAttack = false;
            currentStrategy.OnAttackEnd(weaponState, animator);
        }
    }

    private void StartAttacking(float mx, float my)
    {
        nextAttackTime = Time.time + attackRate;
        currentStrategy.OnAttackStart(weaponState, animator, mx, my);
    }

    private void HandleMovementInput()
    {
        float rawX = Input.GetAxisRaw("Horizontal");
        float rawZ = Input.GetAxisRaw("Vertical");

        float animX = rawX;
        if (weaponState.isTimerActive && rawX <= 0.0f)
        {
            animX = Mathf.Abs(rawX);
        }

        animator.SetFloat("InputX", animX, 0.15f, Time.deltaTime);
        animator.SetFloat("InputZ", rawZ, 0.15f, Time.deltaTime);

        inputDirection = transform.forward * rawZ + transform.right * rawX;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputDirection.normalized * walkSpeed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        if (currentStrategy != null)
        {
            currentStrategy.UpdateSpineRotation(weaponState, animator, spine, rawMouseAngle, rawMousePitch,rawMouseYaw);
        }
        transform.rotation = Quaternion.Euler(0f, mnCamera.rotation.eulerAngles.y, 0f);
    
    }

    public void SwitchWeapon(AttackSystem newStrategy)
    {
        if (newStrategy == null) return;

        currentStrategy = newStrategy;

        weaponState = new AttackSystem.WeaponState();
        
        weaponState.wandOffset = new Vector3(1, 0.5f, 10);

   
        currentStrategy.Initialize(weaponState, animator);
        bool isWand = newStrategy is MagicWand;
        if (wandTracker != null)
        {
            wandTracker.enabled = isWand;
            // Clean up the trail if we are switching away from the wand
            if (!isWand && wandTracker.wandTrail != null)
            {
                wandTracker.wandTrail.emitting = false;
            }
        }
        if (hitbox != null)
        {
            hitbox.system = currentStrategy;
            hitbox.ownerState = weaponState;
            hitbox.ownerAnimator = animator;
            hitbox.myCollider = weaponCollider;
        }
        if (newStrategy.name.Contains("wand"))
        {
            RigB.enabled = true;
        }
        else
        {
            RigB.enabled = false;

        }
        UpdateWeaponModel(newStrategy);
        Debug.Log($"Switched to: {newStrategy.name}");
    }
    private void UpdateWeaponModel(AttackSystem strategy)
    {
    
        foreach (Transform child in weaponHandSlot)
        {

            bool isMatch = child.name == strategy.name;
            child.gameObject.SetActive(isMatch);

            if (isMatch)
            {
                weaponCollider = child.GetComponent<Collider>();
            }
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Player Health: " + health);
    }

    public int IsBlocking()
    {
        if (weaponState.isBlocking)
        {
            return weaponState.currentDirection;
        }
        return -1;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    public bool IsAttacking()
    {
        return weaponState.isTimerActive && !weaponState.isBlocking;
    }

    public int GetCurrentAttackDirection()
    {

        return weaponState.currentDirection;
    }

    public void OnSuccessfulBlock()
    {
        Debug.Log("<color=cyan>OYUNCU BAŞARIYLA BLOK YAPTI! (PARRY)</color>");
    }
}