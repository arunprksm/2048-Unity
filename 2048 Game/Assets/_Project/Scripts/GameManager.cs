using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 4;

    [SerializeField] private NodeScript nodePrefab;
    [SerializeField] private BlockScripts blockPrefab;

    [SerializeField] private SpriteRenderer boardPrefab;
    [SerializeField] private List<BlockType> BlockTypes;

    private List<NodeScript> nodes;
    private List<BlockScripts> blocks;
    private int round;

    private GameState state;

    private BlockType GetBlockTypeByValue(int value) => BlockTypes.First(t => t.value == value);
    private void Start()
    {
        ChangeState(GameState.GenerateLevel);

    }

    private void ChangeState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.GenerateLevel: GenerateGrid();
                break;
            case GameState.SpawnBlocks:   SpawnBlocks(round++ == 0 ? 2 : 1);
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
        round = 0;
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

        ChangeState(GameState.SpawnBlocks);
    }

    void SpawnBlocks(int amount)
    {
        var freeNodes = nodes.Where(n => n.occupiedBlock == null).OrderBy(b => Random.value).ToList();
        foreach (var node in freeNodes.Take(amount))
        {
            BlockScripts block = Instantiate(blockPrefab, node.Pos, Quaternion.identity);
            block.Intialize(GetBlockTypeByValue(Random.value > 0.8f ? 4 : 2));
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
