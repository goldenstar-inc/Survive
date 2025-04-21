using Unity.VisualScripting;
using UnityEngine;

public class NPCBootstrapper : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] ZombieAnimationController controller;
    [SerializeField] HealthManager healthManager;
    [SerializeField] ZombieAttack zombieAttack;
    [SerializeField] ZombieChase zombieChase;
    [SerializeField] NPCBehavior behavior;
    [SerializeField] float detectionRadius;
    [SerializeField] float moveSpeed;
    void Start()
    {
        healthManager.Init(
            100,
            null,
            0,
            null
        );
        
        controller.Init(
            healthManager,
            animator
        );   

        zombieChase.Init(
            moveSpeed,
            controller
        );

        zombieAttack.Init(
            10, 
            zombieChase
            );  

        behavior.Init(
            healthManager,
            moveSpeed,
            detectionRadius
        );  
    }
}
