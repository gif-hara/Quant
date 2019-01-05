using HK.Framework.EventSystems;
using Quant.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.Extensions;

namespace Quant
{
    /// <summary>
    /// プレイヤーの入力を制御するクラス
    /// </summary>
    public sealed class PlayerInputController : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<SpawnedPlayerActor>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.SetupMove(x.Actor);
                    _this.SetupRotation(x.Actor);
                })
                .AddTo(this);
        }

        private void SetupMove(Actor actor)
        {
            this.UpdateAsObservable()
                .SubscribeWithState2(this, actor, (_, _this, a) =>
                {
                    var h = Input.GetAxis("MoveX");
                    var v = Input.GetAxis("MoveY");
                    var cameraTransform = Cameraman.Instance.Camera.transform;
                    var cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1.0f, 0.0f, 1.0f));
                    var velocity = ((cameraForward * v) + (cameraTransform.right * h)).normalized;
                    a.TransformController.Move(velocity * a.StatusController.MoveSpeed);
                    a.AnimationController.SetMove(velocity);
                });
        }

        private void SetupRotation(Actor actor)
        {
            var muzzles = actor.GetComponentsInChildren<Muzzle>();
            this.UpdateAsObservable()
                .SubscribeWithState3(this, actor, muzzles, (_, _this, a, _muzzles) =>
                {
                    var h = Input.GetAxis("RotateX");
                    var v = Input.GetAxis("RotateY");
                    if(HK.Framework.Extensions.Extensions.IsEqual(h + v, 0.0f))
                    {
                        return;
                    }

                    var cameraTransform = Cameraman.Instance.Camera.transform;
                    var cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1.0f, 0.0f, 1.0f));
                    var direction = ((cameraForward * v) + (cameraTransform.right * h)).normalized;
                    a.TransformController.RotateImmediate(Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z), Vector3.up));
                    foreach (var m in _muzzles)
                    {
                        m.Fire();
                    }
                });
        }
    }
}
