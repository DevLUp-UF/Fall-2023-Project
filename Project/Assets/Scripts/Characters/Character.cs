using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField]
    protected float health = 1;
    [SerializeField]
    protected float maxHealth = 1;
    [SerializeField]
    protected float attack = 0;

    [SerializeField]
    protected float movementSpeed = 5;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        // GetComponent allows Character to interact with the Character's Rigidbody
        // Character and Rigidbody *must* be on the same GameObject
        rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        // Read, modify, apply pattern
        // Note: Modifying physics typically is done in FixedUpdate
        
        // --- Read ---
        var movement = GetMovementInput();
        
        // --- Modify ---
        // Multiply by movement speed.
        movement *= movementSpeed;
        
        // Multiply by drag to counteract drag. This allows drag to control how responsive the character is.
        // Lower drag makes the character feel more slippery, as if on ice.
        movement *= rb.drag;
        
        // --- Apply ---
        rb.AddForce(movement, ForceMode.VelocityChange);
    }

    // Allows subclasses of Character to control movement input
    // For the player, this means the player can control the character using the keyboard
    // For the enemies, this means the AI can control the character
    protected abstract Vector2 GetMovementInput();

    // Other classes can call this method to damage this character
    public abstract void TakeDamage(CharType type, int damage);
}
