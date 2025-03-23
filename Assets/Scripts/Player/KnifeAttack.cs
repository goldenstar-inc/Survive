using System.Collections;
using UnityEngine;
using static InventoryController;

/// <summary>
/// �����, ���������� �� ����� �������� �������
/// </summary>
public class KnifeAttack : MonoBehaviour
{
    /// <summary>
    /// ����������, ���������� �� ������� ������ �����
    /// </summary>
    private bool isAttacking = false;

    /// <summary>
    /// ������, ���������� ��������
    /// </summary>
    public GameObject attackingRange;

    /// <summary>
    /// ��������, ���������� �� �������� �����
    /// </summary>
    public Animator attackingAnimator;

    /// <summary>
    /// Скрипт, представляющий инвентарь игрока
    /// </summary>
    private InventoryController inventoryController;

    /// <summary>
    /// �����, �������� ��������� �������
    /// </summary>
    private void ChangeVisibility() => attackingRange.SetActive(isAttacking);

    /// <summary>
    /// Время последнего проигрывания звука
    /// </summary>
    private float lastSwingTime = 0f;

    /// <summary>
    /// Интервал между звуками
    /// </summary>
    private float swingInterval = 0.1f;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    private void Start()
    {
        inventoryController = GetComponent<InventoryController>();

        if (inventoryController == null)
        {
            Debug.LogWarning("InventoryController not loaded");
        }
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButton(0) && !isAttacking)
        {
            if (inventoryController.GetCurrentPickableItem() != null)
            {
                if (inventoryController.GetCurrentPickableItem().UniqueName == PickableItems.Knife)
                { 
                    isAttacking = true;
                    ChangeVisibility();

                    if (Time.time - lastSwingTime > swingInterval)
                    {
                        PlaySwingingKnifeSound();
                        lastSwingTime = Time.time;
                    }
                }
            }
        }

        if (isAttacking && attackingAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            isAttacking = false;
            ChangeVisibility();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        ChangeVisibility();

        if (Time.time - lastSwingTime > swingInterval)
        {
            PlaySwingingKnifeSound();
            lastSwingTime = Time.time;
        }
    }

    /// <summary>
    /// Проигрывает звук размахивания холодным оружием
    /// </summary>
    private void PlaySwingingKnifeSound()
    {
        if (!SoundController.Instance.weaponAudioSource.isPlaying)
        {
            SoundController.Instance.PlayRandomSwingingKnifeSound();
        }
    }
}