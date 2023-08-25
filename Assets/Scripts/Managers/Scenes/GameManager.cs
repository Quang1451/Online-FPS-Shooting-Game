/*using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using MEC;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    [Header("Asset References:")]
    [SerializeField] private AssetReference mapAsset;

    [Header("References:")]
    [SerializeField] public CinemachineVirtualCamera CVCamera;
    [SerializeField] public PoolDataSO poolDataSO;
    [SerializeField] public Map map;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _newGameTrigger;
    [SerializeField] private string _startGameTrigger;

    private List<MVCIController> _listCharacter;

    public PlayerController playerController;

    public Action GameStart;
    public Action GamePasue;
    public Action GameResume;
    public Action GameEnd;
    public Action<string> GameMessage;

    private void Awake()
    {
        //Clear All UI
        UIManager.Instance.ClearAllUi();

        InputManager.EnableUIActions();
    }

    private void Start()
    {

        Initialzie();
        *//*mapAsset.InstantiateAsync().Completed += (handle) =>
        {
            map = handle.Result.GetComponent<Map>();
            StartGame();
        };*//*

        InitializeGame();

        InitializeAction();
    }

    private void Initialzie()
    {
        _listCharacter = new List<MVCIController>();
        poolDataSO.Initialize();
    }

    #region Game Methods

    private void InitializeGame()
    {
        //Spawn black side
        CreateAllPieces(LevelData.GetListKey(LevelData.dictionary[LevelData.chooseLevel].black), Side.Black);
        //Spawn white side
        CreateAllPieces(LevelData.GetListKey(LevelData.dictionary[LevelData.chooseLevel].white), Side.White);
    }

    private void InitializeAction()
    {
        GameStart += StartGame;
        GameMessage += ShowMessage;
    }

    private void ShowMessage(string message)
    {
        Debug.Log(message);
    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        //Change CM camera
        _animator.SetTrigger(_startGameTrigger);

        //Lock and Hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Show GameplayUI
        Timing.RunCoroutine(ShowGameplayUI());
    }

    IEnumerator<float> ShowGameplayUI()
    {
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(UIManager.Instance.LoadAndShow<GameplayUI>(UIKey.GAMEPLAYUI, null, new UIShowData
        {
            ParentType = ParentType.Overlay
        })));

        GameplayUI gameplayUI = UIManager.Instance.Get<GameplayUI>(UIKey.GAMEPLAYUI);
        gameplayUI?.RegisterEvent(GameplayUI.PAUSE, PauseGame);
        gameplayUI?.RegisterEvent(GameplayUI.RESUME, ResumeGame);
        gameplayUI?.RegisterEvent(GameplayUI.QUIT, ReturnToMenu);
    }

    private void FinishCountDoane(object a)
    {

    }

    private void SpawnEnemies()
    {
    }

    private void PauseGame(object obj)
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame(object obj)
    {
        Time.timeScale = 1f;
    }

    private void EndGame()
    {
    }

    [Button]
    private void Replay()
    {
    }

    #endregion

    private void Update()
    {
        InputManager.Update();
        foreach (var character in _listCharacter)
        {
            character.Update();
        }
    }

    private void FixedUpdate()
    {
        foreach (var character in _listCharacter)
        {
            character.FixedUpdate();
        }
    }

    public void CreateAllPieces(List<string> keys, Side side, Action<object> callback = null)
    {
        if (keys == null) return;

        foreach (string key in keys)
        {
            string position = key.Substring(key.Length - 2);
            switch (key.Substring(0, 1))
            {
                case "V":
                    CreatePiece(PieceType.King, side, position);
                    break;
                case "H":
                    CreatePiece(PieceType.Quen, side, position);
                    break;
                case "T":
                    CreatePiece(PieceType.Bishop, side, position);
                    break;
                case "M":
                    CreatePiece(PieceType.Knight, side, position);
                    break;
                case "X":
                    CreatePiece(PieceType.Rook, side, position);
                    break;
                default:
                    CreatePiece(PieceType.Pawn, side, position);
                    break;
            }
        }
        callback?.Invoke(null);
    }

    public async void CreatePiece(PieceType pieceType, Side side, string position)
    {
        CharacterType type = CharacterType.Obstacle;
        if(pieceType == PieceType.King && side == Side.Black)
        {
            type = CharacterType.Player;
        }
        else if(side == Side.White)
        {
            type = CharacterType.Enemy;
        }

        //create model
        var model = MVCFactory.CreateModel(type);

        //create view
        var view = await MVCFactory.CreateView(pieceType, side);
        
        view.SetPosition(map.GetPositionFormString(position));
        
        //create controller
        var controller = MVCFactory.CreateController(type);
     
        controller.SetModel(model);
        controller.SetView(view);
        controller.Initialize();
        _listCharacter.Add(controller);

        if (type == CharacterType.Player) playerController = (PlayerController)controller;
    }

    private void ReturnToMenu(object obj)
    {
        Timing.RunCoroutine(LoadScene(SwitchSceneManager.MAINMENU, LoadSceneMode.Single));
    }

    IEnumerator<float> LoadScene(string key, LoadSceneMode mode)
    {
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(UIManager.Instance.LoadAndShow<LoadingUI>(UIKey.LOADINGUI,
            null, new UIShowData
            {
                ParentType = ParentType.Overlay
            })));
        LoadingUI loadingUI = UIManager.Instance.Get<LoadingUI>(UIKey.LOADINGUI);
        loadingUI.SetPercent(0f);
        Timing.RunCoroutine(SwitchSceneManager.LoadSceneAsync(key, mode, loadingUI));
    }
}

public static class MVCFactory
{
    public static MVCIData CreateModel(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
                var playerData = new PlayerModel();

                //Apply Player simple Data
                return playerData;
            case CharacterType.Enemy:
                var enemyData = new EnemyModel();

                //Apply Enemy simple Data
                return enemyData;

            case CharacterType.Obstacle:
                var obstacleData = new ObstacleModel();

                //Apply obstacle simple Data
                return obstacleData;
        }
        return null;
    }

    public static MVCIController CreateController(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
                var playerController = new PlayerController();
                return playerController;
            case CharacterType.Enemy:
                var enemyController = new EnemyController();
                return enemyController;
            case CharacterType.Obstacle:
                var obstacleController = new ObstacleController();
                return obstacleController;
        }

        return null;
    }

    public static async Task<MVCIView> CreateView(PieceType pieceType, Side side)
    {
        MVCIView view = null;
        string piece = "W";
        if(side == Side.Black)
        {
            piece = "B";
        }

        switch (pieceType)
        {
            case PieceType.King:
                piece += "V";
                break;
            case PieceType.Quen:
                piece += "H";
                break;
            case PieceType.Bishop:
                piece += "T";
                break;
            case PieceType.Knight:
                piece += "M";
                break;  
            case PieceType.Rook:
                piece += "X";
                break;
        }

        var prefabHandle = GameManager.Instance.poolDataSO.GetPoolObject(piece);
        await prefabHandle;

        if (piece.Equals("BV"))
        {
            view = prefabHandle.Result.GetComponent<PlayerView>();
        }
        else if (piece.Contains("W"))
        {
            view = prefabHandle.Result.GetComponent<EnemyView>();
        }
        else
        {
            view = prefabHandle.Result.GetComponent<ObstacleView>();
        }
        return view;
    }
}*/