public class EnemyCharacter : Character
{
    public override void TakeDamage(CharType type, int damage)
    {
        if (type == CharType.Player)
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
