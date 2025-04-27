using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor.Callbacks;

public class ZombieChase : MonoBehaviour
{
    public event Action<HealthHandler> OnCaughtTarget;
    private Vector2 currentDirection = Vector2.zero;
    private ZombieAnimationController controller;
    private GameObject target;
    private float moveSpeed;
    private float attackRange = 0.9f;
    private StateHandler stateHandler;
    private CreatureState currentState;
    private Rigidbody2D rb;

    /// <summary>
    /// Инициализация скрипта [DI]
    /// </summary>
    /// <param name="moveSpeed">Скорость передвижения</param>
    /// <param name="controller">Контроллер анимаций</param>
    public void Init(
        float moveSpeed, 
        Rigidbody2D rb,
        ZombieAnimationController controller,
        StateHandler stateHandler
        )
    {
        this.moveSpeed = moveSpeed;
        this.rb = rb;
        this.controller = controller;
        this.stateHandler = stateHandler;

        stateHandler.OnStateChanged += SetCurrentState;
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (currentState != CreatureState.Normal) return;

        if (target != null)
        {
            Transform targetTransform = TryGetTargetTransform(target);

            Vector3 zombiePosition = transform.position;
            Vector3 targetPosition = targetTransform.position;

            if (CheckIfTargetIsCaught(zombiePosition, targetPosition))
            {
                HealthHandler targetHealthManager = TryGetTargetHealthManager(target);
                OnCaughtTarget?.Invoke(targetHealthManager);
                controller?.UpdateMovementAnimation(Vector2.zero);
            }
            else
            {
                MoveTowardsTarget(targetTransform);
                controller?.UpdateMovementAnimation(currentDirection);
            }
            currentDirection = (targetPosition - zombiePosition).normalized;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Движение в сторону найденной цели
    /// </summary>
    private void MoveTowardsTarget(Transform targetTransform)
    {
        if (targetTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Проверка, что зомби подошел к цели на доступное для атаки расстояние
    /// </summary>
    /// <param name="zombiePosition">Позиция зомби</param>
    /// <param name="targetPosition">Позиция цели</param>
    /// <returns>True - если зомби подошел к цели на доступное для атаки расстояние, иначе - false</returns>
    private bool CheckIfTargetIsCaught(Vector3 zombiePosition, Vector3 targetPosition)
    {
        float distanceToTarget = Vector2.Distance(targetPosition, zombiePosition);
        return distanceToTarget <= attackRange;
    }

    /// <summary>
    /// Детект входа в коллизию с объектом
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Player"))
            {
                target = collision.gameObject;
            }
        }
    }

    /// <summary>
    /// Детект выхода из коллизии с объектом
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                target = null;
                currentDirection = Vector2.zero;
                controller?.UpdateMovementAnimation(currentDirection);
            }
        }
    }

    /// <summary>
    /// Получение компонента Tranform у цели
    /// </summary>
    /// <param name="target">Цель</param>
    /// <returns>Компонент Tranform у цели</returns>
    private Transform TryGetTargetTransform(GameObject target)
    {
        return target?.GetComponent<Transform>();
    }

    /// <summary>
    /// Получение компонента HealthManager у цели
    /// </summary>
    /// <param name="target">Цель</param>
    /// <returns>Компонент HealthManager у цели</returns>
    private HealthHandler TryGetTargetHealthManager(GameObject target)
    {
        return target?.GetComponent<HealthHandler>();
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