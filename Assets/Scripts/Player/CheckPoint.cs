using UnityEngine;



public class CheckPoint : MonoBehaviour
{
    [SerializeField] private bool _setAsStartEnemyPosition;

    //[SerializeField]
    //private List<GameObject> _destroyOn

    private void Awake() {
        if(_setAsStartEnemyPosition) {
            GameInstance.Instance.CurrentCheckPointPosition = transform;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        gameObject.SetActive(false);
        GameInstance.Instance.CurrentCheckPointPosition = transform;
    }
}
