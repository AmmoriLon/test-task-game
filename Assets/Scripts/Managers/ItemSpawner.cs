using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Массив префабов предметов
    public int itemCount = 9; // Количество предметов для спавна
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Размер области спавна (ширина, высота)
    public LayerMask groundLayer; // Слой для проверки столкновений с землей

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < itemCount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            int randomItemIndex = Random.Range(0, itemPrefabs.Length);
            GameObject spawnedItem = Instantiate(itemPrefabs[randomItemIndex], spawnPosition, Quaternion.identity);

            // Опционально: добавить компонент для логики подбора (если еще не добавлен)
            // spawnedItem.AddComponent<PickupItem>();
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition;
        int maxAttempts = 10; // Максимум попыток найти подходящую позицию
        int attempts = 0;

        do
        {
            // Генерируем случайную позицию в пределах spawnAreaSize
            spawnPosition = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            ) + (Vector2)transform.position; // Смещаем относительно позиции спавнера

            // Проверяем, не пересекается ли позиция с другими объектами (например, стенами)
            if (Physics2D.OverlapCircle(spawnPosition, 0.5f, groundLayer) == null)
            {
                return spawnPosition;
            }
            attempts++;
        } while (attempts < maxAttempts);

        // Если не удалось найти позицию, возвращаем последнюю попытку
        return spawnPosition;
    }

    // Опционально: визуализация области спавна в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1f));
    }
}