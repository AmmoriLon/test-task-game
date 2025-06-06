using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;  // Имя предмета
    public Sprite icon;      // Иконка предмета
    public int count;        // Количество в стаке

    public Item(string name, Sprite icon, int count = 1)
    {
        this.itemName = name;
        this.icon = icon;
        this.count = count;
    }
}