using HK.Framework.EventSystems;
using Quant.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// ゲームクリアを実行するマップイベント
    /// </summary>
    public sealed class InvokeGameClear : MapEvent
    {
        public override void Invoke()
        {
            Broker.Global.Publish(GameClear.Get());
        }
    }
}
