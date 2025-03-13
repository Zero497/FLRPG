using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmSubmit : MonoBehaviour
{
    public void OnClick()
    {
        SkillDB.db.addSkill(SubmitSkill.subSkill.curSkill);
        SceneManager.LoadScene("SkillSearch");
    }
}
