using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Buff : ScriptableObject
{
    public BuffType buffType;
    public Schools buffSchool;
    public int buffValue;
    public void Init(BuffType setBuff, Schools setSchool, int setValue)
    {
        buffType = setBuff;
        buffSchool = setSchool;
        buffValue = setValue;
    }
}
