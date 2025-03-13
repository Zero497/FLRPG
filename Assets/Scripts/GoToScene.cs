using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string SceneName;

    public void goToScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
