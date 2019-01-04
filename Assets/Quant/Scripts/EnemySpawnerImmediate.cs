using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 即座に敵アクターを生成するクラス
    /// </summary>
    public sealed class EnemySpawnerImmediate : EnemySpawner
    {
        [SerializeField]
        private ActorSpawnParameter parameter;

        void Start()
        {
            this.Spawn(this.parameter, this.transform);
        }
    }
}
