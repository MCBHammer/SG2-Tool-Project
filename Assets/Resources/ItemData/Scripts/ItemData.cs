using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Item Data", menuName = "ItemData")]
public class ItemData : BaseData
{
    public int[] maxHP = new int [0];
    public int[] maxMP = new int[0];
    public Buff[] BuffArray = new Buff[0];
}

public class Buff : ItemData
{
    public BuffType buffType;
    public Schools buffSchool;
    public int buffValue;
    public Buff(BuffType setBuff, Schools setSchool, int setValue)
    {
        buffType = setBuff;
        buffSchool = setSchool;
        buffValue = setValue;
    }
}