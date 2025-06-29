using TMPro;
using UnityEngine;

public class DisplayGoldValue : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueDisplay;
    void Update() {
        _valueDisplay.text = GameInstance.Instance.Player.Gold.ToString();
    }
}
