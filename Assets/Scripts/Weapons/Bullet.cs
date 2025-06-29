using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5f; // ���� �� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // ������� ����
            }
            Destroy(gameObject); // ���������� ����
        }
    }
}