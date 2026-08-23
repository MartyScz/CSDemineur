
using System;
using Godot;

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

    // Place les mines en protégeant la case du premier clic et ses voisines
    public void PlaceMines(int safeRow, int safeColumn)
    {
        // Empêche de replacer les mines après leur première génération
        if (_minesPlaced)
        {
        return;
        }

        Random random = new Random();

        // Nombre de mines placées
        int minesPlaced = 0;

        // Continue les tentatives jusau'à avoir placé toutes les mines
        while (minesPlaced < _mineCount)
        {
            // Random pour lignes et colonnes
            int randomRow = random.Next(_rows);
            int randomColumn = random.Next(_columns);

            // Vérifie si la lignes et la colonne sont dans la zone protégée
            bool isInSafeRowRange = randomRow >= safeRow - 1 && randomRow <= safeRow + 1;
            bool isInSafeColumnRange = randomColumn >= safeColumn - 1 && randomColumn <= safeColumn + 1;

            // Indique si la position tirée appartient à la zone protégée
            bool isSafeZone = isInSafeColumnRange && isInSafeRowRange;

            // Ignore la position si elle se trouve dans la zone protégée
            if (isSafeZone)
            {
                continue;
            }

            // Ignore la position si une mine est déjà présente sur cette case
            if (_cases[randomRow, randomColumn].HasMine)
            {
                continue;
            }

            // Place une mine sur la case tirée
            _cases[randomRow, randomColumn].HasMine = true;

            //TODO Enlever quand les tests sont validés
            GD.Print($"Mine placée en [{randomRow}, {randomColumn}]");


            // Incrémente le nombre de mines réellement placées
            minesPlaced++;          
        }

        // Toutes les mines ont été placées
        _minesPlaced = true;

    }
}