using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName = "Ammo"; // ��� ��������
    public Sprite itemIcon;          // ������ ��������
    public int count = 1;            // ����������

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Item item = new Item(itemName, itemIcon, count);
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject); // ������� ������� � �����
        }
    }
}