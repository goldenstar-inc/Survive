using UnityEngine;
using UnityEngine.InputSystem;

public class PistolAttack : MonoBehaviour, IAttackScript
{
    public Animator playerAnimationController;
    void Start()
    {
        
    }

    public void Attack()
    {
        playerAnimationController.SetBool("IsAttacking", true);
    }

    public void DisableAttack()
    {
        playerAnimationController.SetBool("IsAttacking", false);
    }
}
