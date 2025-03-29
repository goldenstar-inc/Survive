using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    /// <summary>
    /// Скорость игрока по умолчанию
    /// </summary>
    public const float WALKING_PLAYER_SPEED = 4f;

    /// <summary>
    /// Скорость игрока по умолчанию
    /// </summary>
    public const float RUNNING_PLAYER_SPEED = 6f;

    /// <summary>
    /// Скорость игрока
    /// </summary>
    public float speed = WALKING_PLAYER_SPEED;

    /// <summary>
    /// Скрипт, отвечающий за управление анимацией игрока
    /// </summary>
    private PlayerAnimationController animationController;

    /// <summary>
    /// Компонент Rigidbody2D игрока
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Вектор ввода
    /// </summary>
    private Vector2 input;

    /// <summary>
    /// Время последнего шага
    /// </summary>
    private float lastStepTime = 0f;

    /// <summary>
    /// Интервал между шагами по умолчанию
    /// </summary>
    private const float WALKING_STEP_INTERVAL = 0.5f;

    /// <summary>
    /// Интервал между шагами по умолчанию
    /// </summary>
    private const float RUNNING_STEP_INTERVAL = 0.3f;

    /// <summary>
    /// Интервал между шагами
    /// </summary>
    private float stepInterval = WALKING_STEP_INTERVAL;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RUNNING_PLAYER_SPEED;
            stepInterval = RUNNING_STEP_INTERVAL;
        }
        else
        {
            speed = WALKING_PLAYER_SPEED;
            stepInterval = WALKING_STEP_INTERVAL;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

        if (input != Vector2.zero && Time.time - lastStepTime > stepInterval)
        {
            PlayFootstepSound();
            lastStepTime = Time.time;
        }
    }

    /// <summary>
    /// Метод для обработки физики
    /// </summary>
    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;

        if (animationController != null)
        {
            animationController.UpdateMovementAnimation(input);
        }
    }

    /// <summary>
    /// Проигрывает звук шагов
    /// </summary>
    private void PlayFootstepSound()
    {
        if (!SoundController.Instance.footstepsAudioSource.isPlaying)
        {
            SoundController.Instance.PlayRandomStepSound();
        }
    }
}
