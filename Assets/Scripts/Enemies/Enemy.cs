using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 10f; // �������� �����
    public Slider healthBarPrefab; // ������ ������ �������� (UI)
    private Slider healthBar;      // ������� ������ ��������
    private Transform player;      // ������ �� ������
    public float separationDistance = 1f; // ����������� ���������� ����� �������
    public float separationForce = 2f;    // ���� ������������
    private bool isVisibleToPlayer = false; // ���� ��������� ��� ������
    public float attackRange = 1f; // ������ �����
    public float attackDamage = 5f; // ���� �� �����
    private float attackCooldown = 1f; // ����������� ����� (� ��������)
    private float lastAttackTime = 0f; // ����� ��������� �����
    public Sprite ammoSprite; // ������ �� ������ �������� (����� �������� ��� ��������)
    public GameObject ammoPickupPrefab; // ������ �������� ��� �����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ������� ������ �������� ��� ������� �����
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
                    healthBar.gameObject.SetActive(false); // �������� ������ �������� ��� ������
                    Debug.Log("������ �������� ������� ��� �����: " + gameObject.name);
                }
                else
                {
                    Debug.LogError("�� ������� �������� ��������� Slider ��� ������ ��������!");
                }
            }
            else
            {
                Debug.LogError("UI_Canvas �� ������!");
            }
        }
        else
        {
            Debug.LogError("healthBarPrefab �� �������� � �����!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance < 5f) // ���� ���������
            {
                // ���� ����� ������
                if (!isVisibleToPlayer && healthBar != null)
                {
                    healthBar.gameObject.SetActive(true);
                    isVisibleToPlayer = true;
                }

                // �������� � ������
                Vector2 direction = (player.position - transform.position).normalized;
                Vector2 moveDirection = direction;

                // ������������ �� ������ ������
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

                // ������� ����� � ����������� �� ����������� � ������
                if (direction.x != 0)
                {
                    float scaleX = Mathf.Sign(direction.x); // 1 ��� �������� ������, -1 ��� �����
                    transform.localScale = new Vector3(scaleX, 1, 1); // ������� ����� � ������, ��� ��������
                }

                // ����� ������
                if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }

                // ��������� ������� ������ �������� ��� ������
                if (healthBar != null)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1f, 0));
                    healthBar.transform.position = screenPos;
                }
            }
            else
            {
                // ���� ��� ���� ���������
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
            Debug.Log("���� �������� ������! ����: " + attackDamage);
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
        // ���� �������� �� �����
        if (ammoPickupPrefab != null)
        {
            Instantiate(ammoPickupPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("AmmoPickupPrefab not assigned in Enemy!");
        }
        if (healthBar != null) Destroy(healthBar.gameObject); // ������� ������ ��������
        Destroy(gameObject);
    }
}