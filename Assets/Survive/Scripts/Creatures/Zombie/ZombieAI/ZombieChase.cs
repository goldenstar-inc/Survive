using Unity.VisualScripting;
using UnityEngine;
using System;

public class ZombieChase : MonoBehaviour
{
    public event Action<HealthManager> OnCaughtTarget;
    private Vector2 currentDirection = Vector2.zero;
    private ZombieAnimationController controller;
    private GameObject target;
    private float moveSpeed;
    private float attackRange = 0.75f;

    /// <summary>
    /// Инициализация скрипта [DI]
    /// </summary>
    /// <param name="moveSpeed">Скорость передвижения</param>
    /// <param name="controller">Контроллер анимаций</param>
    public void Init(float moveSpeed, ZombieAnimationController controller)
    {
        this.moveSpeed = moveSpeed;
        this.controller = controller;
    }
    private void Move()
    {
        if (target != null)
        {
            Transform targetTransform = TryGetTargetTransform(target);

            Vector3 zombiePosition = transform.position;
            Vector3 targetPosition = targetTransform.position;

            if (CheckIfTargetIsCaught(zombiePosition, targetPosition))
            {
                HealthManager targetHealthManager = TryGetTargetHealthManager(target);
                OnCaughtTarget?.Invoke(targetHealthManager);
            }

            currentDirection = (targetPosition - zombiePosition).normalized;
            MoveTowardsTarget(targetTransform);
            controller?.UpdateMovementAnimation(currentDirection);
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
    /// Детект коллизии с объектом
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Move();
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
    private HealthManager TryGetTargetHealthManager(GameObject target)
    {
        return target?.GetComponent<HealthManager>();
    }
}