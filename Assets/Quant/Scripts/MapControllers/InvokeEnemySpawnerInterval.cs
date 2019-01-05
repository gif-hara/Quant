using HK.Framework.EventSystems;
using Quant.Events;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// <see cref="EnemySpawnerInterval"/>の生成を開始するマップイベント
    /// </summary>
    public sealed class InvokeEnemySpawnerInterval : MapEvent
    {
        [SerializeField]
        private EnemySpawnerInterval[] targets = null;

        /// <summary>
        /// 生成した敵がすべて倒された際のイベント
        /// </summary>
        [SerializeField]
        private MapEvent[] allDeadEnemyEvents = null;

        /// <summary>
        /// 残っている敵の数
        /// </summary>
        private int remainEnemyCount = 0;

        public override void Invoke()
        {
            var totalSpawnNumber = 0;
            foreach(var e in this.targets)
            {
                e.StartSpawn();
                if(!e.IsLoop)
                {
                    totalSpawnNumber += e.TotalSpawnNumber;
                }
            }

            this.remainEnemyCount = totalSpawnNumber;
            Broker.Global.Receive<SpawnedEnemyActor>()
                .Where(x => this.IsTargetOwner(x.Owner))
                .Take(totalSpawnNumber)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.ObserveDiedActor(x.Actor);
                })
                .AddTo(this);
        }

        private bool IsTargetOwner(GameObject actorSpawnOwner)
        {
            foreach(var e in this.targets)
            {
                if(e.gameObject == actorSpawnOwner)
                {
                    return true;
                }
            }

            return false;
        }

        private void ObserveDiedActor(Actor actor)
        {
            actor.Broker.Receive<DiedActor>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.remainEnemyCount--;
                    if(_this.remainEnemyCount <= 0)
                    {
                        foreach(var e in this.allDeadEnemyEvents)
                        {
                            e.Invoke();
                        }
                    }
                })
                .AddTo(this);
        }
    }
}
