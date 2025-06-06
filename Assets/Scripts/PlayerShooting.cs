using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float shootRange = 3f; // Радиус стрельбы
    private Transform player;

    void Start()
    {
        player = transform;
    }

    public void Fire()
    {
        if (InventoryManager.Instance != null)
        {
            if (InventoryManager.Instance.GetAmmoCount() > 0)
            {
                if (InventoryManager.Instance.UseAmmo(1)) // Тратим 1 патрон
                {
                    // Ищем ближайшего врага
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    GameObject closestEnemy = null;
                    float closestDistance = shootRange;

                    foreach (GameObject enemy in enemies)
                    {
                        float distance = Vector2.Distance(player.position, enemy.transform.position);
                        if (distance <= shootRange && distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestEnemy = enemy;
                        }
                    }

                    if (closestEnemy != null)
                    {
                        Enemy enemyScript = closestEnemy.GetComponent<Enemy>();
                        if (enemyScript != null)
                        {
                            enemyScript.TakeDamage(5f); // Наносим 5 урона
                            Debug.Log("Попадание по врагу!");
                        }
                    }
                    else
                    {
                        Debug.Log("Нет врагов в зоне досягаемости!");
                    }
                }
                else
                {
                    Debug.Log("Не удалось выстрелить: недостаточно патронов!");
                }
            }
            else
            {
                Debug.Log("Нет патронов!");
            }
        }
    }
}