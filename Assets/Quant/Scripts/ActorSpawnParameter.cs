using HK.Framework.EventSystems;
using Quant.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターを生成するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/ActorSpawnParameter")]
    public sealed class ActorSpawnParameter: ScriptableObject
    {
        [SerializeField]
        private Actor actor = null;

        [SerializeField]
        private ActorStatus actorStatus = null;

        /// <summary>
        /// 生成したアクターにアタッチするオブジェクトリスト
        /// </summary>
        [SerializeField]
        private GameObject[] attachObjects = null;

        public Actor Spawn(Vector3 position, Quaternion rotation, Layers.Id layerId)
        {
            var actor = Instantiate(this.actor, position, rotation);
            actor.Setup(this.actorStatus);
            foreach (var o in this.attachObjects)
            {
                var child = Instantiate(o, actor.CachedTransform);
            }
            actor.gameObject.SetLayerRecursive(layerId);

            return actor;
        }
    }
}
