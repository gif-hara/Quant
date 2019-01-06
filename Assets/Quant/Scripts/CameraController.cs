using HK.Framework.EventSystems;
using Quant.Events;
using System;
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
        [SerializeField]
        private SmoothDamp.Vector3 positionSmoothDamp = null;

        [SerializeField]
        private SmoothDamp.Vector3 pivotSmoothDamp = null;

        [SerializeField]
        private SmoothDamp.Float distanceSmoothDamp = null;

        [SerializeField]
        private SmoothDamp.Vector3 rigSmoothDamp = null;

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
                    _this.UpdatePosition();
                    _this.UpdatePivot();
                    _this.UpdateDistance();
                    _this.UpdateRig();
                });
        }

        private void UpdatePosition()
        {
            if (this.lookAtTarget == null)
            {
                return;
            }
            var cameraman = GameEnvironment.Instance.Cameraman;
            this.positionSmoothDamp.Target = this.lookAtTarget.position;
            cameraman.Position = this.positionSmoothDamp.SmoothDamp(cameraman.Position);
        }

        private void UpdatePivot()
        {
            var cameraman = GameEnvironment.Instance.Cameraman;
            cameraman.Pivot = Quaternion.Euler(this.pivotSmoothDamp.SmoothDamp(cameraman.Pivot.eulerAngles));
        }

        private void UpdateDistance()
        {
            var cameraman = GameEnvironment.Instance.Cameraman;
            cameraman.Distance = this.distanceSmoothDamp.SmoothDamp(cameraman.Distance);
        }

        private void UpdateRig()
        {
            var cameraman = GameEnvironment.Instance.Cameraman;
            cameraman.Rig = Quaternion.Euler(this.rigSmoothDamp.SmoothDamp(cameraman.Rig.eulerAngles));
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
                this.pivotSmoothDamp.Target = property.Pivot.Value;
            }
            if(property.Distance.CanApply)
            {
                this.distanceSmoothDamp.Target = property.Distance.Value;
            }
            if(property.Rig.CanApply)
            {
                this.rigSmoothDamp.Target = property.Rig.Value;
            }
        }
    }
}
