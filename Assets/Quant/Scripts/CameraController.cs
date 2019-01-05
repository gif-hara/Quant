using HK.Framework.EventSystems;
using Quant.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// ゲーム中のカメラを制御するクラス
    /// </summary>
    public sealed class CameraController : MonoBehaviour
    {
        private Vector3 currentPosition = Vector3.zero;

        [SerializeField]
        private float positionSmoothDamp = 1.0f;

        private Transform lookAtTarget;

        private void Awake()
        {
            Broker.Global.Receive<SpawnedPlayerActor>()
                .SubscribeWithState(this, (x, _this) => _this.Setup(x.Actor))
                .AddTo(this);

            Broker.Global.Receive<RequestOverrideCameraProperty>()
                .SubscribeWithState(this, (x, _this) => _this.ApplyOverrideProperty(x.Property))
                .AddTo(this);
        }

        private void Setup(Actor actor)
        {
            this.lookAtTarget = actor.transform;
            this.FixedUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if(_this.lookAtTarget == null)
                    {
                        return;
                    }
                    var cameraman = GameEnvironment.Instance.Cameraman;
                    cameraman.Position = Vector3.SmoothDamp(cameraman.Position, _this.lookAtTarget.position, ref _this.currentPosition, _this.positionSmoothDamp);
                });
        }

        private void ApplyOverrideProperty(OverrideCameraProperty property)
        {
            var cameraman = GameEnvironment.Instance.Cameraman;
            if(property.LookAt.CanApply)
            {
                this.lookAtTarget = property.LookAt.Value;
            }
            if(property.Pivot.CanApply)
            {
                cameraman.Pivot = Quaternion.Euler(property.Pivot.Value);
            }
            if(property.Distance.CanApply)
            {
                cameraman.Distance = property.Distance.Value;
            }
            if(property.Rig.CanApply)
            {
                cameraman.Rig = Quaternion.Euler(property.Rig.Value);
            }
        }
    }
}
