using HK.Framework.EventSystems;
using Quant.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Task
{
    /// <summary>
    /// ゲームクリアを実行するタスク
    /// </summary>
    public sealed class InvokeGameClear : TaskMonoBehavior
    {
        public override void Invoke()
        {
            Broker.Global.Publish(GameClear.Get());
        }
    }
}
