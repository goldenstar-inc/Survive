using UnityEngine;
using TMPro;

/// <summary>
/// Класс, отвечающий за привязку ника к игроку
/// </summary>
public class NicknameFollow : MonoBehaviour
{
    private Transform nicknameCanvas;
    private TextMeshProUGUI nicknameText;
    private Vector3 offset = new Vector3(0f, 0.75f, 0f);
    public void Init(TextMeshProUGUI nicknameText, Transform nicknameCanvas, string nickName = "Mark")
    {
        this.nicknameText = nicknameText;
        this.nicknameCanvas = nicknameCanvas;
        nicknameText.text = nickName;
    }

    /// <summary>
    /// Метод, вызывающийся каждый игровой кадр
    /// </summary>
    private void Update()
    {
        if (nicknameCanvas != null && !string.IsNullOrEmpty(nicknameText.text))
        {
            nicknameCanvas.position = transform.position + offset;
        }
    }
}