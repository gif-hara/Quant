using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// ゲームクリアを通知するイベント
    /// </summary>
    public sealed class GameClear : Message<GameClear>
    {
    }
}
