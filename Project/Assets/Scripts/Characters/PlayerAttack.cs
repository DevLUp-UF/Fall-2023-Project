using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private InputActionReference attack1;
    [SerializeField]
    private InputActionReference attack2;

    [SerializeField]
    private PlayerAim playerAim;

    [FormerlySerializedAs("arrow")]
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private GameObject attackArea;
    [SerializeField]
    private float attackTime = 0.25f;
    private bool isAttacking = false;

    private void Start()
    {
        attackArea.SetActive(false);
    }

    private void Update()
    {
        if(attack1.action.IsPressed() && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if(attack2.action.IsPressed() && !isAttacking)
        {
            StartCoroutine(AttackArrow());
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("Swing");
        isAttacking = true;
        attackArea.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        Debug.Log("Finish");
        isAttacking = false;
        attackArea.SetActive(false);
    }
    
    IEnumerator AttackArrow()
    {
        Debug.Log("Fire");
        isAttacking = true;

        var arrow = Instantiate(arrowPrefab);
        var aimDirection = (playerAim.AimWorldPosition - transform.position).normalized;

        // Move arrow to be in front of player
        arrow.transform.position = transform.position + aimDirection * 1.25f;

        // Calculate rotation based on player aim
        arrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);

        yield return new WaitForSeconds(attackTime);

        Debug.Log("Finish");
        isAttacking = false;
        attackArea.SetActive(false);
    }
}
