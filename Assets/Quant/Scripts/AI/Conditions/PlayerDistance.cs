using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Quant/AI/Conditions/PlayerDistance")]
    public sealed class PlayerDistance : AICondition
    {
        /// <summary>
        /// 評価タイプ
        /// </summary>
        public enum ConditionType
        {
            Greater,
            Less,
        }

        [SerializeField]
        private ConditionType type = ConditionType.Greater;

        [SerializeField]
        private float distance = 1.0f;

        public override bool Evalute(Actor owner)
        {
            var player = GameEnvironment.Instance.Player;
            var sqrMagnitude = (player.CachedTransform.position - owner.CachedTransform.position).sqrMagnitude;
            var distance = this.distance * this.distance;
            if (this.type == ConditionType.Greater)
            {
                return sqrMagnitude > distance;
            }
            if (this.type == ConditionType.Less)
            {
                return sqrMagnitude < distance;
            }

            Assert.IsTrue(false, $"type = {this.type}は未対応の値です");
            return false;
        }
    }
}
