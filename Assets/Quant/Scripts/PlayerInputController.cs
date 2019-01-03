using HK.Framework.EventSystems;
using Quant.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// プレイヤーの入力を制御するクラス
    /// </summary>
    public sealed class PlayerInputController : MonoBehaviour
    {
        [SerializeField]
        private float speed;

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
                    a.TransformController.Move(new Vector3(h, 0.0f, v) * Time.deltaTime * _this.speed);
                });
        }

        private void SetupRotation(Actor actor)
        {
            this.UpdateAsObservable()
                .SubscribeWithState2(this, actor, (_, _this, a) =>
                {
                    var h = Input.GetAxis("RotateX");
                    var v = Input.GetAxis("RotateY");
                    if(h + v == 0.0f)
                    {
                        return;
                    }
                    a.TransformController.Rotate(Quaternion.LookRotation(new Vector3(h, 0.0f, v), Vector3.up));
                });
        }
    }
}
