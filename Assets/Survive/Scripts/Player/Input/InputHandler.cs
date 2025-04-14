using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerAnimationController animationController;
    private SoundHandler soundHandler;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="playerInput">Скрипт ввода</param>
    /// <param name="playerMovement">Скрипт движения</param>
    /// <param name="animationController">Контроллер анимаций</param>
    /// <param name="soundHandler">Скрипт, хранящий звуки</param>
    public void Init(PlayerInput playerInput, PlayerMovement playerMovement, PlayerAnimationController animationController, SoundHandler soundHandler)
    {
        this.playerInput = playerInput;
        this.playerMovement = playerMovement;
        this.animationController = animationController;
        this.soundHandler = soundHandler;
    }
    private void FixedUpdate()
    {
        Vector3 movement = playerInput.GetMovementInput();
        bool isRunning = playerInput.IsRunning();

        playerMovement.Move(movement, isRunning);

        animationController.UpdateMovementAnimation(movement);
        
        soundHandler.PlayStepSoundIfNeeded(movement);
    }
}
