using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeDescriptionUI
{
    public VisualElement Root { get; private set; }
    private Label _nameLabel;
    private VisualElement _iconElement;
    private Label _descriptionLabel;

    public string NameText {
        get => _nameLabel.text;
        set => _nameLabel.text = value;
    }

    public Sprite IconSprite {
        set => _iconElement.style.backgroundImage = new StyleBackground(value);
    }

    public string DescriptionText {
        get => _descriptionLabel.text;
        set => _descriptionLabel.text = value;
    }

    public UpgradeDescriptionUI(VisualElement root) {
        Root = root;
        _nameLabel = root.Q<Label>("Name");
        _iconElement = root.Q<VisualElement>("Icon");
        _descriptionLabel = root.Q<Label>("Description");
    }
}
