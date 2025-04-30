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
    public bool allowMultiple = true; // �ߺ� ���� ���� ���� true�ϰ�� ������ ���ð���, false�ϰ�� �ѹ��� ���ð���
}

