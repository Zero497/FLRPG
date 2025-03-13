using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SubmitSkill : MonoBehaviour
{
    public static SubmitSkill subSkill;
    
    public GameObject confirmOverwrite;
    public GameObject errMessageObj;

    [System.NonSerialized]public string errMessage;
    public Skill curSkill;
    
    public TMP_InputField nameField;
    public TMP_InputField primaryCat;
    public TMP_InputField categories;
    public TMP_InputField description;
    public TMP_InputField special;
    public TMP_InputField startingLevel;
    public TMP_Dropdown isDomain;
    public TMP_Dropdown obtainableThroughPractice;

    private void Awake()
    {
        subSkill = this;
    }

    public void submit()
    {
        if (nameField.text.Equals(""))
        {
            errMessage = "Skill must have a name!";
            Instantiate(errMessageObj);
            return;
        }
        TextInfo inf = new CultureInfo("en-US").TextInfo;
        Skill skill = new Skill();
        curSkill = skill;
        skill.sName = inf.ToTitleCase(nameField.text);
        skill.primaryCategory = primaryCat.text.ToUpper();
        string[] cats = categories.text.Split(",");
        for (int i = 0; i<cats.Length; i++)
        {
            cats[i] = inf.ToTitleCase(cats[i].Trim());
        }
        Array.Sort(cats);
        skill.categories = cats;
        skill.setDesc(description.text);
        if(isDomain.value == 0)
            skill.setDomain(true);
        else
        {
            skill.setDomain(false);
        }
        skill.setSpecial(special.text);
        int n;
        bool isNumeric = int.TryParse(startingLevel.text, out n);
        if (isNumeric)
        {
            skill.startingLevel = n;
        }
        else
        {
            errMessage = "Starting Level must be a number!";
            Instantiate(errMessageObj);
            return;
        }
        skill.obtainableThroughPractice = obtainableThroughPractice.value == 0;
        List<(string, string)> customProperties = CustomProperties.customProperties.getCustomProps();
        foreach ((string, string) tup in customProperties)
        {
            skill.AddProperty(inf.ToTitleCase(tup.Item1), tup.Item2);
        }
        if (SkillDB.db.hasSkill(skill.sName, skill.primaryCategory))
        {
            Instantiate(confirmOverwrite);
            return;
        }
        SkillDB.db.addSkill(skill);
        SceneManager.LoadScene("SkillSearch");
    }
}
