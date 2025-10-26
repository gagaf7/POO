using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Kremser_Gaetan
{
    class Spaceship
    {
        private int maxStructure;
        private int maxShield;
        private int currentStructure;
        private int currentShield;
        private bool isAlive;

        public List<Weapon> SpaceshipWeapons { get; }

        public Spaceship(int maxStructure, int maxShield)
        {
            this.maxStructure = maxStructure;
            this.maxShield = maxShield;
            this.currentStructure = maxStructure;
            this.currentShield = maxShield;
            this.isAlive = true;
            SpaceshipWeapons = new List<Weapon>();
        }
        public int getMaxStructure() { return maxStructure; }
        public int getMaxShield() { return maxShield; }
        public int getCurrentStructure() { return currentStructure; }
        public int getCurrentShield() { return currentShield; }
        public int setCurrentStructure(int structure) { return currentStructure = structure; }
        public int setCurrentShield(int shield) { return currentShield = shield; }
        public void isDestroyed()
        {
            if (currentStructure <= 0)
            {
                isAlive = false;
                Console.WriteLine("Spaceship destroyed!");
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            foreach (Weapon armoryWeapon in Armory.GetInstance().GetWeaponLst())
            {
                if (weapon == armoryWeapon)
                {
                    SpaceshipWeapons.Add(weapon);
                }
            }
        }

        public void AddMultipleWeapons(List<Weapon> weaponList)
        {
            foreach (Weapon weapon in weaponList)
            {
                foreach (Weapon armoryWeapon in Armory.GetInstance().GetWeaponLst())
                {
                    if (weapon == armoryWeapon)
                    {
                        SpaceshipWeapons.Add(weapon);
                    }
                }
            }
        }

        public void RemoveWeapons(Weapon weapon)
        {
            SpaceshipWeapons.Remove(weapon);
        }

        public void ClearWeapons()
        {
            SpaceshipWeapons.Clear();
        }

        public void ViewWeapons()
        {
            foreach (Weapon weapon in SpaceshipWeapons)
            {
                Weapon.WeaponInfo(weapon);
            }
        }
        public void ViewShip()
        {
            Console.WriteLine($" Structure max : {maxStructure} \n Structure actuelle : {currentStructure} \n Boucliers max : {maxShield} \n" +
                $" Boucliers actuels : {currentShield} \n Vaisseau détruit : {isDestroyed} \n");
            Console.WriteLine(" - Liste des armes : \n");
            ViewWeapons();
        }

        public double AverageDamage()
        {
            double damageTotal = 0;
            double nbWeapons = 0;
            foreach (Weapon weapon in SpaceshipWeapons)
            {
                damageTotal += (weapon.MaxDamage + weapon.MinDamage) / 2;
                nbWeapons++;
            }
            return damageTotal / nbWeapons;
        }



    }
}
