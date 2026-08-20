using Godot;
using System;

public partial class Case : Node2D
{
    public bool IsRevealed { get; set; }
    public bool HasMine { get; set; }
    public int AdjacentMines { get; set; }
    public bool HasFlag { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }

    
    public Case(int row, int column)
    {
        Row = row;
        Column = column;
    }
}
