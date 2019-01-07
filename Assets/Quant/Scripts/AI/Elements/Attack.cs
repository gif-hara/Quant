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
        private float chargeTime = 1.0f;

        [SerializeField]
        private float coolTime = 1.0f;

        [SerializeField]
        private GameObject attackObject;

        [SerializeField]
        private SmoothDamp.Vector3 rotationSmoothDamp;

        [SerializeField]
        private int animationId = 0;

        /// <summary>
        /// AIを切り替えるまでに発射する最低回数
        /// </summary>
        [SerializeField]
        private int canExitFireCount = 1;

        public override AIElement Clone
        {
            get
            {
                var clone = CreateInstance<Attack>();
                clone.chargeTime = this.chargeTime;
                clone.coolTime = this.coolTime;
                clone.attackObject = this.attackObject;
                clone.rotationSmoothDamp = this.rotationSmoothDamp;
                clone.animationId = this.animationId;
                clone.canExitFireCount = this.canExitFireCount;

                return clone;
            }
        }

        public override void Enter(Actor owner, CompositeDisposable disposables)
        {
            owner.AnimationController.SetMove(Vector3.zero);
            this.StartAttack(owner, disposables);
            owner.UpdateAsObservable()
                .SubscribeWithState2(owner, this.rotationSmoothDamp, (_, _owner, r) =>
                {
                    var player = GameEnvironment.Instance.Player.CachedTransform;
                    r.Target = Quaternion.LookRotation(player.position - _owner.CachedTransform.position, Vector3.up).eulerAngles;
                    _owner.TransformController.RotateImmediate(Quaternion.Euler(r.SmoothDampAngle(_owner.CachedTransform.rotation.eulerAngles)));
                })
                .AddTo(owner)
                .AddTo(disposables);
        }

        public override bool CanExit => this.canExitFireCount <= 0;

        private void StartAttack(Actor owner, CompositeDisposable disposables)
        {
            owner.AnimationController.StartAttack(this.animationId);
            Observable.Timer(TimeSpan.FromSeconds(this.chargeTime))
                .SubscribeWithState3(this, owner, disposables, (_, _this, _owner, _disposables) =>
                {
                    Instantiate(_this.attackObject, _owner.CachedTransform);
                    _this.CoolDown(_owner, _disposables);
                })
                .AddTo(owner)
                .AddTo(disposables);
        }

        private void CoolDown(Actor owner, CompositeDisposable disposables)
        {
            Observable.Timer(TimeSpan.FromSeconds(this.coolTime))
                .SubscribeWithState3(this, owner, disposables, (_, _this, _owner, _disposables) =>
                {
                    _this.canExitFireCount--;
                    _this.StartAttack(_owner, _disposables);
                })
                .AddTo(owner)
                .AddTo(disposables);
        }
    }
}
