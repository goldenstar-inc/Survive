using UnityEngine;
using UnityEngine.Animations;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler; 
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private SoundHandler soundHandler;
    [SerializeField] private SoundController soundController;
    [SerializeField] private PlayerSetting playerSetting;
    private AudioClip[] stepSounds => playerSetting.RunComponent.StepSounds;
    private float stepInterval => playerSetting.RunComponent.StepInterval;
    [SerializeField] private Rigidbody2D playerRB;
    private float walkSpeed => playerSetting.RunComponent.WalkSpeed;
    private float runSpeed => playerSetting.RunComponent.RunSpeed;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private InventoryController inventoryController;
    private void Start()
    {
        playerMovement.Initialize(playerRB, walkSpeed, runSpeed);
        soundHandler.Initialize(stepSounds, stepInterval, soundController);
        inputHandler.Initialize(playerInput, playerMovement, animationController, soundHandler);
        playerAnimationController.Initialize(healthManager, playerAnimator);   
        
    }
}
