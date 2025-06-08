using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5f; // Урон от пули

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Наносим урон
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}