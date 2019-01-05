using HK.Framework.EventSystems;
using Quant.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// カメラが追従するオブジェクトをプレイヤーに戻すマップイベント
    /// </summary>
    public sealed class SetCameraPropertyRevertLookAt : MapEvent
    {
        public override void Invoke()
        {
            var property = new OverrideCameraProperty();
            property.LookAt.CanApply = true;
            property.LookAt.Value = GameEnvironment.Instance.Player.transform;
            Broker.Global.Publish(RequestOverrideCameraProperty.Get(property));
        }
    }
}
