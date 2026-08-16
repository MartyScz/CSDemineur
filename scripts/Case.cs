using Godot;
using System;

public partial class Case : Node2D
{
    public bool IsRevealed { get; set; }
    public bool HasMine { get; set; }
    public int AdjacentMines { get; set; }
    public bool HasFlag { get; set; }
}
