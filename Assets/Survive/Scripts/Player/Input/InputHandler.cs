using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput { get; set; }
    [SerializeField] PlayerMovement playerMovement { get; set; }
    [SerializeField] PlayerAnimationController animationController { get; set; }
    [SerializeField] SoundHandler soundController { get; set; }

    public void Initialize(PlayerInput playerInput, PlayerMovement playerMovement, PlayerAnimationController animationController, SoundHandler soundController)
    {
        this.playerInput = playerInput;
        this.playerMovement = playerMovement;
        this.animationController = animationController;
        this.soundController = soundController;
    }
    private void FixedUpdate()
    {
        Vector3 movement = playerInput.GetMovementInput();
        bool isRunning = playerInput.IsRunning();

        playerMovement.Move(movement, isRunning);

        animationController.UpdateMovementAnimation(movement);

        soundController.PlayStepSoundIfNeeded(movement);
    }
}
