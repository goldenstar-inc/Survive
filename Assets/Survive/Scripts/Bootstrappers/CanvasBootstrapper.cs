using UnityEngine;

public class CanvasBootstrapper : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] HealthDisplay healthDisplay;
    [SerializeField] AmmoDisplay ammoDisplay;
    private HealthManager healthManager;
    private AmmoHandler ammoHandler;
    private Camera renderCamera;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="healthManager">Скрипт, отвечающий за управление здоровьем</param>
    /// <param name="ammoHandler">Скрипт, управляющий боезапасом</param>
    public void Init(
        HealthManager healthManager, 
        AmmoHandler ammoHandler,
        Camera renderCamera
        )
    {
        this.healthManager = healthManager;
        this.ammoHandler = ammoHandler;
        this.renderCamera = renderCamera;

        if (!Validate()) return;

        InitCamera();
        InitHealthDisplay();
        InitAmmoDisplay();
    }

    /// <summary>
    /// Валидация скриптов и систем канваса
    /// </summary>
    /// <returns>True - если все скрипты и системы пргружены корректно, иначе - false</returns>
    private bool Validate()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas not loaded");
            return false;
        }

        if (renderCamera == null)
        {
            Debug.LogError("Camera not loaded");
            return false;
        }

        if (healthDisplay == null)
        {
            Debug.LogError("Health display system isn't loaded correctly");
            return false;
        }

        if (ammoDisplay == null)
        {
            Debug.LogError("Ammo display system isn't loaded correctly");
            return false;
        }

        if (healthManager == null)
        {
            Debug.LogError("HealthManager not loaded");
            return false;
        }

        if (ammoHandler == null)
        {
            Debug.LogError("AmmoHandler not loaded");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Инициализация камеры
    /// </summary>
    private void InitCamera()
    {
        canvas.worldCamera = renderCamera;
    }

    /// <summary>
    /// Инициализация системы отображения здоровья
    /// </summary>
    private void InitHealthDisplay()
    {
        healthDisplay.Init(healthManager);
    }

    /// <summary>
    /// Инициализация системы отображения боезапаса
    /// </summary>
    private void InitAmmoDisplay()
    {
        ammoDisplay.Init(ammoHandler);
    }
}