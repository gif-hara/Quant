using HK.Framework.EventSystems;
using Quant.Events;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// カメラのプロパティを設定するマップイベント
    /// </summary>
    public sealed class SetCameraProperty : MapEvent
    {
        [SerializeField]
        private OverrideCameraProperty property;

        public override void Invoke()
        {
            Broker.Global.Publish(RequestOverrideCameraProperty.Get(this.property));
        }
    }
}
