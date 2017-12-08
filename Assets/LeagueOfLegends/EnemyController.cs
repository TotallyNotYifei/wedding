
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
        /// Override property to identify
        /// </summary>
        protected override bool IsFriendly
        {
            get
            {
                return false;
            }
        }

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
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            Projectile proj = collision.gameObject.GetComponent<Projectile>();
            if (proj != null)
            {
                if (proj.CarriedEffects.Contains(EffectEnum.LeeQLanded))
                {
                    LeeSin.OnQLanded(this);
                }

                for(int i= 0;i<proj.EffectVisualPrefabs.Count;i++)
                {
                    var effectPrefab = proj.EffectVisualPrefabs[i];
                    if (effectPrefab != null)
                    {
                        var newEffect = Instantiate(proj.EffectVisualPrefabs[i]);
                        newEffect.TargetCharacter = this;
                        newEffect.TargetEffect = proj.CarriedEffects[i];
                        newEffect.transform.position = new Vector3(this.transform.position.x, 0);
                    }
                }

                this.ApplyEffects(proj.CarriedEffects, proj.EffectDuration);
                this.TakeDamage(proj.Damage);

                proj.OnHittingEnemy();
            }

            base.OnTriggerEnter2D(collision);
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
