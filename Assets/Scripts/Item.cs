using UnityEngine.UI; // Добавлено для Sprite
using System;
using UnityEngine; // Убедитесь, что эта строка есть

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int count;
    public ItemType type; // Новый параметр для типа предмета

    public enum ItemType
    {
        Ammo,
        Helmet,
        Vest,
        Backpack
    }

    public Item(string name, Sprite icon, int count, ItemType type)
    {
        this.itemName = name;
        this.icon = icon;
        this.count = count;
        this.type = type;
    }
}