using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class Projectile : BaseProjectile
    {
        protected override void Start()
        {
            base.Start();
            var forse = new Vector2(_speed * Direction, 0);
            Rigidbody.AddForce(forse, ForceMode2D.Impulse);
        }
    }
}
