using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimationController animationController;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="playerInput">Класс ввода</param>
    /// <param name="playerMovement">Класс движения</param>
    /// <param name="animationController">Контроллер анимаций</param>
    /// <param name="soundHandler">Класс, хранящий звуки</param>
    public void Init(
        PlayerInput playerInput, 
        PlayerMovement playerMovement, 
        PlayerAnimationController animationController
    )
    {
        this.playerInput = playerInput;
        this.playerMovement = playerMovement;
        this.animationController = animationController;
    }
    private void FixedUpdate()
    {
        Vector3 movement = playerInput.GetMovementInput();
        bool isRunning = playerInput.IsRunning();

        playerMovement.Move(movement, isRunning);

        animationController.UpdateMovementAnimation(movement);
    }
}
