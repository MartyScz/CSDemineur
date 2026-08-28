

using System.Net;
using Godot;


public partial class CaseView : TextureButton
{
    [Export]
    // Ajoute une case de propriété dans l'inspecteur de Godot
    private Texture2D _revealedTexture;

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
        if (_gameCase.IsRevealed)
        {
            ShowRevealed();
        }
    }
}
