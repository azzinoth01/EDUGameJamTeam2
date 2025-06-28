using TMPro;
using UnityEngine;

public class DisplayGoldValue : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueDisplay;


    // Update is called once per frame
    void Update() {
        _valueDisplay.text = GameInstance.Instance.Player.Gold.ToString();
    }
}
