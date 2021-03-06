using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardCoordinate
{

    /*
     * Obiect folosit ca referinta pentru calcule.
     * Trebuie sa stim cat de mare e hexagonul ca sa putem pune unde trebuie pe board
     */

    private static GameObject measurementsReference;
    private static float widthOfTile =  0.01004f * 11048.06f * 0.3f;
    public float q;
    public float r;

    public BoardCoordinate()
    {
        q = 0;
        r = 0;
    }

    public BoardCoordinate(float q, float r)
    {
        this.q = q;
        this.r = r;
    }

    public override int GetHashCode()
    {
        int hash = 89;
        hash = hash * 97 + Mathf.FloorToInt(q * 10).GetHashCode();
        hash = hash * 97 + Mathf.FloorToInt(r * 10).GetHashCode();

        //Debug.Log(q + " " + r + "=>" + hash);
        return hash;
    }

    public override bool Equals(object obj)
    {
        BoardCoordinate other = (BoardCoordinate)obj;

        return this == other;
    }

    public static bool operator !=(BoardCoordinate a, BoardCoordinate b)
    {
        return !(a == b);
    }
    public static bool operator ==(BoardCoordinate a, BoardCoordinate b)
    {
        return (Mathf.Abs(a.q - b.q) <= 0.01) && (Mathf.Abs(a.r - b.r) <= 0.01);
    }
    public static BoardCoordinate operator +(BoardCoordinate a, BoardCoordinate b)
    {
        return new BoardCoordinate(a.q + b.q, a.r + b.r);
    }

    public static BoardCoordinate operator -(BoardCoordinate a, BoardCoordinate b)
    {
        return new BoardCoordinate(a.q - b.q, a.r - b.r);
    }

    public static BoardCoordinate operator -(BoardCoordinate a)
    {
        return new BoardCoordinate(-a.q, -a.r);
    }

    public static BoardCoordinate operator /(BoardCoordinate a, float f)
    {
        return new BoardCoordinate(a.q / f, a.r / f);
    }

    /// <summary>
    /// Functie care converteste din coordonatele pe care le folosim in 
    /// coordonatele din unity
    /// </summary>
    /// <returns></returns>
    public Vector3 ToWorldSpace()
    {
    
        float widthOfHex = widthOfTile / 2;

        //deltaX reprezinta Offset-ul pe X atunci cand mergem pe diagonala
        float deltaX = Mathf.Sqrt(3) * widthOfHex;
        Vector3 worldPosition = new Vector3(0, 0, 0);
        worldPosition.z += -q * 2 * widthOfHex;
        worldPosition.x += -r * deltaX;
        worldPosition.z += -r * widthOfHex;

        return worldPosition;
    }

    /// <summary>
    /// Transforma o positie din lumea unity in coordonate de pe masa
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static BoardCoordinate ToBoardCoordinate(Vector3 position)
    {
        float widthOfHex = widthOfTile / 2;

        //deltaX reprezinta Offset-ul pe X atunci cand mergem pe diagonala
        float deltaX = Mathf.Sqrt(3) * widthOfHex;

        float r = -(position.x / deltaX);

        float z = position.z;

        z -= -r * widthOfHex;


        float q = -(z / (2 * widthOfHex));

        return new BoardCoordinate(q, r);
    }
    
}
