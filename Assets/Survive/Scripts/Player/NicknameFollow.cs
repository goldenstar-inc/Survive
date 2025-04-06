using UnityEngine;
using TMPro;

/// <summary>
/// Класс, отвечающий за привязку ника к игроку
/// </summary>
public class NicknameFollow : MonoBehaviour
{
    [SerializeField] private Transform nicknameCanvas;
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.75f, 0f);


    public void Initialize(string nickName)
    {
        if (string.IsNullOrEmpty(nickName))
        {
            Debug.LogError("The nickname is null or empty");
        }
        nicknameText.text = nickName;
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    private void FixedUpdate()
    {
        if (nicknameCanvas != null && !string.IsNullOrEmpty(nicknameText.text))
        {
            nicknameCanvas.position = transform.position + offset;
        }
    }
}