
using System;
using System.Data;
using System.Security.Principal;
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

        CalculateAdjacentMines();

        // Toutes les mines ont été placées
        _minesPlaced = true;
    }

    // Calcule le nombre de mines adjacentes pour chaque case du plateau
    private void CalculateAdjacentMines()
    {
        //  Parcourt toutes les lignes du plateau
        for (int row = 0; row < _rows; row++)
        {
            // Parcourt toutes les colonnes du plateau
            for (int column = 0; column < _columns; column++)
            {
                // Compte le nombre de mines autour de la case actuelle
                int adjacentMines = 0;
                
                // Parcourt les lignes voisines de la case actuelle
                for (int neighborRow = row - 1; neighborRow <= row  + 1; neighborRow++)
                {   
                    // Parcourt les colonnes voisines de la case actuelle
                    for (int neighborColumn = column - 1; neighborColumn <= column + 1; neighborColumn++)
                    {

                        // Vérifie que la position voisine reste dans les limites du plateau
                        bool isInsideBoard = neighborRow >= 0 && neighborRow < _rows && neighborColumn >= 0 && neighborColumn <_columns;

                        // Ignore cette position si elle se trouve hors du plateau
                        if (!isInsideBoard)
                        {
                            continue;
                        }

                        // Vérifie si la position voisine correspond à la case actuellement analysée
                        bool isCurrentCase = neighborRow == row && neighborColumn == column;

                        // Ignore la case elle-même :  elle ne doit pas être comptée comme voisine
                        if (isCurrentCase)
                        {
                            continue;
                        }

                        // Compte la case voisine uniquement si elle contient une mine
                        if (_cases[neighborRow, neighborColumn].HasMine)
                        {
                            adjacentMines++;
                        }
                    }
                }
                // Enregistre le nombre total de mines trouvées autour de la case
                _cases[row, column].AdjacentMines = adjacentMines;
            }
        }
    }

    // Révèle une case et propage la révélation si elle n'a aucune mine adjacente
    public void RevealCase(int row, int column)
    {
        // Récupère la case située aux coordonnées reçues
        Case currentCase = _cases[row, column];

        // Arrête si la case est déjà révélée
        if (currentCase.IsRevealed)
        {
            return;
        }

        // Marque la case révélée
        currentCase.IsRevealed = true;

        // Arrête la propagation si la case actuelle possède au moins une mine adjacente
        if (currentCase.AdjacentMines != 0)
        {
            return;
        }

        // Parcourt les lignes voisines de la case actuelle
        for (int neighborRow = row - 1; neighborRow <= row + 1; neighborRow++)
        {
            // Parcourt les colonnes voisines de la case actuelle
            for (int neighborColumn = column - 1; neighborColumn <= column +1; neighborColumn++)
            {
                // Vérifie que la position voisine reste dans les limites du plateau
                bool isInsideBoard = neighborRow >= 0 && neighborRow < _rows && neighborColumn >= 0 && neighborColumn < _columns;

                // Ignore les positions situées hors du plateau
                if (!isInsideBoard)
                {
                    continue;
                }

                // Vérifie si la position correspond à la case actuellement révélée
                bool isCurrentCase = neighborRow == row && neighborColumn == column;

                // Ignore la case elle-même :  elle ne doit pas être traitée comme voisine
                if (isCurrentCase)
                {
                    continue;
                }

                RevealCase(neighborRow, neighborColumn);
            }
        }
    }
}