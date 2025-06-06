using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName = "Ammo"; // Имя предмета
    public Sprite itemIcon;          // Иконка предмета
    public int count = 1;            // Количество

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Item item = new Item(itemName, itemIcon, count);
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject); // Удаляем предмет с карты
        }
    }
}