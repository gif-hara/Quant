using HK.Framework.EventSystems;
using Quant.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 敵を生成する抽象クラス
    /// </summary>
    public abstract class EnemySpawner : MonoBehaviour
    {
        protected void Spawn(ActorSpawnParameter parameter, Vector3 position, Quaternion rotation)
        {
            var actor = parameter.Spawn(position, rotation, Layers.Id.Enemy);
            Broker.Global.Publish(SpawnedEnemyActor.Get(actor));
        }

        protected void Spawn(ActorSpawnParameter parameter, Transform transform)
        {
            this.Spawn(parameter, transform.position, transform.rotation);
        }
    }
}
