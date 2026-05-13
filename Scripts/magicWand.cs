using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "NewWand", menuName = "Combat/Attack Strategy/Wand")]
public class MagicWand : AttackSystem
{

    
    public override void OnAttackUpdate(WeaponState state, Animator anim, Collider weaponCollider)
    {
        state.attackTimer += Time.deltaTime * attackSpeed;
        float normalizedTime = Mathf.Clamp01(state.attackTimer / attackDuration);



        if (normalizedTime >= 1.0f && state.isTimerActive)
        {
            state.isTimerActive = false;
            anim.SetBool("isAttacking", false);
  
        }
    }

    public override void UpdateSpineRotation(WeaponState state, Animator anim, Transform spine, float angle, float mousePitch, float mouseYaw)
    {
        if (state.wandIKTarget == null) return;

        Camera cam = Camera.main;

        Plane plane = new Plane(-cam.transform.forward,cam.transform.position + cam.transform.forward * 3.0f);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        float enter;

        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            state.wandIKTarget.position = hitPoint;
        }
    }


    public override void OnAttackStart(WeaponState state, Animator anim, float mx, float my)
    {
        state.attackTimer = 0;
        state.isTimerActive = true;
        anim.SetBool("isAttacking", true);
    }
    public override void Initialize(WeaponState state, Animator anim)
    {
        
        GameObject target = GameObject.FindWithTag("WandIKTarget");
        if (target != null)
        {
            state.wandIKTarget = target.transform;
        }
        
    }
    public override void OnAttackEnd(WeaponState state, Animator anim) { }
    public override void OnBlockStart(WeaponState state, Animator anim, float mx, float my) { }
    public override void OnBlockEnd(WeaponState state, Animator anim) { }
    public override void ProcessHit(WeaponState state, Animator attackerAnim, Transform attackerPos, Collider attackerCollider, GameObject victim) { }
}
