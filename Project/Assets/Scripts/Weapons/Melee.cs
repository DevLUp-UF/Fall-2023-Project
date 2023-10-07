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
    private float attackTime;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float damage;
    [SerializeField] 
    private bool autoAttack;
    [SerializeField]
    private CharType user;
    [SerializeField]
    private int availableTargets = 1;

    private RaycastHit2D[] hits;

    private void Awake()
    {
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

            Debug.Log("Auto Attack Triggered");
            if (col.gameObject.GetComponent<Character>() != null) 
            {
                Debug.Log("Hit?");
                StartCoroutine(Attack(col.GetComponent<Transform>()));
            }
        }
    }

    IEnumerator Attack(Transform target)
    {
        Debug.Log("Melee Attack Started");
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
        Debug.Log("Melee Attack Ended");
        availableTargets++;
    }
}
