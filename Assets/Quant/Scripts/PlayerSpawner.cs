using HK.Framework.EventSystems;
using Quant.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// プレイヤーのアクターを生成するクラス
    /// </summary>
    public sealed class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private ActorSpawnParameter parameter;

        void Start()
        {
            var t = this.transform;
            var actor = this.parameter.Spawn(t.position, t.rotation, Layers.Id.Player);
            Broker.Global.Publish(SpawnedPlayerActor.Get(actor));
        }
    }
}
