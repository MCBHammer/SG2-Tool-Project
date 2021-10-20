using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenuAttribute(fileName = "New Item Data", menuName = "ItemData")]
public class ItemData : BaseData
{
    public int[] maxHP = new int [0];
    public int[] maxMP = new int[0];
    public DamageBuff[] damageBuff = new DamageBuff[0];
    public AccuracyBuff[] accuracyBuff = new AccuracyBuff[0];
    public CritBuff[] critBuff = new CritBuff[0];
    public ResistanceBuff[] resistanceBuff = new ResistanceBuff[0];
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
