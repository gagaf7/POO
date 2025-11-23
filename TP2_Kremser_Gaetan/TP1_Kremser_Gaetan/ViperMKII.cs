using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class ViperMKII : Spaceship
    {
        public ViperMKII(double structure, double shield) : base(structure, shield)
        {
            name = "Viper MK II";
            maxStructure = 10;
            maxShield = 15;
            currentStructure = 10;
            currentShield = 15;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Mitrailleuse"));
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("EMG"));
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Missile"));
        }
        public ViperMKII() : base()
        {
            name = "Viper MK II";
            maxStructure = 10;
            maxShield = 15;
            currentStructure = 10;
            currentShield = 15;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Mitrailleuse"));
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("EMG"));
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Missile"));
        }

        public override void ShootTarget(Spaceship target)
        {
            // ViperMKII only uses one recharged weapon per turn
            foreach (Weapon weapon in SpaceshipWeapons)
            {
                if (weapon.timeBeforeReload == 0)
                {
                    int damage = weapon.Shoot();
                    target.TakeDamages(damage);
                    return;
                }
            }
        }

    }
}
