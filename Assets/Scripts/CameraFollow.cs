using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ссылка на игрока
    public float smoothSpeed = 0.5f; // Увеличенная скорость сглаживания
    public Vector3 offset; // Смещение камеры
    public Vector2 minBounds; // Минимальные границы (левый нижний угол)
    public Vector2 maxBounds; // Максимальные границы (правый верхний угол)
    public Vector2 boundsBuffer = new Vector2(1f, 1f); // Буфер для границ (чтобы камера не обрезала края)

    void Start()
    {
        // Установи начальные границы (например, -5, -5 до 5, 5)
        minBounds = new Vector2(-9f, -10f);
        maxBounds = new Vector2(22f, 3f);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Вычисляем желаемую позицию камеры
            Vector3 desiredPosition = target.position + offset;

            // Ограничиваем позицию камеры с буфером
            float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + boundsBuffer.x, maxBounds.x - boundsBuffer.x);
            float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + boundsBuffer.y, maxBounds.y - boundsBuffer.y);
            Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

            // Сглаживаем движение
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}