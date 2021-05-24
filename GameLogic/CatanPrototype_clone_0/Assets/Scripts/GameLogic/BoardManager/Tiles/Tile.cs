using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Tile : ILoadVFX
{


    //Am facut un enum ca sa nu trebuiasca sa tinem minte
    //care offset corespunde carui colt..
    public enum Corners
    {
        TOP, TOP_RIGHT, BOTTOM_RIGHT, BOTTOM, BOTTOM_LEFT, TOP_LEFT
    };

    //offseturi folosite pentru a calcula coordonatele colturilor
    //ofseturile sunt in ordinea care sunt in enum-ul Corners

    public static BoardCoordinate[] cornerOffsets =
    {
        new BoardCoordinate(0.33f, -0.66f),
        new BoardCoordinate(0.66f, -0.33f),
        new BoardCoordinate(0.33f, 0.33f),
        new BoardCoordinate(-0.33f, 0.66f),
        new BoardCoordinate(-0.66f, 0.33f),
        new BoardCoordinate(-0.33f, -0.33f),

    };

    public ResourceTile resourceTile;

    public Corner[] corners = new Corner[6];

    //Coordonatele in q si r
    public BoardCoordinate coordinate;

    //Cum trebuie sa arate acest Tile?
    public GameObject VFX;

    public GameObject inGameObject;

    protected Tile(float q, float r)
    {
        coordinate = new BoardCoordinate(q, r);
        LoadVFX();
    }


    protected Tile(BoardCoordinate boardCoordinate)
    {
        coordinate = boardCoordinate;
        LoadVFX();
    }

    
    public abstract void LoadVFX();
    //Functie care spune care e actiunea speciala a acestui Tile
    public abstract void SpecialAction();

    public abstract string GetTypeAsString();


    //Functie care adauga VFX-ul tile-ului pe un anume obiect
    //Il si returnez, in cazul in care vrem sa adaugam lucruri la el
    //De exemplu, un numar
    public virtual GameObject AddVFX2Object(GameObject parent) 
    {   
        if(inGameObject == null)
        {
            inGameObject = parent;
        }
        return GameObject.Instantiate(VFX, parent.transform);
    }


  
}
