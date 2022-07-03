using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public Vector2 Pos => transform.position;
    public BlockScripts occupiedBlock;
}
