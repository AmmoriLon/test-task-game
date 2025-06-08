using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float shootRange = 3f; // ������ ��������
    private Transform player;
    public GunController gun; // ������ �� �������

    void Start()
    {
        player = transform;
        gun = transform.Find("Gun").GetComponent<GunController>(); // ������� �������
        if (gun == null)
        {
            Debug.LogError("PlayerShooting: GunController not found!");
        }
    }

    void Update()
    {
        // ���������� ��������� �� ���������� �����
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
            gun.RotateToTarget(closestEnemy.transform); // ������� �������
        }
    }

    public void Fire()
    {
        if (InventoryManager.Instance != null && gun != null)
        {
            if (InventoryManager.Instance.GetAmmoCount() > 0)
            {
                // ���� ���������� ����� ��� ��������
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
                    gun.Shoot(target); // �������� ������ �� ������
                    Debug.Log("������� �� �����!");
                }
                else
                {
                    Debug.Log("��� ������ � ���� ������������!");
                }
            }
            else
            {
                Debug.Log("��� ��������!");
            }
        }
    }
}