using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class Reputation : MonoBehaviour
    {
        public float maxReputation = 100f;
        public Slider reputationSlider;

        private void Start()
        {
            reputationSlider.maxValue = maxReputation;
            reputationSlider.value = maxReputation;
        }

        public void AddReputation(float n)
        {
            maxReputation += n;
            if (maxReputation <= 0)
            {
                maxReputation = 0;
                Managers.GameManager.Instance.Defeat();
            }
            if(maxReputation > reputationSlider.maxValue) { reputationSlider.value = reputationSlider.maxValue; }
            else reputationSlider.value = maxReputation;
        }

    }
}
