using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за передвижение игрока
/// </summary>
public class PlayerMovement : MonoBehaviour
{   
    private float walkSpeed { get; set; }
    private float runSpeed { get; set; }
    private Rigidbody2D playerRB;

    [SerializeField] PlayerSetting setting;
    private ICommandMovement moveCommand;

    // TO FIX
    public void Initialize(Rigidbody2D playerRB, float walkSpeed, float runSpeed)
    {
        if (walkSpeed <= 0f || runSpeed <= 0f)
        {
            Debug.LogError("Speed values must be greater than zero");
            return;
        }
        this.playerRB = playerRB;
        
        moveCommand = new MoveCommand(playerRB);

        this.walkSpeed = walkSpeed;
        this.runSpeed = runSpeed;
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        this.walkSpeed = setting.RunComponent.WalkSpeed;
        this.runSpeed = setting.RunComponent.RunSpeed;
        moveCommand = new MoveCommand(playerRB);
    }

    public void Move(Vector3 direction, bool isRunning)
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        moveCommand.Execute(direction * speed);
    }
}
