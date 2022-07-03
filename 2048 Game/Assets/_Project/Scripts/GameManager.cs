using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 4;
    [SerializeField] private NodeScript nodePrefab;
    [SerializeField] private BlockScripts blockPrefab;
    [SerializeField] private SpriteRenderer boardPrefab;
    [SerializeField] private List<BlockType> types;

    private List<NodeScript> nodes;
    private List<BlockScripts> blocks;

    private GameState state;

    private BlockType getBlockTypeByValue(int value) => types.First(t => t.value == value);
    private void Start()
    {
        GenerateGrid();
    }

    private void ChangeState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.GenerateLevel:
                break;
            case GameState.SpawnBlocks:
                break;
            case GameState.WaitingInput:
                break;
            case GameState.Moving:
                break;
            case GameState.Lose:
                break;
                default:
                break;
        }
    }
    private void GenerateGrid()
    {
        nodes = new List<NodeScript>();
        blocks = new List<BlockScripts>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var node = Instantiate(nodePrefab, new Vector2(i, j), Quaternion.identity);
                nodes.Add(node);
            }
        }
        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);
        var board = Instantiate(boardPrefab, center, Quaternion.identity);

        board.size = new Vector2(width, height);

        Camera.main.transform.position = new Vector3(center.x, center.y, -10);

        SpawnBlocks(2);
    }

    void SpawnBlocks(int amount)
    {
        var freeNodes = nodes.Where(n => n.occupiedBlock == null).OrderBy(b => UnityEngine.Random.value).ToList();
        foreach (var node in freeNodes.Take(amount))
        {
            var block = Instantiate(blockPrefab, node.Pos, Quaternion.identity);
            block.Intialize(getBlockTypeByValue(2));
        }

        if (freeNodes.Count() == 1)
        {
            return;
        }
    }
}

[Serializable]
public struct BlockType
{
    public int value;
    public Color color;
}

public enum GameState
{
    GenerateLevel,
    SpawnBlocks,
    WaitingInput,
    Moving,
    Win,
    Lose
}
