using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton
    public List<Item> items = new List<Item>(); // Список предметов
    public GameObject inventoryPanel; // Панель инвентаря
    public Transform slotsParent;     // Родительский объект слотов
    public GameObject slotPrefab;     // Префаб слота
    private List<GameObject> slots = new List<GameObject>(); // Список слотов
    private int selectedSlotIndex = -1; // Индекс выбранного слота (-1 = не выбран)

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeInventory();
        UpdateInventoryUI();
    }

    void InitializeInventory()
    {
        for (int i = 0; i < 6; i++) // Создаем 6 слотов
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            slots.Add(slot);

            // Добавляем обработчик клика для выбора слота
            Button slotButton = slot.GetComponent<Button>();
            if (slotButton != null)
            {
                int slotIndex = i; // Захватываем индекс для лямбда-выражения
                slotButton.onClick.AddListener(() => OnSlotClicked(slotIndex));
            }
        }
    }

    public void AddItem(Item item)
    {
        // Проверяем, есть ли уже такой предмет
        Item existingItem = items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            existingItem.count += item.count;
        }
        else
        {
            items.Add(item);
        }
        UpdateInventoryUI();
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            items.RemoveAt(index);
            selectedSlotIndex = -1; // Сбрасываем выбор после удаления
            UpdateInventoryUI();
        }
    }

    public int GetAmmoCount()
    {
        Item ammo = items.Find(i => i.itemName == "Ammo");
        return ammo != null ? ammo.count : 0;
    }

    public bool UseAmmo(int amount)
    {
        Item ammo = items.Find(i => i.itemName == "Ammo");
        if (ammo != null && ammo.count >= amount)
        {
            ammo.count -= amount;
            if (ammo.count <= 0)
            {
                items.Remove(ammo);
            }
            UpdateInventoryUI();
            return true;
        }
        return false;
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var icon = slots[i].transform.Find("ItemIcon").GetComponent<Image>();
            var countText = slots[i].transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
            var deleteButton = slots[i].transform.Find("DeleteButton").GetComponent<Button>();

            if (i < items.Count)
            {
                icon.sprite = items[i].icon;
                icon.color = new Color(1, 1, 1, 1); // Делаем иконку видимой
                countText.text = items[i].count > 1 ? items[i].count.ToString() : "";
                deleteButton.gameObject.SetActive(i == selectedSlotIndex); // Показываем кнопку только для выбранного слота

                // Настраиваем кнопку удаления (только для выбранного слота)
                if (i == selectedSlotIndex)
                {
                    deleteButton.onClick.RemoveAllListeners();
                    int slotIndex = i;
                    deleteButton.onClick.AddListener(() => RemoveItem(slotIndex));
                }
            }
            else
            {
                icon.sprite = null;
                icon.color = new Color(1, 1, 1, 0); // Скрываем иконку
                countText.text = "";
                deleteButton.gameObject.SetActive(false);
            }
        }
    }

    private void OnSlotClicked(int index)
    {
        selectedSlotIndex = index; // Устанавливаем выбранный слот
        UpdateInventoryUI(); // Обновляем UI для отображения кнопки
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}