using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SkillViewSingle : MonoBehaviour
{
    public TextMeshProUGUI sName;
    public TextMeshProUGUI pCat;
    public TextMeshProUGUI cat;

    private Skill skill;

    public void SetMySkill(Skill nSkill)
    {
        skill = nSkill;
        sName.text = "Name: " + skill.sName;
        pCat.text = "Campaign: " + skill.primaryCategory;
        cat.text = "Tags: ";
        Array.Sort(skill.categories);
        foreach (string category in skill.categories)
        {
            cat.text += category + " ";
        }
    }
}
