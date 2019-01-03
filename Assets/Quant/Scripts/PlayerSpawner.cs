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
        private Actor actor = null;

        void Start()
        {
            var actor = Instantiate(this.actor);
            actor.gameObject.SetLayerRecursive(Layers.Id.Player);
            Broker.Global.Publish(SpawnedPlayerActor.Get(actor));
        }
    }
}
