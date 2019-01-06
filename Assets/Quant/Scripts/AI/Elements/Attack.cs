using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// 攻撃を行うAI
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/AI/Element/Attack")]
    public sealed class Attack : AIElement
    {
        [SerializeField]
        private float waitAttack = 1.0f;

        [SerializeField]
        private GameObject attackObject;

        [SerializeField]
        private SmoothDamp.Vector3 rotationSmoothDamp;

        public override void Enter(Actor owner, CompositeDisposable disposables)
        {
            owner.AnimationController.SetMove(Vector3.zero);
            this.StartAttack(owner, disposables);
            owner.UpdateAsObservable()
                .SubscribeWithState2(owner, new SmoothDamp.Vector3(this.rotationSmoothDamp), (_, _owner, r) =>
                {
                    var player = GameEnvironment.Instance.Player.CachedTransform;
                    r.Target = Quaternion.LookRotation(player.position - _owner.CachedTransform.position, Vector3.up).eulerAngles;
                    _owner.TransformController.RotateImmediate(Quaternion.Euler(r.SmoothDampAngle(_owner.CachedTransform.rotation.eulerAngles)));
                })
                .AddTo(owner)
                .AddTo(disposables);
        }

        private void StartAttack(Actor owner, CompositeDisposable disposables)
        {
            Observable.Timer(TimeSpan.FromSeconds(this.waitAttack))
                .SubscribeWithState3(this, owner, disposables, (_, _this, _owner, _disposables) =>
                {
                    Instantiate(_this.attackObject, _owner.CachedTransform);
                    _this.StartAttack(_owner, _disposables);
                })
                .AddTo(owner)
                .AddTo(disposables);
        }
    }
}
