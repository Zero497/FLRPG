using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillView : MonoBehaviour
{
    public Transform viewContent;
    public GameObject skillPrefab;
    public List<SkillViewSingle> skills = new List<SkillViewSingle>();
    private void Start()
    {
        List<Skill> toShow = SkillDB.db.getSkills();
        while (skills.Count < toShow.Count)
        {
            skills.Add(Instantiate(skillPrefab, viewContent).GetComponent<SkillViewSingle>());
        }
        for (int i = 0; i < skills.Count; i++)
        {
            if (i < toShow.Count)
            {
                skills[i].gameObject.SetActive(true);
                skills[i].SetMySkill(toShow[i]);
            }
            else
            {
                skills[i].gameObject.SetActive(false);
            }
        }
    }
}
