using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//임시 스킬 받아오는 스크립트
public class PMS_Skill
{
    public string skillName;
    public string skillDescription;
    public int skillLevel;

    public PMS_Skill(string skillName, string skillDescription, int skillLevel = 0)
    {
        this.skillName = skillName;
        this.skillDescription = skillDescription;
        this.skillLevel = skillLevel;
    }
}
