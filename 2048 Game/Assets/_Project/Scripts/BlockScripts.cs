using System.Collections;
using UnityEngine;
public class BlockScripts : MonoBehaviour
{
    public int value;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMPro.TextMeshPro text;
    public void Intialize(BlockType blockType)
    {
        value = blockType.value;
        spriteRenderer.color = blockType.color;
        text.text = blockType.value.ToString();
    }
}
