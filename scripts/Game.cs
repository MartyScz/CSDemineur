using Godot;


public partial class Game : Control
{
    // Référence vers le GridContainer de la scène
    private GridContainer _grid;

    // Nombre de lignes
    private int _rows = 8;
    
    // Nombre de colonnes
    private int _columns = 8;

    // Référence vers le plateau logique de la partie
    private Board _board;

    // Initialise le jeu lorsque le noeud est prêt dans la scène
    public override void _Ready()
    {
        // Récupère la référence vers le GridContainer placé dans le CenterContainer
        _grid = GetNode<GridContainer>("CenterContainer/GridContainer");

        // Définit le nombre de colonnes utilisées pour afficher le plateau
        _grid.Columns = _columns;

        // Initialise le plateau logique
        _board = new Board(_rows, _columns);

        // Génère les cases qui composent le plateau
        CreateGrid();
    }

    // Méthode pour crée les cases du plateau
    private void CreateGrid()
    {
        // Boucle qui créer les cases du plateau
        for (int i = 0; i < _rows * _columns; i++)
        {
            // Crée un nouveau bouton
            Button button = new Button
            {
                // "?" est la valeur de Text
                Text = "?",

                // Taille minimale du bouton
                CustomMinimumSize = new Vector2(40, 40)
            };

            // Donne ce bouton à la grille comme enfant
            _grid.AddChild(button);        
        }
    }

}
