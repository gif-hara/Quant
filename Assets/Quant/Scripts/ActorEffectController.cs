using Quant.Events;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// アクターに関する演出を制御するクラス
    /// </summary>
    public sealed class ActorEffectController
    {
        public static void Apply(Actor actor)
        {
            actor.Broker.Receive<DiedActor>()
                .Take(1)
                .SubscribeWithState(actor, (_, _actor) =>
                {
                    _actor.StatusController.DeadEffect.Spawn(
                        _actor.CachedTransform.position,
                        _actor.CachedTransform.rotation,
                        6.0f
                        );
                })
                .AddTo(actor);
        }
    }
}
