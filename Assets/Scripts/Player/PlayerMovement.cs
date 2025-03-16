using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;

    private PlayerAnimationController animationController;
    private Rigidbody2D rb;
    private Vector2 input;

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
}
