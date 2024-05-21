using System;

[Serializable]
public class GridTypes {

    public enum Grid
    {
        Trigonal,
        Centroid,
        None
    }

    public Grid typeOfGrid;
    
}