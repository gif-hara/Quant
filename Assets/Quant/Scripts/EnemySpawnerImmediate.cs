using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 即座に敵アクターを生成するクラス
    /// </summary>
    public sealed class EnemySpawnerImmediate : MonoBehaviour
    {
        [SerializeField]
        private ActorSpawnParameter parameter;

        void Start()
        {
            var t = this.transform;
            this.parameter.Spawn(t.position, t.rotation, Layers.Id.Enemy);
        }
    }
}
