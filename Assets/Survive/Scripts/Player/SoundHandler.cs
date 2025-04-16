using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class SoundHandler : MonoBehaviour
{
    private SoundController soundController;
    private AudioClip[] stepSounds { get; set; }
    private float stepInterval { get; set; }
    private float timeSinceLastStep = 0f;

    /// <summary>
    /// Инициализация
    /// </summary>
    /// <param name="stepSounds">Фасад данных об игроке</param>
    /// <param name="stepInterval">Интервал между шагами</param>
    public void Init(
        AudioClip[] stepSounds, 
        float stepInterval, 
        SoundController soundController, 
        QuestManager questManager
        )
    {
        this.stepSounds = stepSounds;
        this.stepInterval = stepInterval;
        this.soundController = soundController;
        questManager.OnQuestCompleted += PlayQuestCompleteSound;
    }
    public void PlayStepSoundIfNeeded(Vector3 movement)
    {
        if (movement != Vector3.zero && Time.time - timeSinceLastStep > stepInterval)
        {
            soundController?.PlayRandomSound(stepSounds);
            timeSinceLastStep = Time.time;
        }
    }
    public void PlayQuestCompleteSound(Quest quest)
    {
        if (quest is DeliveryQuest deliveryQuest)
        {
            soundController?.PlayAudioClip(deliveryQuest.QuestComplete);
        }
        else if (quest is ExplorationQuest explorationQuest)
        {
            soundController?.PlayAudioClip(explorationQuest.QuestComplete);
        }
    }
}
