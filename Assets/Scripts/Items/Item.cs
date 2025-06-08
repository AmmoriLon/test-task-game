using UnityEngine.UI; // ��������� ��� Sprite
using System;
using UnityEngine; // ���������, ��� ��� ������ ����

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int count;
    public ItemType type; // ����� �������� ��� ���� ��������

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