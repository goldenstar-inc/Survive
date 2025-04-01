using UnityEngine;

/// <summary>
/// Класс, контроллирующий порядок слоев
/// </summary>
public class SortingLayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetSortingLayer(string layerName)
    {
        spriteRenderer.sortingLayerName = layerName;
    }
}
