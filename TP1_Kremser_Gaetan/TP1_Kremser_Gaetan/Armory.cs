using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Kremser_Gaetan
{
    public sealed class Armory
    {
        private static Armory? armoryInstance;

        private List<Weapon> weapons = new List<Weapon>();
        private Armory()
        {
            init();
        }
        

        private void init() { weapons = new List<Weapon>();
            weapons.Add(new Weapon("AK-47",  25,  35, Weapon.EWeaponType.Direct ));
            weapons.Add(new Weapon( "Roquette launcher", 20,  30,  Weapon.EWeaponType.Explosive ));
            weapons.Add(new Weapon("Missile launcher",  30,  50, Weapon.EWeaponType.Guided ));
        }

        public static Armory GetInstance()
        {
            if (armoryInstance == null)
            {
                armoryInstance = new Armory();
            }
            return armoryInstance;
        }

        public void ArmoryView()
        {
            foreach (Weapon weapon in weapons)
            {
                Weapon.WeaponInfo(weapon);
            }
        }
        public List<Weapon> GetWeaponLst()
        {
            return weapons;
        }

        public void AddWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
        }

        public void RemoveWeapon(Weapon weapon)
        {
            weapons.Remove(weapon);
        }

        public void ClearWeapons(Weapon weapon)
        {
            weapons.Clear();
        }

        public Weapon GetWeaponByName(string weaponName)
        {
            while (true)
            {
                foreach (Weapon weapon in weapons)
                {
                    if (weaponName == weapon.Name)
                    {
                        return weapon;
                    }
                }
            }
               
        }
    }
}
