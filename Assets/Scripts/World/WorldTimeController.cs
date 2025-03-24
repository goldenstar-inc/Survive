using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering.Universal;
using TMPro;

/// <summary>
/// Класс, управляющий внутриигровым временем
/// </summary>
public class WorldTimeController : MonoBehaviour
{   
    /// <summary>
    /// Компонент освещения в мире
    /// </summary>
    public Light2D worldLight;

    /// <summary>
    /// Свойство, хранящее длину внутриигрового дня в реальных секундах 
    /// </summary>
    public static int OneInGameDay => 1440;

    /// <summary>
    /// Интервал между сменой силы освещения
    /// </summary>
    private float timeChangeInterval;

    /// <summary>
    /// Текст, отображающий текущие часы
    /// </summary>
    public TextMeshProUGUI hours;

    /// <summary>
    /// Текст, отображающий текущие минуты
    /// </summary>
    public TextMeshProUGUI minutes;

    /// <summary>
    /// Текущее время
    /// </summary>
    private int currentTime = OneInGameDay / 2;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        if (worldLight == null)
        {
            Debug.LogWarning("WorldLight not loaded");
        } 

        timeChangeInterval = CalculateTimeChangeInterval();  

        StartCoroutine(ChangeWorldLightIntensity());
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        UpdateClock();
    }

    /// <summary>
    /// Метод, вычисляющий интервал для смены интенсивности освещения в мире
    /// </summary>
    /// <returns>Интервал для смены интенсивности освещения в мире</returns>
    private float CalculateTimeChangeInterval() => (float) 100 / (OneInGameDay / 2) / 100;

    /// <summary>
    /// Корутина, плавно изменяющая интенсивность света в мире
    /// </summary>
    /// <returns>IEnumerator для управление корутиной</returns>
    public IEnumerator ChangeWorldLightIntensity()
    {
        while (true)
        {
            while (worldLight.intensity > 0)
            {
                currentTime += 1;
                worldLight.intensity -= timeChangeInterval;
                yield return new WaitForSeconds(1);
            }

            while (worldLight.intensity < 1)
            {
                currentTime += 1;
                worldLight.intensity += timeChangeInterval;
                yield return new WaitForSeconds(1);
            }
        }   
    }

    /// <summary>
    /// Метод, обновляющий внутриигровые часы
    /// </summary>
    private void UpdateClock()
    {
        int currentHour = currentTime / (OneInGameDay / 24);
        int currentMinute = currentTime - currentHour * (OneInGameDay / 24);

        hours.text = $"{currentHour}";

        if (currentMinute < 10)
        {
            minutes.text = $"0{currentMinute}";
        }
        else
        {
            minutes.text = $"{currentMinute}";
        }
    }
}
