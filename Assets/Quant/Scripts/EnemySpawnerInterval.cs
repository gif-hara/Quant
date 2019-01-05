using Quant.Events;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// 一定間隔で敵アクターを生成するクラス
    /// </summary>
    public sealed class EnemySpawnerInterval : EnemySpawner
    {
        [SerializeField]
        private ActorSpawnParameter[] actorSpawnParameters = null;

        [SerializeField]
        private float initialInterval = 0.0f;

        [SerializeField]
        private float interval = 1.0f;

        /// <summary>
        /// 生成を止める生成した敵の数
        /// </summary>
        /// <remarks>
        /// <c>1</c>にした場合は1体生成したあと、その敵が倒されるまで生成しません
        /// <c>0</c>の場合は無制限に生成します
        /// </remarks>
        [SerializeField]
        private int limitAliveEnemyNumber = 0;

        /// <summary>
        /// <see cref="actorSpawnParameters"/>をループさせて生成するか
        /// </summary>
        [SerializeField]
        private bool isLoop = false;
        public bool IsLoop => this.isLoop;

        /// <summary>
        /// 生成した敵が生存している数
        /// </summary>
        private int currentAliveEnemyCount = 0;

        private float timer = 0.0f;

        private bool isStart = false;

        private int currentSpawnId = 0;

        /// <summary>
        /// 生成する予定の数
        /// </summary>
        public int TotalSpawnNumber => this.actorSpawnParameters.Length;

        /// <summary>
        /// 生成を開始する
        /// </summary>
        public void StartSpawn()
        {
            if(this.isStart)
            {
                Assert.IsTrue(false, $"{this.name}はすでに生成を開始しています");
                return;
            }

            this.isStart = true;
            this.timer = this.initialInterval;
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.UpdateSpawn());
        }

        private void UpdateSpawn()
        {
            if(!this.CanSpawn())
            {
                return;
            }

            this.timer -= Time.deltaTime;
            if (this.timer <= 0.0f)
            {
                this.timer = this.interval;
                var actor = this.Spawn(this.actorSpawnParameters[this.currentSpawnId], this.transform);
                this.currentSpawnId++;
                this.currentAliveEnemyCount++;
                actor.Broker.Receive<DiedActor>()
                    .SubscribeWithState(this, (_, _this) => _this.currentAliveEnemyCount--)
                    .AddTo(this);

                if(this.isLoop && this.currentSpawnId >= this.actorSpawnParameters.Length)
                {
                    this.currentSpawnId = 0;
                }
            }
        }

        private bool CanSpawn()
        {
            if (this.currentSpawnId >= this.actorSpawnParameters.Length)
            {
                return false;
            }

            if (this.limitAliveEnemyNumber <= 0)
            {
                return true;
            }

            return this.currentAliveEnemyCount < this.limitAliveEnemyNumber;
        }
    }
}
