using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
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
            weapons.Add(new Weapon("AK-47", Weapon.EWeaponType.Direct , 25, 35,5));
            weapons.Add(new Weapon( "Roquette launcher", Weapon.EWeaponType.Explosive , 20, 30,4));
            weapons.Add(new Weapon("Missile launcher",  Weapon.EWeaponType.Guided, 30, 50,4));
            weapons.Add(new Weapon("Laser", Weapon.EWeaponType.Direct, 2, 3, 2));
            weapons.Add(new Weapon("Hammer", Weapon.EWeaponType.Explosive, 1, 8, 1.5));
            weapons.Add(new Weapon("Torpille", Weapon.EWeaponType.Guided, 3, 3, 2));
            weapons.Add(new Weapon("Mitrailleuse", Weapon.EWeaponType.Direct, 6, 8, 1));
            weapons.Add(new Weapon("EMG", Weapon.EWeaponType.Explosive, 1, 7, 1.5));
            weapons.Add(new Weapon("Missile",Weapon.EWeaponType.Guided, 4, 100, 4));
            weapons.Add(new Weapon("Pistolet", Weapon.EWeaponType.Direct, 10, 15, 3));
            weapons.Add(new Weapon("Tête nucléaire", Weapon.EWeaponType.Explosive, 9999, 9999,0,9999));
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
                    if (weaponName == weapon.name)
                    {
                        return weapon;
                    }
                }
            }
               
        }
    }
}
