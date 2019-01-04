using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// アクターが死亡した際に通知されるイベント
    /// </summary>
    /// <remarks>
    /// 死亡したアクターに対して通知しています
    /// </remarks>
    public sealed class DiedActor : Message<DiedActor>
    {
    }
}
