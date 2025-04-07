using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimationController animationController;
    [SerializeField] SoundHandler soundController;

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
