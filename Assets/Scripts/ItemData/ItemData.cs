using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

//[CreateAssetMenuAttribute(fileName = "New Item Data", menuName = "ItemData")]
public class ItemData : BaseData
{
    public int[] maxHP = new int [0];
    public int[] maxMP = new int[0];
    public Buff[] BuffArray = new Buff[0];
}