using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint; // ����� ������ ����
    public GameObject bulletPrefab; // ������ ����
    public float bulletSpeed = 10f; // �������� ����
    public float fireRate = 0.5f; // �������� �������� (������ ����� ����������)
    private float nextFireTime; // ����� ���������� ��������

    void Start()
    {
        if (firePoint == null)
        {
            firePoint = transform; // �� ��������� ����� ��������
        }
    }

    // ��������� �� ���� � ������ ��������������
    public void RotateToTarget(Transform target)
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float parentScaleX = transform.parent.localScale.x; // �������� ������� �������� (Player)

            // ������������ ���� � ����������� �� ��������������
            if (parentScaleX < 0)
            {
                angle = angle + 180f; // ����������� ����, ���� �������� ������������
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 5f);
        }
    }

    // �������� �� ���� (���������� �� ������)
    public void Shoot(Transform target)
    {
        if (target != null && Time.time >= nextFireTime && InventoryManager.Instance.UseAmmo(1))
        {
            nextFireTime = Time.time + fireRate;
            Vector2 direction = (target.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float parentScaleX = transform.parent.localScale.x;

            // ������������ ���� ��� �������� � ����
            if (parentScaleX < 0)
            {
                angle = angle + 180f; // ����������� ����������� ��� ��������������
            }
            // ������������� ��� ������� ����, ������������� �����
            angle = angle + 90f; // ��������� 90�, ����� ���� �������� ����� �� ����������� ��������

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed; // ���� ����� � ����
                rb.gravityScale = 0f; // ����������, ��� ���������� ��������� (�� ��� ������ ���)
            }
            Destroy(bullet, 2f); // ���������� ���� ����� 2 �������
        }
    }
}