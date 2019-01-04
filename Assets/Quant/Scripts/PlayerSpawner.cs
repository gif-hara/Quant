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

        [SerializeField]
        private ActorStatus status = null;

        [SerializeField]
        private GameObject[] attachObjects = null;

        void Start()
        {
            var actor = Instantiate(this.actor);
            actor.Setup(this.status);
            foreach(var o in this.attachObjects)
            {
                var child = Instantiate(o);
                child.transform.SetParent(actor.transform, false);
            }
            actor.gameObject.SetLayerRecursive(Layers.Id.Player);
            Broker.Global.Publish(SpawnedPlayerActor.Get(actor));
        }
    }
}
