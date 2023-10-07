using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField]
    protected float health = 1;
    [SerializeField]
    protected float maxHealth = 1;
    [SerializeField]
    protected float meleeDamage = 1;
    [SerializeField]
    protected float collideDamage = 0;
    [SerializeField]
    protected float movementSpeed = 5;
    [SerializeField]
    protected float currMovementSpeed = 5;
    [SerializeField]
    protected float movementSmoothTime = 0.05f;

    private Vector3 smoothedVelocityVelocity;
    protected Rigidbody2D rb;
    public CharType type;

    protected virtual void Awake()
    {
        // GetComponent allows Character to interact with the Character's Rigidbody
        // Character and Rigidbody *must* be on the same GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        // Read, modify, apply pattern
        // Note: Modifying physics typically is done in FixedUpdate
        
        // --- Read ---
        var targetVelocity = GetMovementInput();
        
        // --- Modify ---
        // Multiply by movement speed.
        targetVelocity *= currMovementSpeed;
        
        // --- Apply ---
        // Smoothly apply target velocity
        // This allows movement input to be responsive and smooth
        // Alternatively, can use drag and AddForce, but I find SmoothDamp to feel better
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref smoothedVelocityVelocity, movementSmoothTime);
    }

    // Allows subclasses of Character to control movement input
    // For the player, this means the player can control the character using the keyboard
    // For the enemies, this means the AI can control the character
    protected abstract Vector2 GetMovementInput();

    // Other classes can call this method to damage this character
    public abstract void TakeDamage(CharType type, float damage);

    public void AlterMoveSpeed(float timesFaster)
    {
        currMovementSpeed = timesFaster * movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var temp = collision.gameObject.GetComponent<Character>();
        if (temp != null)
        {
            collision.gameObject.GetComponent<Character>().TakeDamage(type, collideDamage);
        }
    }
}
