using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int hp = 30;

    public void TakeDamage(int damage)
    {
        hp-=damage;

        if(hp<=0)
        {
            Destroy(gameObject);
        }
    }
}
