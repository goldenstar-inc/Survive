using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float walkingSpeed = 4f;
    [SerializeField] private float runningSpeed = 6f;

    private Rigidbody2D playerRB;
    private ICommandMovement moveCommand;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        moveCommand = new MoveCommand(playerRB);
    }

    public void Move(Vector3 direction, bool isRunning)
    {
        float speed = isRunning ? runningSpeed : walkingSpeed;
        moveCommand.Execute(direction * speed);
    }
}
