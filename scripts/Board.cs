

public class Board
{
    // Les Lignes
    private int _rows;

    // Les colonnes
    private int _columns;

    // Tableau 2D
    private Case[,] _cases;

    // Les mines
    private int _mineCount;

    // Indique si les mines ont déja été placées sur le plateau
    private bool _minesPlaced = false;

    // Initialise le plateau avec son nombre de lignes, de colonnes et de mines
    public Board(int rows, int columns, int mineCount)
    {
        // Lignes
        _rows = rows;

        // Colonnes
        _columns = columns;

        // Mines
        _mineCount = mineCount;

        // Création du tableau 2D
        _cases = new Case[_rows, _columns];

        // Boucle pour parcourir le tableau
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                _cases[row, column] = new Case(row, column);
            }
        }
    }

    // Retourne la case située à la ligne et à la colonne demandées
    public Case GetCase(int row, int column)
    {
        return _cases[row, column];
    }

    // Place les mines à un écart de 8 case après le clic
    public void PlaceMines(int row, int column)
    {
        
    }
}