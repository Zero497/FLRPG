using TMPro;
using UnityEngine;

public class ShowHide : MonoBehaviour
{
    public GameObject target;

    public TextMeshProUGUI text;

    private bool active = false;

    public void OnClick()
    {
        if (active)
        {
            target.SetActive(false);
            text.text = "Show";
        }
        else
        {
            target.SetActive(true);
            text.text = "Hide";
        }
        active = !active;
    }
}
