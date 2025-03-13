using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PropertyGetter : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField baseCost;
    public TMP_InputField perLevel;
    public TMP_InputField perTier;

    public (string,string) getProperty()
    {
        string ret = "{";
        ret += baseCost.text;
        if (!perLevel.text.Equals(""))
        {
            ret += ":+:";
            ret += perLevel.text;
            ret += ":*:L1";
        }
        if (!perTier.text.Equals(""))
        {
            ret += ":+:";
            ret += perTier.text;
            ret += ":*:T1";
        }
        ret += "}";
        return (nameField.text, ret);
    }
}
