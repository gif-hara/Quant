using Quant.Events;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// <see cref="ActorStatus"/>を制御するクラス
    /// </summary>
    public sealed class ActorStatusController
    {
        private Actor actor = null;

        /// <summary>
        /// 元となるステータス
        /// </summary>
        private ActorStatus baseStatus;

        /// <summary>
        /// 動的に変更されるステータス
        /// </summary>
        private ActorStatus dynamic;

        public ActorStatusController(Actor actor, ActorStatus status)
        {
            this.actor = actor;
            this.baseStatus = status.Clone();
            this.dynamic = status.Clone();
            actor.Broker.Receive<CollisionedBullet>()
                .SubscribeWithState2(this, actor, (x, _this, _actor) =>
                {
                    _this.TakeDamage(x.Bullet.Status.Power);
                })
                .AddTo(actor);
        }

        public void TakeDamage(int damage)
        {
            var oldIsDead = this.IsDead;
            this.dynamic.HitPoint -= damage;

            // 初めて死亡した際にイベントを通知する
            var newIsDead = this.IsDead;
            if(oldIsDead != newIsDead)
            {
                Object.Destroy(this.actor.gameObject);
                this.actor.Broker.Publish(DiedActor.Get());
            }
        }

        public float MoveSpeed => this.baseStatus.MoveSpeed;

        public bool IsDead => this.dynamic.HitPoint <= 0;
    }
}
