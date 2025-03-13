using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class SkillDB : MonoBehaviour
{
    public static SkillDB db;

    public string dbDirectoryName;

    private Dictionary<string, Dictionary<string, Skill>> _skills = new Dictionary<string, Dictionary<string, Skill>>();

    public bool hasSkill(string skill, string primaryCategory)
    {
        if(_skills.TryGetValue(primaryCategory, out var skill1))
            return skill1.ContainsKey(skill);
        return false;
    }

    public void addSkill(Skill skill)
    {
        if (!_skills.ContainsKey(skill.primaryCategory))
            _skills[skill.primaryCategory] = new Dictionary<string, Skill>();
        _skills[skill.primaryCategory][skill.sName] = skill;
    }

    public List<Skill> getSkills()
    {
        List<List<Skill>> toSort = new List<List<Skill>>();
        foreach (string key in _skills.Keys)
        {
            toSort.Add(_skills[key].Values.ToList());
        }
        return merge(toSort);
    }

    private List<Skill> merge(List<List<Skill>> input)
    {
        int[] inds = new int[input.Count];
        List<Skill> ret = new List<Skill>();
        foreach (List<Skill> list in input)
        {
            list.Sort();
        }
        while (true)
        {
            int ind = 0;
            Skill skill = null;
            for (int i = 0; i < inds.Length; i++)
            {
                if(inds[i] == input[i].Count) continue;
                if (skill == null || skill.CompareTo(input[i][inds[i]]) > 0)
                {
                    ind = i;
                    skill = input[i][inds[i]];
                }
            }
            if (skill == null) break;
            ret.Add(skill);
            inds[ind] += 1;
        }
        return ret;
    }

    private void Awake()
    {
        if (db == null)
        {
            db = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Load()
    {
        XmlSerializer ser = new XmlSerializer(typeof(Skill));
        Directory.CreateDirectory(dbDirectoryName);
        string[] files = Directory.GetFiles(dbDirectoryName);
        foreach (string file in files)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            Skill nSkill = (Skill)ser.Deserialize(fs);
            addSkill(nSkill);
            fs.Close();
        }
    }

    private void Save()
    {
        Directory.CreateDirectory(dbDirectoryName);
        XmlSerializer ser = new XmlSerializer(typeof(Skill));
        foreach (string key in _skills.Keys)
        {
            foreach (string k2 in _skills[key].Keys)
            {
                TextWriter writer = new StreamWriter(dbDirectoryName + "/" + k2);
                ser.Serialize(writer, _skills[key][k2]);
                writer.Close();
            }
            
        }
    }
}
