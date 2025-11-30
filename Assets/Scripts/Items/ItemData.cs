using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public enum ItemType{
        InteractiveItem,
        ConsumableItem
    }   

    public string name = "";
    public Sprite icon;
    public string description = "";
    public ItemType type;
}
