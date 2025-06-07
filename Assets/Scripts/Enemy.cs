using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 10f; // Здоровье врага
    public Slider healthBarPrefab; // Префаб полосы здоровья (UI)
    private Slider healthBar;      // Инстанс полосы здоровья
    private Transform player;      // Ссылка на игрока
    public float separationDistance = 1f; // Минимальное расстояние между врагами
    public float separationForce = 2f;    // Сила отталкивания
    private bool isVisibleToPlayer = false; // Флаг видимости для игрока
    public float attackRange = 1f; // Радиус атаки
    public float attackDamage = 5f; // Урон от атаки
    private float attackCooldown = 1f; // Перезарядка атаки (в секундах)
    private float lastAttackTime = 0f; // Время последней атаки
    public Sprite ammoSprite; // Ссылка на спрайт патронов (будут выпадать при убийстве)
    public GameObject ammoPickupPrefab; // Префаб патронов для дропа

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Создаем полосу здоровья для каждого врага
        if (healthBarPrefab != null)
        {
            GameObject canvas = GameObject.Find("UI_Canvas");
            if (canvas != null)
            {
                GameObject healthBarObj = Instantiate(healthBarPrefab.gameObject, canvas.transform);
                healthBar = healthBarObj.GetComponent<Slider>();
                if (healthBar != null)
                {
                    healthBar.maxValue = health;
                    healthBar.value = health;
                    healthBar.gameObject.SetActive(false); // Скрываем полосу здоровья при старте
                    Debug.Log("Полоса здоровья создана для врага: " + gameObject.name);
                }
                else
                {
                    Debug.LogError("Не удалось получить компонент Slider для полосы здоровья!");
                }
            }
            else
            {
                Debug.LogError("UI_Canvas не найден!");
            }
        }
        else
        {
            Debug.LogError("healthBarPrefab не привязан к врагу!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance < 5f) // Зона видимости
            {
                // Враг виден игроку
                if (!isVisibleToPlayer && healthBar != null)
                {
                    healthBar.gameObject.SetActive(true);
                    isVisibleToPlayer = true;
                }

                // Движение к игроку
                Vector2 direction = (player.position - transform.position).normalized;
                Vector2 moveDirection = direction;

                // Отталкивание от других врагов
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    if (enemy != gameObject)
                    {
                        float distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                        if (distToEnemy < separationDistance)
                        {
                            Vector2 repelDirection = (transform.position - enemy.transform.position).normalized;
                            moveDirection += repelDirection * separationForce * (separationDistance - distToEnemy);
                        }
                    }
                }

                transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)moveDirection, 1f * Time.deltaTime);

                // Поворот врага в зависимости от направления к игроку
                if (direction.x != 0)
                {
                    float scaleX = Mathf.Sign(direction.x); // 1 для движения вправо, -1 для влево
                    transform.localScale = new Vector3(scaleX, 1, 1); // Поворот лицом к игроку, без инверсии
                }

                // Атака игрока
                if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }

                // Обновляем позицию полосы здоровья над врагом
                if (healthBar != null)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
                    healthBar.transform.position = screenPos;
                }
            }
            else
            {
                // Враг вне зоны видимости
                if (isVisibleToPlayer && healthBar != null)
                {
                    healthBar.gameObject.SetActive(false);
                    isVisibleToPlayer = false;
                }
            }
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Враг атаковал игрока! Урон: " + attackDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthBar != null) healthBar.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Дроп патронов на землю
        if (ammoPickupPrefab != null)
        {
            Instantiate(ammoPickupPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("AmmoPickupPrefab not assigned in Enemy!");
        }
        if (healthBar != null) Destroy(healthBar.gameObject); // Удаляем полосу здоровья
        Destroy(gameObject);
    }
}