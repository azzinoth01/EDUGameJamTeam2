using UnityEngine;

public class GameInstance
{

    private static GameInstance _instance;

    private MovePaths _movePaths;
    private Player _player;
    private Transform _currentCheckPointPosition;


    public static GameInstance Instance {
        get {
            if(_instance == null) {
                _instance = new GameInstance();
            }
            return _instance;
        }
    }

    public MovePaths MovePaths {
        get {
            return _movePaths;
        }

        set {
            _movePaths = value;
        }
    }

    public Player Player {
        get {
            return _player;
        }

        set {
            _player = value;
        }
    }

    public Transform CurrentCheckPointPosition {
        get {
            return _currentCheckPointPosition;
        }

        set {
            _currentCheckPointPosition = value;
        }
    }

    private GameInstance() {
    }

}
