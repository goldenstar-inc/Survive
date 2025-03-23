using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Скорость игрока
    /// </summary>
    public float speed = 4f;

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
    /// Интервал между шагами
    /// </summary>
    private float stepInterval = 0.5f;

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
