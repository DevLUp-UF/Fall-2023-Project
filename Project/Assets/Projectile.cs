using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
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
        if (col.rigidbody && col.rigidbody.TryGetComponent(out Character character))
        {
            character.TakeDamage(CharType.Player, damage);
        }
        
        Destroy(gameObject);
    }
}
