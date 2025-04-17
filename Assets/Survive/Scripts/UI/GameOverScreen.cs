using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] HealthManager playerHealthManager;
    [SerializeField] GameObject gameOverScreen;

    /// <summary>
    /// Включение экрана "Game Over"
    /// </summary>
    public void Setup()
    {
        gameOverScreen.SetActive(true);
    }

    /// <summary>
    /// Кнопка перезагрузки игры
    /// </summary>
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnEnable()
    {
        playerHealthManager.OnDeath += Setup;
    }
    void OnDisable()
    {
        if (playerHealthManager != null)
        {
            playerHealthManager.OnDeath -= Setup;
        }
    }
}
