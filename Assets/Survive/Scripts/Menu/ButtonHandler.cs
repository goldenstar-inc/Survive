using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{

    [Header("����� �����")]
    public int sceneNumber;
    public void Transition()
    {
        gameObject.SetActive(true);
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(sceneNumber);
    }

}
