using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // ������ �������� ���������
    public int itemCount = 9; // ���������� ��������� ��� ������
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // ������ ������� ������ (������, ������)
    public LayerMask groundLayer; // ���� ��� �������� ������������ � ������

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

            // �����������: �������� ��������� ��� ������ ������� (���� ��� �� ��������)
            // spawnedItem.AddComponent<PickupItem>();
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition;
        int maxAttempts = 10; // �������� ������� ����� ���������� �������
        int attempts = 0;

        do
        {
            // ���������� ��������� ������� � �������� spawnAreaSize
            spawnPosition = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            ) + (Vector2)transform.position; // ������� ������������ ������� ��������

            // ���������, �� ������������ �� ������� � ������� ��������� (��������, �������)
            if (Physics2D.OverlapCircle(spawnPosition, 0.5f, groundLayer) == null)
            {
                return spawnPosition;
            }
            attempts++;
        } while (attempts < maxAttempts);

        // ���� �� ������� ����� �������, ���������� ��������� �������
        return spawnPosition;
    }

    // �����������: ������������ ������� ������ � ���������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1f));
    }
}