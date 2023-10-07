using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField]
    private InputActionReference ranged;

    [SerializeField]
    private PlayerAim playerAim;

    [FormerlySerializedAs("arrow")]
    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private float attackTime = 0.25f;
    private bool isAttacking = false;

    private void Update()
    {
        if(ranged.action.IsPressed() && !isAttacking)
        {
            StartCoroutine(AttackArrow());
        }
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
    }
}
