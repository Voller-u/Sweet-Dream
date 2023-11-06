using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public string skillName;
    public float skillCoolTime;
    [TextArea]
    public string skillDescription;
    public Skill(string skillName,float skillCoolTime,string skillDescription){
        this.skillName = skillName;this.skillCoolTime = skillCoolTime;this.skillDescription = skillDescription;
    }
}
