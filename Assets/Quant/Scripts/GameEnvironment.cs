using HK.Framework.EventSystems;
using Quant.Events;
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

        public Cameraman Cameraman { get; set; }

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            Broker.Global.Receive<SpawnedPlayerActor>()
                .SubscribeWithState(this, (x, _this) => _this.Player = x.Actor)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }
    }
}
