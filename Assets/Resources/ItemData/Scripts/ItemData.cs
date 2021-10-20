using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Item Data", menuName = "ItemData")]
public class ItemData : BaseData
{
    public int maxHP;
    public int maxMP;
    public DamageBuff damageBuff;
    public AccuracyBuff accuracyBuff;
    public CritBuff critBuff;
    public ResistanceBuff resistanceBuff;
}

public class DamageBuff : ItemData
{
    public Schools buffSchool;
    public int buffValue;
}

public class AccuracyBuff : ItemData
{
    public Schools buffSchool;
    public int buffValue;
}

public class CritBuff : ItemData
{
    public Schools buffSchool;
    public int buffValue;
}

public class ResistanceBuff : ItemData
{
    public Schools buffSchool;
    public int buffValue;
}
