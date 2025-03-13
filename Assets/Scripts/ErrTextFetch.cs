using System;
using TMPro;
using UnityEngine;

public class ErrTextFetch : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Awake()
    {
        text.text = SubmitSkill.subSkill.errMessage;
    }
}
