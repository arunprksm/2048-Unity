using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 4;
    [SerializeField] private NodeScript nodePrefab;
    [SerializeField] private SpriteRenderer boardPrefab; 

    private void Start()
    {
        GenerateGrid();
    }
    
    private void GenerateGrid()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                var node = Instantiate(nodePrefab, new Vector2(i,j),Quaternion.identity);
            }
        }
        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);
        var board = Instantiate(boardPrefab, center, Quaternion.identity);

        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);
    }
}
