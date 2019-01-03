using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// プレイヤーが制御するアクターが生成された際のイベント
    /// </summary>
    public sealed class SpawnedPlayerActor : Message<SpawnedPlayerActor, Actor>
    {
        /// <summary>
        /// 生成されたアクター
        /// </summary>
        public Actor Actor => this.param1;
    }
}
