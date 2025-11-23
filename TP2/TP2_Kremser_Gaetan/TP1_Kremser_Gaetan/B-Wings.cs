using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class B_Wings : Spaceship
    {
        public B_Wings(double structure, double shield) : base(structure, shield)
        {
            name = "B-Wings";
            structure = 30;
            shield = 0;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Hammer"));
        }

        public B_Wings() : base()
        {
            name = "B-Wings";
            maxStructure = 30;
            maxShield = 0;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Hammer"));
        }

        public override void AddWeapon(Weapon weapon)
        {
            foreach (Weapon armoryWeapon in Armory.GetInstance().GetWeaponLst())
            {
                if (weapon == armoryWeapon && SpaceshipWeapons.Count <= 3)
                {
                    if (weapon.weaponType == Weapon.EWeaponType.Explosive)
                    {
                        weapon.reloadTime = 0;
                        SpaceshipWeapons.Add(weapon);
                    }
                    else
                    {
                        SpaceshipWeapons.Add(weapon);
                    }
                }
            }
        }
    }
}
