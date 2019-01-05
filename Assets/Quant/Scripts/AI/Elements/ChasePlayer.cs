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
            var agent = owner.GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            owner.UpdateAsObservable()
                .SubscribeWithState3(this, owner, agent, (_, _this, _owner, _agent) =>
                {
                    _agent.SetDestination(GameEnvironment.Instance.Player.CachedTransform.position);
                    var velocity = (_agent.nextPosition - _owner.CachedTransform.position).normalized;
                    owner.TransformController.Move(velocity * _owner.StatusController.MoveSpeed);
                    owner.AnimationController.SetMove(velocity);
                    owner.TransformController.RotateImmediate(Quaternion.LookRotation(Vector3.Scale(velocity, new Vector3(1, 0, 1)), Vector3.up));
                })
                .AddTo(disposables);
        }
    }
}
