using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ResqueQuest : IQuest, IDisposable
{
    private ResqueQuestConfig questConfig;
    public QuestConfig QuestConfig => questConfig;
    public event Action OnCompleted;
    private QuestManager questManager;
    private QuestEvents questEvents;
    private int maxProgress => questConfig.MaxProgress;
    private List<HealthHandler> questEnemies = new();
    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="questConfig">Конфиг задания</param>
    /// <param name="questManager">Менеджер заданий</param>
    /// <param name="questEvents">Event Bus заданий игрока</param>
    public ResqueQuest(ResqueQuestConfig questConfig, QuestManager questManager, QuestEvents questEvents)
    {
        this.questConfig = questConfig;
        this.questManager = questManager;
        this.questEvents = questEvents;
        CreateQuestNPC();
        CreateQuestEnemy();
        questEvents.OnCreatureKilled += UpdateProgress;
    }

    /// <summary>
    /// Метод, обновляющий прогресс
    /// </summary>
    /// <param name="enemyType">Тип врага</param>
    /// <param name="enemy">Объект класса, управляющий здоровьем</param>
    private void UpdateProgress(CreatureType enemyType, HealthHandler enemy)
    {
        if (questEnemies.Contains(enemy))
        {
            questEnemies.Remove(enemy);
            int currentProgress = maxProgress - questEnemies.Count;
            questManager?.UpdateProgress(this, currentProgress);
        }

        if (questEnemies.Count == 0)
        {
            CompleteQuest(this);
        }
    }

    /// <summary>
    /// Метод, создающий спасаемого NPC
    /// </summary>
    private void CreateQuestNPC()
    {
        Spawner.Instance.Spawn(questConfig.ResqueNPC, questConfig.Coords, Quaternion.identity);
    }

    /// <summary>
    /// Метод, создающий врагов
    /// </summary>
    private void CreateQuestEnemy()
    {
        for (int i = 0; i < maxProgress; i++)
        {
            GameObject enemy = Spawner.Instance.Spawn(questConfig.EnemyPrefab, GenerateRandomPointInRing(questConfig.Coords, 3, 10), Quaternion.identity);
            HealthHandler healthHandler = enemy.GetComponentInChildren<HealthHandler>();

            if (healthHandler != null)
            {
                questEnemies.Add(healthHandler);
            }
        }
    }

    /// <summary>
    /// Метод, генерирующий координаты случайной точки в кругу по заданным условиям
    /// </summary>
    /// <param name="center">Центр круга</param>
    /// <param name="minDistance">Минимальная дистанция от центра</param>
    /// <param name="maxDistance">Максимальная дистанция от центра</param>
    /// <returns>Координаты случайной точки</returns>
    private Vector3 GenerateRandomPointInRing(Vector2 center, float minDistance, float maxDistance)
    {
        float randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        float randomRadius = Mathf.Sqrt(UnityEngine.Random.Range(minDistance * minDistance, maxDistance * maxDistance));
        float x = center.x + randomRadius * Mathf.Cos(randomAngle);
        float y = center.y + randomRadius * Mathf.Sin(randomAngle);
        return new Vector3(x, y);
    }

    /// <summary>
    /// Метод завершения квеста
    /// </summary>
    public void CompleteQuest(IQuest _)
    {
        OnCompleted?.Invoke();
        Dispose();
    }

    public void Dispose()
    {
    }
}
