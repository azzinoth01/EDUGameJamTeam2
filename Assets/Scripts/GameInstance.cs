public class GameInstance
{

    private static GameInstance _instance;

    private MovePaths _movePaths;


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

    private GameInstance() {
    }

}
