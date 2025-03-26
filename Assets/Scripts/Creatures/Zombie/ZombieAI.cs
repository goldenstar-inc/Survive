using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private ZombieAnimationController controller;

    // Ссылка на объект игрока
    private Transform player;

    // Радиус обнаружения игрока
    public float detectionRadius = 5f;

    // Скорость передвижения зомби
    public float moveSpeed = 2f;

    // Флаг, указыва    ющий, находится ли игрок в радиусе видимости
    private bool isPlayerInRadius = false;

    void Start()
    {
        player = FindAnyObjectByType<InventoryController>()?.transform;
        controller = GetComponent<ZombieAnimationController>();
    }
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            isPlayerInRadius = distanceToPlayer <= detectionRadius;

            Vector2 direction = (player.position - transform.position).normalized;

            if (isPlayerInRadius)
            {
                MoveTowardsPlayer(direction);
            }
            else
            {
                direction = Vector2.zero;
            }
            controller.UpdateMovementAnimation(direction);
        }
    }

    private void MoveTowardsPlayer(Vector2 direction)
    {

        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}