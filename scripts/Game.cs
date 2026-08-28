using Godot;
using System;

public partial class Game : Control
{
    // Référence vers le GridContainer de la scène
    private GridContainer _grid;

    // Nombre de lignes
    private int _rows = 8;
    
    // Nombre de colonnes
    private int _columns = 8;

    // Nombre de mines de la partie
    private int _mineCount = 10;

    // Référence vers le plateau logique de la partie
    private Board _board;

    // Ressource Godot qui contient le modèle prêt à être instancié 
    private PackedScene _caseViewScene;

    // Initialise le jeu lorsque le noeud est prêt dans la scène
    public override void _Ready()
    {
        // Récupère la référence vers le GridContainer placé dans le CenterContainer
        _grid = GetNode<GridContainer>("CenterContainer/GridContainer");

        // Définit le nombre de colonnes utilisées pour afficher le plateau
        _grid.Columns = _columns;

        // Initialise le plateau logique
        _board = new Board(_rows, _columns, _mineCount);

        // Initialise la scène de la case
        _caseViewScene = GD.Load<PackedScene>("res://scenes/case_view.tscn");

        // Génère les cases qui composent le plateau
        CreateGrid();

    }

    // Méthode pour crée les cases du plateau
    private void CreateGrid()
    {
        // Boucle qui créer les cases du plateau
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                // Récupère la case logique correspondant aux coordonnées de la case en cours de création
                Case gameCase = _board.GetCase(row, column);

                // Crée une instance de la scène visuelle CaseView
                CaseView caseView = _caseViewScene.Instantiate<CaseView>();

                // Associe la case visuelle à sa case logique correspondante
                caseView.Initialize(gameCase);

                // Relie le clic de la case visuelle à sa case logique correspondante
                caseView.Pressed += () => OnCasePressed(gameCase);

                // Ajoute la CaseView à la grille pour l'afficher
                _grid.AddChild(caseView);        
            }
        }
    }

    // Reçoit et traite la case logique correspondant au bouton cliqué 
    private void OnCasePressed(Case gameCase)
    {
        // Place les mines sur le plateau
        _board.PlaceMines(gameCase.Row, gameCase.Column);

        // Révèle cette position et déclenche éventuellement une cascade
        _board.RevealCase(gameCase.Row, gameCase.Column);

        // Parcourt toutes les CaseView
        RefreshGrid();

        // TODO: Vérifie le nombre de mines voisines. Enlever après test validé
        GD.Print($"Case [{gameCase.Row}, {gameCase.Column}] : {gameCase.AdjacentMines} mine(s) voisine(s)");

        // Marque la case logique comme révélée
        gameCase.IsRevealed = true;
        
    }

    // Met à jour l'affichage de toutes les cases de la grille
    private void RefreshGrid()
    {
        // Parcourt tous les enfants du GridContainer
        foreach (Node child in _grid.GetChildren())
        {
            // Vérifie si l'enfant actuel est une CaseView
            if (child is CaseView caseView)
            {
                // Met à jour l'affichage de cette CaseView selon sa Case logique
                caseView.Refresh();
            }
        }
    }
}
