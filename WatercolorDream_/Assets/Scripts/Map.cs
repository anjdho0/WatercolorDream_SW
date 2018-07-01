using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    public List<TileTypes[]> map;
    public List<Color[]> areaColors;
    public List<NextAreaPos> areasPos;
    public bool isCleared = false;

    public Map()
    {
        map = new List<TileTypes[]>();
        areasPos = new List<NextAreaPos>();
        areaColors = new List<Color[]>();
    }

    public void AddArea(TileTypes[] area, NextAreaPos nextPos, Color[] colors)
    {
        map.Add(area);
        areasPos.Add(nextPos);
        areaColors.Add(colors);
    }
    
    public void AddArea(int[] area, int nextPos, Color[] colors)
    {
        TileTypes[] area_ = new TileTypes[7];

        for(int i = 0; i < 7; i++)
        {
            if (area[i] == 0)
                area_[i] = TileTypes.empty;
            if (area[i] == 1)
                area_[i] = TileTypes.normaltile;
            if (area[i] == 2)
                area_[i] = TileTypes.starttile;
            if (area[i] == 3)
                area_[i] = TileTypes.desttile;
        }
        map.Add(area_);

        switch (nextPos)
        {
            case 0:
                areasPos.Add(NextAreaPos.none);
                break;
            case 1:
                areasPos.Add(NextAreaPos.left);
                break;
            case 2:
                areasPos.Add(NextAreaPos.leftdown);
                break;
            case 3:
                areasPos.Add(NextAreaPos.rightdown);
                break;
            case 4:
                areasPos.Add(NextAreaPos.right);
                break;
        }

        areaColors.Add(colors);
    }
	
}

public enum TileTypes{ empty, normaltile, starttile, desttile };

public enum NextAreaPos { none, left, leftdown, rightdown, right};
