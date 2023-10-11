using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AreaOfDamage : MonoBehaviour
{
    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float damageRefresh = 1;
    [SerializeField]
    private float slowEffect = 0;
    [SerializeField]
    private bool autoAttack;
    [SerializeField]
    private CharType user = CharType.Default;

    private HashSet<GameObject> damaging = new HashSet<GameObject>();

    private void Awake()
    {
        var area = this.GetComponent<Collider2D>();
        if (area == null)
        {
            throw new System.NotImplementedException("No area implemented to do damage!!!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            damaging.Add(collision.gameObject);
            character.AlterMoveSpeed(1 - slowEffect);
            StartCoroutine(DamageTicks(collision.gameObject, character));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        damaging.Remove(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.gameObject.GetComponent<Character>();
        if (character != null)
        {
            damaging.Add(collision.gameObject);
            character.AlterMoveSpeed(1 - slowEffect);
            StartCoroutine(DamageTicks(collision.gameObject, character));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        damaging.Remove(collision.gameObject);
    }


    #region Setter/Getter
    public float GetAttackDamage() {  return damage; }

    public float GetAttackRange() { return attackRange; }

    public float GetAttackTime() { return attackTime; }

    public float GetAttackDelay() { return attackDelay; }
    public CharType GetUserType() { return user; }

    public bool IsAutoAttack() { return autoAttack; }

    public void SetUserType(CharType userRequest)
    {
        if(userRequest == CharType.Default)
        {
            throw new System.ArgumentException("Set User Type as Default which is invalid.");
        }
        else
        {
            user = userRequest;
        }
    }
    #endregion

    IEnumerator DamageTicks(GameObject charObj, Character character)
    {
        Debug.Log("DamageTick Called for " + charObj.tag);
        character.TakeDamage(user, damage);
        yield return new WaitForSeconds(damageRefresh);
        if (damaging.Contains(charObj))
        {
            StartCoroutine(DamageTicks(charObj, character));
        }
    }
}
