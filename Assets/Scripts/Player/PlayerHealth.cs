using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthBar;

    void Start()
    {
        LoadHealth(); // ��������� ����������� �������� ��� ������
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null) healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
        SaveHealth(); // ��������� �������� ��� ��������� �����
    }

    void Die()
    {
        Debug.Log("����� ����!");
        gameObject.SetActive(false); // ��������� ������
        SaveHealth(); // ��������� ��������� ����� ������
    }

    private void SaveHealth()
    {
        PlayerData data = new PlayerData
        {
            health = currentHealth,
            position = new float[] { transform.position.x, transform.position.y }
        };
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/playerHealth.json", json);
    }

    private void LoadHealth()
    {
        string path = Application.persistentDataPath + "/playerHealth.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            currentHealth = data.health;
            // ����� �������� �������������� �������, ���� �����
            // transform.position = new Vector2(data.position[0], data.position[1]);
        }
        else
        {
            currentHealth = maxHealth; // ������������� ������������ ��������, ���� ����� ���
        }
    }

    [System.Serializable]
    private class PlayerData
    {
        public float health;
        public float[] position;
    }
}