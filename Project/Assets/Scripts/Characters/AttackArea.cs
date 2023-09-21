using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField]
    private float attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<Character>();
        if (target != null)
        {
            Debug.Log("Attack");
            target.TakeDamage(CharType.Player, attack);
        }
    }
}
