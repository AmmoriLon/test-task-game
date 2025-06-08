using UnityEngine.UI; // ��������� ��� Sprite
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public int count = 1;
    public Item.ItemType itemType; // ����� �������� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(new Item(itemName, itemIcon, count, itemType));
            Destroy(gameObject);
        }
    }
}