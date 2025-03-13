using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomProperties : MonoBehaviour
{
    public static CustomProperties customProperties;
    
    public GameObject customProperty;

    public Transform panel;

    private List<GameObject> propList = new List<GameObject>();

    private void Awake()
    {
        customProperties = this;
    }

    public void AddProperty()
    {
        propList.Add(Instantiate(customProperty, panel));
    }

    public void RemoveProperty()
    {
        if (propList.Count >= 1)
        {
            Destroy(propList[^1]);
            propList.RemoveAt(propList.Count-1);
        }
    }

    public List<(string,string)> getCustomProps()
    {
        List<(string,string)> ret = new List<(string, string)>();
        foreach (GameObject propO in propList)
        {
            ret.Add(propO.GetComponent<PropertyGetter>().getProperty());
        }
        return ret;
    }
}
