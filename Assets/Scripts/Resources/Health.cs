using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;
using RPG.Saving;


namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable//, IJsonSaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] UnityEvent onDie;
        [SerializeField] TakeDamageEvent takeDamage;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }
        float healthPoints = -1f;

        bool isDead = false;

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }  
            
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
             GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);


            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if (healthPoints == 0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();//cancel every other action after death
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
           float regenHealthPoints  = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
           healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float) state;

            if (healthPoints == 0)
            {
                Die();
            }
            
        }

        // public JToken CaptureAsJToken()
        // {
        //     return JToken.FromObject(healthPoints);
        // }

        // public void RestoreFromJToken(JToken state)
        // {
        //     healthPoints = state.ToObject<float>();
        //     return healthPoints;

        // }
    }
}
