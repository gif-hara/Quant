using HK.Framework.EventSystems;
using Quant.Events;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// ゲームの状態を保持するクラス
    /// </summary>
    public sealed class GameEnvironment : MonoBehaviour
    {
        public static GameEnvironment Instance { get; private set; }

        public Actor Player { get; private set; }

        public List<Actor> Enemies { get; private set; } = new List<Actor>();

        public Cameraman Cameraman { get; set; }

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            Broker.Global.Receive<SpawnedPlayerActor>()
                .SubscribeWithState(this, (x, _this) => _this.Player = x.Actor)
                .AddTo(this);

            Broker.Global.Receive<SpawnedEnemyActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Enemies.Add(x.Actor);
                    x.Actor.Broker.Receive<DiedActor>()
                        .Take(1)
                        .SubscribeWithState2(_this, x.Actor, (_, __this, _actor) => __this.Enemies.Remove(_actor))
                        .AddTo(_this);
                })
                .AddTo(this);
        }

        private void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }
    }
}
