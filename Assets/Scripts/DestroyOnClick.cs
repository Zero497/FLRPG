using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
    public GameObject DO;

    public void OnClick()
    {
        Destroy(DO);
    }
}
