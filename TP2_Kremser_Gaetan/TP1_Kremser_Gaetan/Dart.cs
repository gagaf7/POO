using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class Dart : Spaceship
    {
        public Dart(double structure, double shield) : base(structure, shield)
        {
            name = "Dart";
            maxStructure = 10;
            maxShield = 3;
            currentStructure = 10;
            currentShield = 3;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Laser"));

        }
        public Dart() : base()
        {
            name = "Dart";
            maxStructure = 10;
            maxShield = 3;
            currentStructure = 10;
            currentShield = 3;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Laser"));

        }
        public override void AddWeapon(Weapon weapon)
        {
            foreach (Weapon armoryWeapon in Armory.GetInstance().GetWeaponLst())
            {
                if (weapon == armoryWeapon && SpaceshipWeapons.Count <= 3)
                {
                    if(weapon.weaponType == Weapon.EWeaponType.Direct)
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
