using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton
    public List<Item> items = new List<Item>(); // ������ ���������
    public GameObject inventoryPanel; // ������ ���������
    public Transform slotsParent;     // ������������ ������ ������
    public GameObject slotPrefab;     // ������ �����
    private List<GameObject> slots = new List<GameObject>(); // ������ ������

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
        for (int i = 0; i < 6; i++) // ������� 6 ������
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            slots.Add(slot);
        }
    }

    public void AddItem(Item item)
    {
        // ���������, ���� �� ��� ����� �������
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
                icon.color = new Color(1, 1, 1, 1); // ������ ������ �������
                countText.text = items[i].count > 1 ? items[i].count.ToString() : "";
                deleteButton.gameObject.SetActive(true);

                // ����������� ������ ��������
                int slotIndex = i;
                deleteButton.onClick.RemoveAllListeners();
                deleteButton.onClick.AddListener(() => RemoveItem(slotIndex));
            }
            else
            {
                icon.sprite = null;
                icon.color = new Color(1, 1, 1, 0); // �������� ������
                countText.text = "";
                deleteButton.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}