using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable
}

[System.Serializable]
public class ItemDataSpeedBoost
{
    public float multiplier;    //����
    public float duration;      //���� �ð�
}

[CreateAssetMenu(fileName = "Item", menuName = "New Speed Boost Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type = ItemType.Consumable;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Speed Boost Effect")]
    public ItemDataSpeedBoost speedBoost;
}
