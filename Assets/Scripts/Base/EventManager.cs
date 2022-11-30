using System;
using Base.DataSystem;

namespace DBase
{
    public static class EventManager
    {
        public static Action<GameStat> BeforeFinishGame;
        public static Action<GameStat> FinishGame;

        public static Action NextLevel;
        public static Action RestartLevel;

        public static Action FirstTouch;
        public static Action<bool> OnPause;

        public static Action OnBeforeLoadedLevel;
        public static Action OnAfterLoadedLevel;
    }
}