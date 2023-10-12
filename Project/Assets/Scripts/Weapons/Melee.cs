using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Melee : MonoBehaviour
{
    [SerializeField]
    private InputActionReference melee; 
    [SerializeField]
    private PlayerAim playerAim;
    [SerializeField]
    private GameObject meleeArea;
    [SerializeField]
    private CharType user;
    [SerializeField]
    private int availableTargets = 1;

    private RaycastHit2D[] hits;
    private float attackTime;
    private float attackRange;
    private float attackDelay;
    private float damage;
    private bool autoAttack;

    private void Awake()
    {
        try
        {
            //Set melee strength to the strength of the weapon
            AreaOfDamage damageObject = meleeArea.GetComponent<AreaOfDamage>();
            attackTime = damageObject.GetAttackTime();
            attackRange = damageObject.GetAttackRange();
            attackDelay = damageObject.GetAttackDelay();
            damage = damageObject.GetAttackDamage();
            autoAttack = damageObject.IsAutoAttack();

            //So weapons can be for any user type without needing different prefabs
            if (user != damageObject.GetUserType())
            {
                damageObject.SetUserType(user);
            }
        }
        catch (System.Exception)
        {
            throw new System.NotImplementedException("Damage Area does not have Area of Damage to make it work.");
        }


        //Hardcoded lower limit to auto-attack range
        if(attackRange < 0.5f) 
        {
            attackRange = 0.5f;
        }

        meleeArea.SetActive(false);
        var meleeDetector = this.gameObject.GetComponent<Collider2D>();
        if (autoAttack)
        {
            if(meleeDetector == null || !meleeDetector.isTrigger) 
            {
                throw new System.NotImplementedException("Auto Attack not Implemented correctly!!");
            }
        }
        else
        {

            if (meleeDetector != null)
            {
                meleeDetector.enabled = false;
            }
        }

        meleeArea.SetActive(false);
    }

    private void Update()
    {
        if (!autoAttack && melee.action.IsPressed() && availableTargets > 0)
        {
            StartCoroutine(Attack(this.transform));
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(availableTargets > 0 && col.gameObject != null) 
        {
            Character target = null;
            if (col.gameObject.tag == "player" && user != CharType.Player)
            {
                target = col.gameObject.GetComponentInParent<Character>();
            }
            else
            {
                target = col.gameObject.GetComponent<Character>();
            }

            //Debug.Log("Auto Attack Triggered");
            if (col.gameObject.GetComponent<Character>() != null) 
            {
                //Debug.Log("Hit?");
                StartCoroutine(Attack(col.GetComponent<Transform>()));
            }
        }
    }

    IEnumerator Attack(Transform target)
    {
        //Debug.Log("Melee Attack Started");
        var meleeEffect = Instantiate(meleeArea);

        if (autoAttack)
        {
            meleeEffect.transform.position = target.position;
        }
        else
        {
            var aimDirection = (playerAim.AimWorldPosition);
            var maxReach = (playerAim.AimWorldPosition - target.transform.position).normalized * attackRange;
            //                                               logic statement               ?  (if true)   : (if false)
            meleeEffect.transform.position = (aimDirection.magnitude < maxReach.magnitude) ? aimDirection : maxReach;
        }


        meleeEffect.SetActive(true);
        availableTargets--;
        yield return new WaitForSeconds(attackTime);
        Destroy(meleeEffect);
        //Debug.Log("Melee Attack Ended");
        yield return new WaitForSeconds(attackDelay);
        availableTargets++;
    }
}
