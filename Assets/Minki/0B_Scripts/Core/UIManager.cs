using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoSingleton<UIManager>
{
    [field: SerializeField] public UXMLHelperSO UxmlHelper { get; private set; }

    private UIDocument _document;

    private VisualElement _root;
    private VisualElement _stageLabel;
    private UpgradeDescriptionUI _upgradeDescription;

    private void Awake() {
        _document = GetComponent<UIDocument>();
    }

    private void Start() {
        _root = _document.rootVisualElement.Q<VisualElement>("Container");

        _stageLabel = _root.Q<VisualElement>("StageLabel");

        var upgradeDescription = UxmlHelper.GetTree(UXML.Upgrade).Instantiate();
        _root.Add(upgradeDescription);

        _upgradeDescription = new UpgradeDescriptionUI(upgradeDescription);
        _upgradeDescription.Root.style.display = DisplayStyle.None;
    }

    public void ShowStageLabel(string name) {
        _stageLabel.Q<Label>("StageText").text = name;

        StartCoroutine(ShowStageLabelCoroutine());
    }

    private IEnumerator ShowStageLabelCoroutine() {
        _stageLabel.AddToClassList("on");

        yield return new WaitForSeconds(1.5f);

        _stageLabel.RemoveFromClassList("on");
    }

    public void ActiveUpgradeDescription(UpgradeSO so, Vector2 worldPosition) {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        screenPosition = new Vector2(screenPosition.x, Screen.height - screenPosition.y);

        _upgradeDescription.Root.style.left = screenPosition.x;
        _upgradeDescription.Root.style.top = screenPosition.y;

        _upgradeDescription.NameText = so.name;
        _upgradeDescription.IconSprite = so.icon;
        _upgradeDescription.DescriptionText = so.description;

        _upgradeDescription.Root.style.display = DisplayStyle.Flex;
    }

    public void DeactiveUpgradeDescription() {
        _upgradeDescription.Root.style.display = DisplayStyle.None;
    }
}
