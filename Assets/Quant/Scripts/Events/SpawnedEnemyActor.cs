using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// 敵アクターが生成された際のイベント
    /// </summary>
    public sealed class SpawnedEnemyActor : Message<SpawnedEnemyActor, Actor>
    {
        /// <summary>
        /// 生成されたアクター
        /// </summary>
        public Actor Actor => this.param1;
    }
}
