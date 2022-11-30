using UnityEngine;

namespace Base.DataSystem
{
    [CreateAssetMenu(fileName = "GameData", menuName = "HyperBase/Data/GameData", order = 0)]
    public partial class GameData : DataLoader<GameData>
    {
        public float EnemyWalkSpeed;
    }
}
