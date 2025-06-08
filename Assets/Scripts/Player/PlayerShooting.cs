using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float shootRange = 3f; // Радиус стрельбы
    private Transform player;
    public GunController gun; // Ссылка на автомат

    void Start()
    {
        player = transform;
        gun = transform.Find("Gun").GetComponent<GunController>(); // Находим автомат
        if (gun == null)
        {
            Debug.LogError("PlayerShooting: GunController not found!");
        }
    }

    void Update()
    {
        // Постоянное наведение на ближайшего врага
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
            gun.RotateToTarget(closestEnemy.transform); // Наводим автомат
        }
    }

    public void Fire()
    {
        if (InventoryManager.Instance != null && gun != null)
        {
            if (InventoryManager.Instance.GetAmmoCount() > 0)
            {
                // Ищем ближайшего врага для стрельбы
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
                    Transform target = closestEnemy.transform;
                    gun.Shoot(target); // Стреляем только по кнопке
                    Debug.Log("Выстрел по врагу!");
                }
                else
                {
                    Debug.Log("Нет врагов в зоне досягаемости!");
                }
            }
            else
            {
                Debug.Log("Нет патронов!");
            }
        }
    }
}