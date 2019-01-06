using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.Task
{
    /// <summary>
    /// 全ての敵を強制的に死亡させるタスク
    /// </summary>
    public sealed class AllDeadEnemy : TaskMonoBehavior
    {
        public override void Invoke()
        {
            var enemies = GameEnvironment.Instance.Enemies.ToArray();
            foreach(var e in enemies)
            {
                e.ForceDead();
            }
        }
    }
}
