using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// <see cref="Bullet"/>が衝突した際のイベント
    /// </summary>
    /// <remarks>
    /// 現状は衝突した<see cref="Actor"/>に対して通知しています
    /// </remarks>
    public sealed class CollisionedBullet : Message<CollisionedBullet, Bullet>
    {
        /// <summary>
        /// 衝突した<see cref="Bullet"/>
        /// </summary>
        public Bullet Bullet => this.param1;
    }
}
