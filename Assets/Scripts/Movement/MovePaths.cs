using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MovePaths : MonoBehaviour
{

    [SerializeField] private List<SplineContainer> _movePaths;


    private void Awake() {
        GameInstance.Instance.MovePaths = this;
    }

    public SplineContainer GetRandomMovePath() {

        int count = _movePaths.Count;
        if(count == 0) {
            return null;
        }
        int index = Random.Range(0,count);

        return _movePaths[index];
    }
}
