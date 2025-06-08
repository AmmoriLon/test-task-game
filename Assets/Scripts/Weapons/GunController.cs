using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint; // Точка вылета пули
    public GameObject bulletPrefab; // Префаб пули
    public float bulletSpeed = 10f; // Скорость пули
    public float fireRate = 0.5f; // Скорость стрельбы (секунд между выстрелами)
    private float nextFireTime; // Время следующего выстрела

    void Start()
    {
        if (firePoint == null)
        {
            firePoint = transform; // По умолчанию центр автомата
        }
    }

    // Наведение на цель с учетом зеркалирования
    public void RotateToTarget(Transform target)
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float parentScaleX = transform.parent.localScale.x; // Получаем масштаб родителя (Player)

            // Корректируем угол в зависимости от зеркалирования
            if (parentScaleX < 0)
            {
                angle = angle + 180f; // Инвертируем угол, если персонаж зеркалирован
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 5f);
        }
    }

    // Стрельба по цели (вызывается по кнопке)
    public void Shoot(Transform target)
    {
        if (target != null && Time.time >= nextFireTime && InventoryManager.Instance.UseAmmo(1))
        {
            nextFireTime = Time.time + fireRate;
            Vector2 direction = (target.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float parentScaleX = transform.parent.localScale.x;

            // Корректируем угол для стрельбы и пули
            if (parentScaleX < 0)
            {
                angle = angle + 180f; // Инвертируем направление при зеркалировании
            }
            // Корректировка для спрайта пули, направленного вверх
            angle = angle + 90f; // Добавляем 90°, чтобы пуля смотрела вверх по направлению движения

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed; // Пуля летит к цели
                rb.gravityScale = 0f; // Убеждаемся, что гравитация выключена (ты уже сделал это)
            }
            Destroy(bullet, 2f); // Уничтожаем пулю через 2 секунды
        }
    }
}