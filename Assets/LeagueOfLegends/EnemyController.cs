
namespace Assets.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// Controls an enemy
    /// </summary>
    public class EnemyController : Character
    {
        /// <summary>
        /// LeeSin controller
        /// </summary>
        public LeesinController LeeSin;

        /// <summary>
        /// A list  of enemies
        /// </summary>
        public static List<EnemyController> Enemies = new List<EnemyController>();

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected override void Start()
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), this.LeeSin.GetComponent<BoxCollider2D>());
            EnemyController.Enemies.Add(this);

            base.Start();
        }

        /// <summary>
        /// When a trigger enters
        /// </summary>
        /// <param name="collision">The collision</param>
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile p = collision.gameObject.GetComponent<Projectile>();
            if (p != null)
            {
                if (p.CarriedEffect == EffectEnum.LeeQLanded)
                {
                    LeeSin.OnQLanded(this);
                    Destroy(p.gameObject);
                }

                if (p.EffectVisualPrefab != null)
                {
                    var newEffect = Instantiate(p.EffectVisualPrefab);
                    newEffect.TargetCharacter = this;
                    newEffect.TargetEffect = p.CarriedEffect;
                    this.ApplyEffect(p.CarriedEffect, p.EffectDuration);
                }
            }
        }

        /// <summary>
        /// Called  when the enemy gameobject is destroyed
        /// </summary>
        protected void OnDestroy()
        {
            EnemyController.Enemies.Remove(this);
        }
    }
}
