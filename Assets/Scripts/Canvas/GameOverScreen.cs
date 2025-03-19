using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    /// <summary>
    /// Метод, выводящий экран "Game Over"
    /// </summary>
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Метод, начинающий игру заново, при нажатии на кнопку "Restart"
    /// </summary>
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
