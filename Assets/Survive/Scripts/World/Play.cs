using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [Header("����� �����")]
    public int sceneNumber;
    public void Transition()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
