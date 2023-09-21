public class PlayerCharacter : Character
{
    public override void TakeDamage(CharType type, int damage)
    {
        if (type == CharType.Enemy)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
                Destroy(gameObject);
            }
        }
    }
}
