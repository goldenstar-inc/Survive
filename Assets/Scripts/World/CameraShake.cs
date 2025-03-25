using UnityEngine;

/// <summary>
/// Класс для реализации эффекта тряски камеры
/// </summary>
public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0f; // Длительность тряски
    private float shakeMagnitude = 0.1f; // Масштаб тряски (интенсивность)
    private float dampingSpeed = 1f; // Скорость затухания тряски

    private Vector3 initialPosition; // Начальная позиция камеры

    void Start()
    {
        // Сохраняем начальную позицию камеры
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            // Генерируем случайное смещение
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            // Уменьшаем длительность тряски
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // Возвращаем камеру в исходное положение
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    /// <summary>
    /// Метод для запуска тряски камеры
    /// </summary>
    /// <param name="duration">Длительность тряски</param>
    /// <param name="magnitude">Интенсивность тряски</param>
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}