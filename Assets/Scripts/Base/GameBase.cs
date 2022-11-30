using System;
using System.Threading.Tasks;
using Base.AudioSystem;
using Base.CameraSystem;
using Base.DataSystem;
using Base.MenuSystem;
using Base.PoolSystem;
using DBase.LevelSystem;
using IngameDebugConsole;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DBase
{
    public class GameBase : Singleton<GameBase>
    {
        [Header("Base Managers")] public DataManager DataManager;
        public LevelManager LevelManager;
        public MenuManager MenuManager;
        public AudioManager AudioManager;
        public CameraManager CameraManager;
        public PoolManager PoolManager;

        [Header("Game Stats")] public GameStat GameStat;
        public bool ShowFps;
        public bool ShowConsole;
        [Header("Objects")] public GameObject RunTimeConsole;

        private async void Awake()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
  Debug.unityLogger.logEnabled = false;
#endif

            var loadingMenu = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            while (!loadingMenu.isDone)
            {
                await Task.Yield();
            }
        
            GameStat = GameStat.Start;
            RunTimeConsole = DebugLogManager.Instance.gameObject;
            RunTimeConsole.SetActive(ShowConsole);

            DataManager = GetComponentInChildren<DataManager>();
            LevelManager = GetComponentInChildren<LevelManager>();
            AudioManager = GetComponentInChildren<AudioManager>();
            CameraManager = GetComponentInChildren<CameraManager>();
            MenuManager = GetComponentInChildren<MenuManager>();
            PoolManager = GetComponentInChildren<PoolManager>();


            await DataManager.Setup();
            await PoolManager.Setup();
            await MenuManager.Setup();
            await CameraManager.Setup();
            await AudioManager.Setup();
            await LevelManager.Setup();

            Debug.Log("GameBase Setup Complete");
        }

        private void OnEnable()
        {
            EventManager.FirstTouch += StartGame;
        }

        private void OnDisable()
        {
            EventManager.FirstTouch -= StartGame;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                }
                else Time.timeScale = 1;
            }

            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        float deltaTime = 0.0f;

        void OnGUI()
        {
            if (ShowFps)
                ShowFPS();
        }

        private void ShowFPS()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
#endif

        public void ChangeStat(GameStat stat)
        {
            if (Equals(GameStat, stat))
                return;

            GameStat = stat;
        }

        private void StartGame()
        {
            ChangeStat(GameStat.Start);
        }
        
    }

    public static class Base
    {
        public static GameStat GetStat()
        {
            return GameBase.Instance.GameStat;
        }
    
        public static void ChangeStat(GameStat stat)
        {
            GameBase.Instance.ChangeStat(stat);
        }

        public static Transform GetLevelHolder()
        {
            return GameBase.Instance.LevelManager.LevelHolder;
        }

        public static bool IsPlaying()
        {
            return GameBase.Instance.GameStat == GameStat.Playing;
        }
        
        public static async void FinisGame(GameStat gameStat, float time = 0f)
        {
            if (GameBase.Instance.GameStat == GameStat.Playing)
                GameBase.Instance.ChangeStat(gameStat);

            EventManager.BeforeFinishGame?.Invoke(gameStat);
            await Task.Delay((int)time * 1000);
            if (!Application.isPlaying) return;
            EventManager.FinishGame?.Invoke(gameStat);
        }

        public static void StartGameAddFunc(Action func)
        {
            EventManager.FirstTouch += func;
        }

        public static void NextLevelAddFunc(Action func)
        {
            EventManager.NextLevel += func;
        }

        public static void RestartLevelAddFunc(Action func)
        {
            EventManager.RestartLevel += func;
        }

        public static void FinishGameAddFunc(Action<GameStat> func)
        {
            EventManager.FinishGame += func;
        }
    }
}