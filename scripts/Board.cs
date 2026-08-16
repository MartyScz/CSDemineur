

public class Board
{
    // Les Lignes
    private int _rows;

    // Les colonnes
    private int _columns;

    // Tableau 2D
    private Case[,] _cases;

    // Initialise le plateau avec son nombre de lignes et de colonnes
    public Board(int rows, int columns)
    {
        // lignes
        _rows = rows;

        // colonnes
        _columns = columns;

        // Création du tableau 2D
        _cases = new Case[_rows, _columns];

        // Boucle pour parcourir le tableau
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                _cases[row, column] = new Case();
            }
        }
    }

}