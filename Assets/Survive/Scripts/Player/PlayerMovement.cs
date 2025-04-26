using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    public event Action<MovementComponent> OnStepTaken;
    private MovementComponent movementComponent;
    private float walkSpeed { get; set; }
    private float runSpeed { get; set; }
    private ICommandMovement moveCommand;
    private float timeSinceLastMove = 0f;
    private float baseStepInterval;
    private float currentStepInterval;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="movementComponent">Конфиг, содержащий информацию, связанную с движением</param>
    /// <param name="playerRB">Rigidbody2D игрока</param>
    /// <param name="walkSpeed">Скорость ходьбы</param>
    /// <param name="runSpeed">Скорость бегаы</param>
    public void Init(
        MovementComponent movementComponent,
        Rigidbody2D playerRB, 
        float walkSpeed, 
        float runSpeed, 
        float baseStepInterval
        )
    {
        this.movementComponent = movementComponent;
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.baseStepInterval = baseStepInterval;
        moveCommand = new MoveCommand(playerRB);
    }
    public void Move(Vector3 direction, bool isRunning)
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        currentStepInterval = isRunning ? (walkSpeed * baseStepInterval / runSpeed) : baseStepInterval;
        moveCommand.Execute(direction * speed);

        if (direction != Vector3.zero && Time.time - timeSinceLastMove > currentStepInterval)
        {
            OnStepTaken?.Invoke(movementComponent);
            timeSinceLastMove = Time.time;
        }
    }
}
