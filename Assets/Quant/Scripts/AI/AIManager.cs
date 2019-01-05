using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Quant.AIControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AIManager : MonoBehaviour
    {
        [SerializeField]
        private Bundle[] bundles;

        private CompositeDisposable disposables = new CompositeDisposable();

        private int currentBundleId = 0;

        private Actor owner;

        private void Start()
        {
            this.owner = this.GetComponentInParent<Actor>();
            this.bundles[this.currentBundleId].Element.Enter(this.owner, this.disposables);

            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.ChangeAI());
        }

        private void ChangeAI()
        {
            var bundle = this.bundles[this.currentBundleId];
            foreach (var c in bundle.ConditionBundles)
            {
                if (c.Condition.Evalute(this.owner))
                {
                    bundle.Element.Exit();
                    this.disposables.Clear();
                    this.currentBundleId = c.NextBundleId;
                    var nextBundle = this.bundles[this.currentBundleId];
                    nextBundle.Element.Enter(this.owner, this.disposables);
                    break;
                }
            }
        }

        [Serializable]
        public class Bundle
        {
            [SerializeField]
            private AIElement element;
            public AIElement Element => this.element;

            [SerializeField]
            private ConditionBundle[] conditionBundles = null;
            public ConditionBundle[] ConditionBundles => this.conditionBundles;
        }

        [Serializable]
        public class ConditionBundle
        {
            [SerializeField]
            private AICondition condition = null;
            public AICondition Condition => this.condition;

            [SerializeField]
            private int nextBundleId = 0;
            public int NextBundleId => this.nextBundleId;
        }
    }
}
