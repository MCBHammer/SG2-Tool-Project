using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class ItemData : ScriptableObject
{
    public string name;
    public ItemType itemType;
    public Schools school;
    public int levelRestriction;
    public Sprite Visual2D;
}
