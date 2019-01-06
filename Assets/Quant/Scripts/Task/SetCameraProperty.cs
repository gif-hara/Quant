using HK.Framework.EventSystems;
using Quant.Events;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Task
{
    /// <summary>
    /// カメラのプロパティを設定するタスク
    /// </summary>
    public sealed class SetCameraProperty : TaskMonoBehavior
    {
        [SerializeField]
        private OverrideCameraProperty property = null;

        public override void Invoke()
        {
            Broker.Global.Publish(RequestOverrideCameraProperty.Get(this.property));
        }
    }
}
