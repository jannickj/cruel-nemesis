using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib.Module;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic.Events.UnitEvents;
using Cruel.GameLogic.Unit;

namespace Cruel.GameLogic.Modules
{
	public class HealthModule : UniversalModule<UnitEntity>
	{
        private int originalHP = 1;
        private int maxHealth = 1;
        private int health = 1;

        public HealthModule(int maxhealth)
        {
            if (maxhealth > 1)
            {
                this.health = maxhealth;
                this.maxHealth = maxhealth;
            }
        }

        public void SetStartingHealth(int maxhp)
        {
            if (maxhp < 1)
            {
                maxhp = 1;
            }
            this.originalHP = maxhp;
            this.health = maxhp;
            this.maxHealth = maxhp;
        }

        public void ResetToStartingHealth()
        {
            this.IncreaseMaxHealth(originalHP - this.maxHealth);
            this.IncreaseCurrentHealth(originalHP - this.health);
        }

        public bool IsDead()
        {
            return health == 0;
        }

        public void Revive()
        {
            if(IsDead())
            {
                this.health = 1;
                ResetToStartingHealth();
                this.Host.Raise(new UnitRevivedEvent(this.Host));
            }
        }


        public int Health 
        {
            get
            {
                return health;
            }
        
        }

        /// <summary>
        /// Increase/Decrease the current health of unit
        /// </summary>
        /// <param name="inc">a positive value increases the current health, while a negative decreases it</param>
        public void IncreaseCurrentHealth(int inc)
        {
            if (IsDead())
                return;

            int oldhp = health;
            int newhp = health+inc;
            if (inc > maxHealth)
                newhp = maxHealth;
            if (inc < 0)
                newhp = 0;

            health = newhp;
            this.Host.Raise(new UnitHealthChangedEvent(oldhp, newhp));
        }

        public int MaxHealth
        {

            get
            {
                return maxHealth;
            }
            
        }

        /// <summary>
        /// Increase/Decrease the max health of unit
        /// </summary>
        /// <param name="inc">a positive value increases the max health, while a negative decreases it</param>
        public void IncreaseMaxHealth(int inc)
        {
            int newMax = this.maxHealth+inc;
            int oldMax = this.maxHealth;
            if (newMax < 1)
                newMax = 1;
            this.maxHealth = newMax;
            this.Host.Raise(new UnitMaxHealthChangedEvent(oldMax, newMax));
            this.IncreaseCurrentHealth(inc);
        }


        public float HealthPct 
        {
            get
            {
                return ((float)Health)*100f / ((float)MaxHealth);
            }
        }
    }
}
