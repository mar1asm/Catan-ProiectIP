using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameDescriber
{
    public TileNumberDescriber[] availableTiles;
    public HexDescriber[] board;
    public AvailableToken[] availableTokens;
}
