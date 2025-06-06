using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;  // ��� ��������
    public Sprite icon;      // ������ ��������
    public int count;        // ���������� � �����

    public Item(string name, Sprite icon, int count = 1)
    {
        this.itemName = name;
        this.icon = icon;
        this.count = count;
    }
}