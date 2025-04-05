using UnityEngine;

public class MoveCommand : ICommandMovement
{
    private Rigidbody2D rigidbody;

    public MoveCommand(Rigidbody2D Rigidbody)
    {
        rigidbody = Rigidbody;
    }

    public void Execute(Vector2 movement)
    {
        rigidbody.linearVelocity = movement;
    }
}
