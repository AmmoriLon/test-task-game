using UnityEngine.UI; // Добавлено для Sprite
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public int count = 1;
    public Item.ItemType itemType; // Новый параметр типа

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(new Item(itemName, itemIcon, count, itemType));
            Destroy(gameObject);
        }
    }
}