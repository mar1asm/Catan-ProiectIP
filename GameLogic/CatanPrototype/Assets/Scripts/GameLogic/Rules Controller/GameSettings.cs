using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    private int PlayerCardNumber;
    private int TotalCardNumber;
    private int HexagonNumbers;
    private int DiceValue;
    private int secperRound;
    private int MaxTilesBetweenRoad;
    private int MaxTileBetweenLocation;
    private double DefaultTradeRatio;

    // Start is called before the first frame update
    public void SetPlayerCardNumber(int value)
    {
        PlayerCardNumber = value;
    }
    public void SetTotalCardNumber(int value)
    {
        TotalCardNumber = value;
    }
    public void SetMaximumDiceValue(int value)
    {
        DiceValue = value;
    }
    public void SetMaxTilesBetweenRoads(int value)
    {
        MaxTilesBetweenRoad = value;

    }
    public void SetMaxTilesBetweenLocation(int value)
    {
        MaxTileBetweenLocation = value;
    }

    public void SetSecondsperRound(int time)
    {
        secperRound = time;
    }

    public void SetMaxHexagonNumbers(int value)
    {
        HexagonNumbers = value;

    }

    public void SetDefaultTradeRatio(double ratio)
    {
        DefaultTradeRatio = ratio;
    }

    public int getPlayerCardNumber()
    {
        return PlayerCardNumber;
    }
    public int getTotalCardNumber()
    {
        return TotalCardNumber;
    }
    public int getMaximumDiceValue()
    {
        return DiceValue;
    }
    public int getMaxTilesBetweenRoads()
    {
        return MaxTilesBetweenRoad;

    }
    public int getMaxTilesBetweenLocation()
    {
        return MaxTileBetweenLocation;
    }

    public int getSecondsperRound()
    {
        return secperRound;
    }

    public int getMaxHexagonNumbers()
    {
        return HexagonNumbers;

    }

    public double getDefaultTradeRatio()
    {
        return DefaultTradeRatio;
    }
}
