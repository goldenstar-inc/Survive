using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode keyMoveUp;
    [SerializeField] private KeyCode keyMoveDown;
    [SerializeField] private KeyCode keyMoveLeft;
    [SerializeField] private KeyCode keyMoveRight;
    [SerializeField] private KeyCode keyRun;

    public Vector3 GetMovementInput()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(keyMoveUp)) movement += Vector3.up;
        if (Input.GetKey(keyMoveDown)) movement += Vector3.down;
        if (Input.GetKey(keyMoveLeft)) movement += Vector3.left;
        if (Input.GetKey(keyMoveRight)) movement += Vector3.right;

        return movement.normalized;
    }

    public bool IsRunning()
    {
        return Input.GetKey(keyRun);
    }
}
