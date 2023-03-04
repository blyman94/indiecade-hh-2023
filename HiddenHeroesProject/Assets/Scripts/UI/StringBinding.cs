using TMPro;
using UnityEngine;

public class StringBinding : MonoBehaviour
{
    [SerializeField] private StringVariable _observedString;
    [SerializeField] private TextMeshProUGUI _boundText;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _observedString.VariableUpdated += UpdateBoundText;
    }
    private void Start()
    {
        UpdateBoundText();
    }
    private void OnDisable()
    {
        _observedString.VariableUpdated -= UpdateBoundText;
    }
    #endregion

    private void UpdateBoundText()
    {
        _boundText.text = _observedString.Value;
    }
}
