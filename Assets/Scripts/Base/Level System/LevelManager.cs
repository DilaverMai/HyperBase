using System;
using System.Threading.Tasks;
using Base.DataSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DBase.LevelSystem
{
    public class LevelManager : Singleton<LevelManager>
    {
        public GameObject[] Levels;
        [HideInInspector]
        public Transform LevelHolder;
        public Task Setup()
        {
            if (Levels != null)
            {
                Array.Clear(Levels, 0, Levels.Length);
            }

            Levels = Resources.LoadAll<GameObject>("Levels");
        
            if (Levels.Length == 0)
            {
                Debug.LogError("No Levels Found");
                return Task.CompletedTask;
            }
        
            LoadLevelFunc();
        
            return Task.CompletedTask;
        }

        private void LoadLevelFunc()
        {
            Debug.Log("Loading Level");

            EventManager.OnBeforeLoadedLevel?.Invoke();
            var currentLevel = GameBase.Instance.DataManager.PlayerData.level;

            if (Levels.Length == 0)
                Debug.LogError("No Levels Found");

            if (Levels.Length <= currentLevel)
                currentLevel = 0;

            var level = Levels[currentLevel];
            var levelObject = Instantiate(level, Vector3.zero, Quaternion.identity);
            LevelHolder = levelObject.transform;
            levelObject.name = "Level " + currentLevel;
            //levelObject.transform.SetParent(transform);
            //Elephant.LevelStarted(DataManager.Instance.PlayerData.showingLevel);
            EventManager.OnAfterLoadedLevel?.Invoke();
        }

        private void NextLevelFunc()
        {
            Debug.Log("Next Level");
            DataManager.Instance.PlayerData.level++;
            var currentLevel = DataManager.Instance.PlayerData.level;
            DataManager.Instance.PlayerData.showingLevel++;
        
            EventManager.OnBeforeLoadedLevel?.Invoke();

            var nextLevel = currentLevel;

            if (nextLevel >= Levels.Length)
            {
                nextLevel = RandomSelectedLevel(Levels.Length);
                DataManager.Instance.PlayerData.level = nextLevel;
            }

            if (LevelHolder != null)
                Destroy(LevelHolder.gameObject);

            var lvl = Instantiate(Levels[nextLevel]);
            //lvl.transform.SetParent(transform);
            LevelHolder = lvl.transform;

            EventManager.OnAfterLoadedLevel?.Invoke();
            DataManager.Instance.SaveGame();
        }

        private void RestartLevelFunc()
        {
            Debug.Log("Restart Level");
            DataManager.ReLoadData?.Invoke();

            EventManager.OnBeforeLoadedLevel?.Invoke();

            var currentLevel = GameBase.Instance.DataManager.PlayerData.level;

            if (currentLevel >= Levels.Length)
                currentLevel = 0;


            if (LevelHolder != null)
                Destroy(LevelHolder.gameObject);

            var lvl = Instantiate(Levels[currentLevel]);
            //lvl.transform.SetParent(transform);
            LevelHolder = lvl.transform;
            EventManager.OnAfterLoadedLevel?.Invoke();
        }

        private void OnEnable()
        {
            EventManager.NextLevel += NextLevelFunc;
            EventManager.RestartLevel += RestartLevelFunc;
        }

        private void OnDisable()
        {
            EventManager.NextLevel -= NextLevelFunc;
            EventManager.RestartLevel -= RestartLevelFunc;
        }

        private int RandomSelectedLevel(int currentLevel)
        {
            if (Levels.Length == 1) return 0;
            var newLevel = Random.Range(0, Levels.Length);
            if (newLevel == currentLevel)
            {
                return RandomSelectedLevel(currentLevel);
            }
            return newLevel;
        }

#if UNITY_EDITOR
        [Button]
        private void OverrideLoadLevels()
        {
            Levels = Resources.LoadAll<GameObject>("Levels");
        }
#endif
    }
}
