using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField]
    private CharType user = CharType.Obstacle;

    [SerializeField]
    private float speed = 10;
    
    [SerializeField]
    private float damage = 1;
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Character target = null;
        if(col.gameObject.tag == "player" && user != CharType.Player)
        {
            target = col.gameObject.GetComponentInParent<Character>();
        }
        else
        {
            target = col.gameObject.GetComponent<Character>();
        }
        
        if (target != null)
        {
            if (target.type == user)
            {
                return;
            }
            Debug.Log("Shot landed");
            target.TakeDamage(user, damage);
        }
        Destroy(gameObject);
    }
}
