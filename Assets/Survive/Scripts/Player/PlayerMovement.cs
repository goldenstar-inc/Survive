using System;
using UnityEngine;
/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    public event Action<MovementComponent> OnStepTaken;
    private StateHandler stateHandler;
    private MovementComponent movementComponent;
    private float walkSpeed { get; set; }
    private float runSpeed { get; set; }
    private ICommandMovement moveCommand;
    private float timeSinceLastMove = 0f;
    private float baseStepInterval;
    private float currentStepInterval;
    private CreatureState currentState;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="stateHandler">Скрипт, управляющий состояниями</param>
    /// <param name="movementComponent">Конфиг, содержащий информацию, связанную с движением</param>
    /// <param name="playerRB">Rigidbody2D игрока</param>
    /// <param name="walkSpeed">Скорость ходьбы</param>
    /// <param name="runSpeed">Скорость бегаы</param>
    public void Init(
        StateHandler stateHandler,
        MovementComponent movementComponent,
        Rigidbody2D playerRB, 
        float walkSpeed, 
        float runSpeed, 
        float baseStepInterval
        )
    {
        this.stateHandler = stateHandler;
        this.movementComponent = movementComponent;
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.baseStepInterval = baseStepInterval;

        stateHandler.OnStateChanged += SetCurrentState; 
        moveCommand = new MoveCommand(playerRB);
    }
    public void Move(Vector3 direction, bool isRunning)
    {
        if (currentState != CreatureState.Normal) return;

        float speed = isRunning ? runSpeed : walkSpeed;
        currentStepInterval = isRunning ? (walkSpeed * baseStepInterval / runSpeed) : baseStepInterval;
        moveCommand.Execute(direction * speed);

        if (direction != Vector3.zero && Time.time - timeSinceLastMove > currentStepInterval)
        {
            OnStepTaken?.Invoke(movementComponent);
            timeSinceLastMove = Time.time;
        }
    }

    public void SetCurrentState(CreatureState currentState)
    {
        this.currentState = currentState;
    }

    private void OnDestroy()
    {
        if (stateHandler != null)
        {
            stateHandler.OnStateChanged -= SetCurrentState; 
        }
    }
}
