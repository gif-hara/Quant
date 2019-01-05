using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.MapControllers
{
    /// <summary>
    /// 全ての敵を強制的に死亡させるマップイベント
    /// </summary>
    public sealed class AllDeadEnemy : MapEvent
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
