using System.Collections.Generic;
using UnityEngine;

namespace Base.DataSystem
{
    [System.Serializable]
    public partial class Data
    {
        public int coin;
        public int level;
        [HideInInspector]
        public int showingLevel;
        public List<ExtraData> ExtraDatas;

        public Data(int _coin, int _level, int _showingLevel)
        {
            coin = _coin;
            level = _level;
            showingLevel = _showingLevel;
        }

        public void Res()
        {
            coin = 0;
            level = 0;
            showingLevel = 1;
            ExtraDatas.Clear();
        }
    }
}