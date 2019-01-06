using System;
using UniRx;
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

        public override void Enter(Actor owner, CompositeDisposable disposables)
        {
            this.StartAttack(owner, disposables);
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
