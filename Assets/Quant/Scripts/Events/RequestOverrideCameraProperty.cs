using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Events
{
    /// <summary>
    /// カメラのプロパティの上書きをリクエストするイベント
    /// </summary>
    public sealed class RequestOverrideCameraProperty : Message<RequestOverrideCameraProperty, OverrideCameraProperty>
    {
        public OverrideCameraProperty Property => this.param1;
    }
}
