using HK.Framework.EventSystems;
using Quant.Events;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.UI
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameClearUIController : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.SetActive(false);
            Broker.Global.Receive<GameClear>()
                .SubscribeWithState(this, (_, _this) => _this.gameObject.SetActive(true))
                .AddTo(this);
        }
    }
}
