using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/AI/Element/ChasePlayer")]
    public sealed class ChasePlayer : AIElement
    {
        public override void Enter(Actor owner, CompositeDisposable disposables)
        {
            Debug.Log(owner);
            var agent = owner.GetComponent<NavMeshAgent>();
            owner.UpdateAsObservable()
                .SubscribeWithState2(this, agent, (_, _this, _agent) =>
                {
                    _agent.SetDestination(GameEnvironment.Instance.Player.CachedTransform.position);
                })
                .AddTo(disposables);
        }

        public override void Exit()
        {
        }
    }
}
