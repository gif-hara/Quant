using UnityEngine;
using UnityEngine.Assertions;

namespace Quant
{
    /// <summary>
    /// <see cref="ActorStatus"/>を制御するクラス
    /// </summary>
    public sealed class ActorStatusController
    {
        private ActorStatus baseStatus;

        public ActorStatusController(ActorStatus status)
        {
            this.baseStatus = status;
        }

        public float MoveSpeed => this.baseStatus.MoveSpeed;
    }
}
