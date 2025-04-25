using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    public event Action OnStepTaken;
    private float walkSpeed { get; set; }
    private float runSpeed { get; set; }
    private ICommandMovement moveCommand;
    private float timeSinceLastMove = 0f;
    private float stepInterval;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="playerRB">Rigidbody2D игрока</param>
    /// <param name="walkSpeed">Скорость ходьбы</param>
    /// <param name="runSpeed">Скорость бегаы</param>
    public void Init(
        Rigidbody2D playerRB, 
        float walkSpeed, 
        float runSpeed, 
        float stepInterval
        )
    {
        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
        this.stepInterval = stepInterval;
        moveCommand = new MoveCommand(playerRB);
    }
    public void Move(Vector3 direction, bool isRunning)
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        moveCommand.Execute(direction * speed);

        if (direction != Vector3.zero && Time.time - timeSinceLastMove > stepInterval)
        {
            OnStepTaken?.Invoke();
            timeSinceLastMove = Time.time;
        }
    }
}
