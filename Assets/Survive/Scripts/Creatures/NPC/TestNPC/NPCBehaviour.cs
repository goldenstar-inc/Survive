using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Transform player;

    private HealthHandler healthManager;
    private float detectionRadius = 10f;
    private float moveSpeed = 3f;
    public int health => healthManager.GetCurrrentHealth();

    private Vector2 searchDirection;
    private Vector2 searchStartPosition;
    private float searchDistance = 5f;
    private ITaskQueue taskQueue;
    private List<Task> allTasks = new();
    private Task currentTask;

    public void Init(HealthHandler healthManager, float moveSpeed, float detectionRadius)
    {
        this.healthManager = healthManager;
        this.moveSpeed = moveSpeed;
        this.detectionRadius = detectionRadius;
        taskQueue = new PairingHeap<Task>();
    }

    private void Update()
    {
        HandleTaskLogic();
        ExecuteTopPriorityTask();
        HandleTaskMovement();
    }

    private void HandleTaskLogic()
    {
        float distToPlayer = Vector3.Distance(player.position, transform.position);
        bool playerClose = distToPlayer < detectionRadius;
        bool playerFar = distToPlayer > detectionRadius + 2f;
        bool lowHealth = health < 50;
        bool safeAndHealthy = !playerClose && !lowHealth;
        

        HandleOrInsertTask("Heal", lowHealth && playerFar, Mathf.Clamp(health, 1, 100));
        HandleOrInsertTask("Search", safeAndHealthy, 100);
        HandleOrInsertTask("Attack", playerClose && !lowHealth, Mathf.Clamp((int)distToPlayer, 1, 100));
        HandleOrInsertTask("RunAway", playerClose && lowHealth, Mathf.Clamp(100 - health, 1, 100));
    }

    private void HandleOrInsertTask(string taskName, bool shouldExist, int priority)
    {
        var task = allTasks.Find(t => t.Name == taskName);
        if (shouldExist)
        {
            if (task != null)
            {
                taskQueue.DecreaseKey(task, priority);
            }
            else
            {
                var newTask = new Task(taskName, priority);
                taskQueue.Insert(newTask);
                allTasks.Add(newTask);
            }
        }
        else if (task != null)
        {
            taskQueue.DecreaseKey(task, int.MaxValue);
        }
    }

    private void ExecuteTopPriorityTask()
    {
        if (currentTask == null && !taskQueue.IsEmpty())
        {
            currentTask = taskQueue.ExtractMin();
            Debug.Log($"[NPC] Now executing: {currentTask.Name} (priority {currentTask.Priority})");
            StartCoroutine(ExecuteTask(currentTask));
        }
    }

    private IEnumerator ExecuteTask(Task task)
    {
        yield return new WaitForSeconds(1f);
        
        allTasks.Remove(task);
        currentTask = null;
    }

    private void HandleTaskMovement()
    {
        if (currentTask == null) return;

        Vector2 direction = Vector2.zero;

        switch (currentTask.Name)
        {
            case "Attack":
                direction = (player.position - transform.position).normalized;
                break;

            case "RunAway":
                direction = (transform.position - player.position).normalized;
                break;

            case "Search":
                if (searchDirection == Vector2.zero) 
                {
                    searchStartPosition = transform.position;
                }

                if (Vector2.Distance(transform.position, searchStartPosition) > searchDistance)
                {
                    searchDirection = -searchDirection;
                    searchStartPosition = transform.position;
                }

                direction = searchDirection;
                break;

            case "Heal":
                direction = Vector2.zero;
                break;
        }

        if (direction != Vector2.zero)
        {
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player = gameObject.transform;
            }
        }
    }
}
