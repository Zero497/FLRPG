using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
// ReSharper disable StringCompareToIsCultureSpecific

public class Skill : IComparable<Skill>
{
    public string sName;

    public string primaryCategory;

    public string[] categories;

    public bool isDomain;
    public void setDomain(bool isDomain)
    {
        this.isDomain = isDomain;
    }

    public string desc;
    public void setDesc(string input)
    {
        desc = input;
    }
    public string Desc(int level = int.MaxValue)
    {
        return toDisplayString(desc, level);
    }

    public List<string> propertiesKeys = new List<string>();

    public List<string> propertiesValues = new List<string>();

    public void AddProperty(string pName, string val)
    {
        propertiesKeys.Add(pName);
        propertiesValues.Add(val);
    }

    public bool obtainableThroughPractice;

    public int startingLevel;

    public string special;
    public void setSpecial(string input)
    {
        special = input;
    }
    public string Special(int level = int.MaxValue)
    {
        return toDisplayString(special, level);
    }

    public static int LevelToTier(int level)
    {
        if (level <= 0)
        {
            return 0;
        }
        if (level <= 7)
        {
            return (int)Mathf.Ceil(level / 2.0f);
        }

        level -= 7;
        return 4 + (int)Mathf.Floor(level / 3.0f);
    }

    private string toDisplayString(string input, int level)
    {
        if (level == int.MaxValue)
        {
            return toDisplayString(input);
        }
        string ret = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '{')
            {
                (string, int) parseStr = getParseString(input, i);
                ret += pstrToFloat(parseStr.Item1, level);
                i = parseStr.Item2;
            }
            else
            {
                ret += input[i];
            }
        }

        return ret;
    }

    private float pstrToFloat(string input, int level)
    {
        string[] split = input.Split(":");
        int tier = LevelToTier(level);
        string eval = "";
        foreach (string str in split)
        {
            switch (str)
            {
                case "T":
                    eval += tier;
                    break;
                case "T1":
                    eval += Mathf.Max(tier-1,0);
                    break;
                case "L":
                    eval += level;
                    break;
                case "L1":
                    eval += Mathf.Max(level-1, 0);
                    break;
                default:
                    eval += str;
                    break;
            }
        }
        DataTable dt = new DataTable();
        return (float) dt.Compute(eval,"");
    }

    private string toDisplayString(string input)
    {
        string ret = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '{')
            {
                (string, int) parseStr = getParseString(input, i);
                ret += pstrToDisplayString(parseStr.Item1);
                i = parseStr.Item2;
            }
            else
            {
                ret += input[i];
            }
        }

        return ret;
    }

    private string pstrToDisplayString(string input)
    {
        string[] split = input.Split(":");
        string ret = "";
        foreach (string str in split)
        {
            switch (str)
            {
                case "T":
                    ret += "Tier ";
                    break;
                case "T1":
                    ret += "Tier above Novice ";
                    break;
                case "L":
                    ret += "Level ";
                    break;
                case "L1":
                    ret += "Level above 1 ";
                    break;
                default:
                    ret += str;
                    break;
            }
        }
        return ret.Trim();
    }

    private (string, int) getParseString(string input, int startInd)
    {
        string ret = "";
        for (int i = startInd+1; i < input.Length; i++)
        {
            if (input[i] == '}')
            {
                return (ret, i);
            }
            ret += input[i];
        }
        throw new Exception("Got string with invalid format");
    }

    public int CompareTo(Skill other)
    {
        int comp = sName.CompareTo(other.sName);
        if (comp == 0)
        {
            return primaryCategory.CompareTo(other.primaryCategory);
        }
        return comp;
    }
}
