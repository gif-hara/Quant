using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// アクターが死亡した際に通知されるイベント
    /// </summary>
    public sealed class DiedActor : Message<DiedActor, Actor>
    {
        /// <summary>
        /// 死亡したアクター
        /// </summary>
        public Actor Actor => this.param1;
    }
}
