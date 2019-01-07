using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// プレイヤーを追いかけるAI
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/AI/Element/ChasePlayer")]
    public sealed class ChasePlayer : AIElement
    {
        public override AIElement Clone => CreateInstance<ChasePlayer>();

        public override void Enter(Actor owner, CompositeDisposable disposables)
        {
            var agent = owner.GetComponent<NavMeshAgent>();
            agent.updatePosition = true;
            agent.updateRotation = true;
            agent.isStopped = false;
            owner.AnimationController.StartIdle();
            owner.UpdateAsObservable()
                .SubscribeWithState3(this, owner, agent, (_, _this, _owner, _agent) =>
                {
                    _agent.destination = (GameEnvironment.Instance.Player.CachedTransform.position);
                    var velocity = (_agent.nextPosition - _owner.CachedTransform.position);
                    //if(velocity.magnitude >= 1.0f)
                    {
                        velocity = velocity.normalized;
                        //_owner.CachedTransform.position = _agent.nextPosition;
                        //_owner.TransformController.Move(velocity * _owner.StatusController.MoveSpeed);
                        //_owner.TransformController.RotateImmediate(Quaternion.LookRotation(Vector3.Scale(velocity, new Vector3(1, 0, 1)), Vector3.up));
                        _owner.AnimationController.SetMove(velocity);
                    }
                })
                .AddTo(disposables);
        }
    }
}
