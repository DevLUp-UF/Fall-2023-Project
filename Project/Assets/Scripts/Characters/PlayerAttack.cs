using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow;
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
        if(Input.GetMouseButtonDown(0) && !isAttacking) 
        {
            StartCoroutine(Attack());
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isAttacking) 
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
        Instantiate(arrow);
        yield return new WaitForSeconds(attackTime);
        Debug.Log("Finish");
        isAttacking = false;
        attackArea.SetActive(false);
    }
}
