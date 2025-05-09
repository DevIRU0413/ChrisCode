using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Trait
{
    public string traitName;
    public string description;
    public TraitType type;
    public float value;
    public bool allowMultiple = true; // 중복 선택 가능 여부 true일경우 여러번 선택가능, false일경우 한번만 선택가능
}

