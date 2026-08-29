

using System.Net;
using Godot;


public partial class CaseView : TextureButton
{
    // Ajoute une case de propriété dans l'inspecteur de Godot
    [Export]
    private Texture2D _revealedTexture;

    // Contient les textures des chiffres de 1 à 8
    [Export]
    private Texture2D[] _numberTextures;

    // Référence vers le TextureRect utilisé pour afficher le contenu de la case
    private TextureRect _content;

    // Référence vers la case logique représentée par cette CaseView
    private Case _gameCase;

    // Initialise les références vers les noeuds nécessaires lorsque CaseView est prêt
    public override void _Ready()
    {
        // Récupère le TextureRect "Content" présent dans la scène CaseView
        _content = GetNode<TextureRect>("Content");
    }

    // Associe cette case visuelle à la case logique qu' elle représente 
    public void Initialize(Case gameCase)
    {
        _gameCase = gameCase;
    }

    // Affiche la texture correspondant à une case révélée
    public void ShowRevealed()
    {
        // Remplace le fond de la case par la texture de case vide
        TextureNormal = _revealedTexture;
    }

    // Met à jour l'état visuel de la case selon son état logique
    public void Refresh()
    {
        // Met à jour l'affichage uniquement si la case est révélée
        if (_gameCase.IsRevealed)
        {
            // Affiche le don d'une case révélée
            ShowRevealed();

            // Affiche un chiffre uniquement si une ou plusieurs mines sont adjacentes
            if (_gameCase.AdjacentMines > 0)
            {
                // Convertit le nombre de mines adjacentes en index du tableau de textures
                int textureIndex = _gameCase.AdjacentMines - 1;

                // Affiche la texture du chiffre correspondant
                _content.Texture = _numberTextures[textureIndex];
            }
        }

    }
}
