using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    public List<TileTypes[]> map;

    public Map()
    {
        map = new List<TileTypes[]>();
    }

    public void AddArea(TileTypes[] area)
    {
        map.Add(area);
    }
    
    public void AddArea(int[] area)
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
    }
	
}

public enum TileTypes{ empty, normaltile, starttile, desttile };
