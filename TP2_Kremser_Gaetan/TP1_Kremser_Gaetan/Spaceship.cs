using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    abstract class Spaceship
    {
        protected string name { get; set; }
        protected double maxStructure;
        protected double maxShield;
        protected double currentStructure;
        protected double currentShield;
        protected bool isAlive;

        public List<Weapon> SpaceshipWeapons { get; set; }

        public Spaceship( double MaxStructure, double MaxShield)
        {
            maxStructure = MaxStructure;
            maxShield = MaxShield;
            currentStructure = MaxStructure;
            currentShield = MaxShield;
            isAlive = true;
            SpaceshipWeapons = new List<Weapon>();
        }
        public Spaceship()
        {
            maxStructure = 0;
            maxShield = 0;
            currentStructure = 0;
            currentShield = 0;
            isAlive = true;
            SpaceshipWeapons = new List<Weapon>();
        }

        public double getMaxStructure() { return maxStructure; }
        public double getMaxShield() { return maxShield; }
        public double getCurrentStructure() { return currentStructure; }
        public double getCurrentShield() { return currentShield; }
        public double setCurrentStructure(double structure) { return currentStructure = structure; }
        public double setCurrentShield(double shield) { return currentShield = shield; }
        public void isDestroyed()
        {
            if (currentStructure <= 0)
            {
                isAlive = false;
                Console.WriteLine("Spaceship destroyed!");
            }
        }

        public virtual void AddWeapon(Weapon weapon)
        {
            foreach (Weapon armoryWeapon in Armory.GetInstance().GetWeaponLst())
            {
                if (weapon == armoryWeapon && SpaceshipWeapons.Count <= 3)
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
                $" Boucliers actuels : {currentShield} \n Vaisseau détruit : {!isAlive} \n");
            Console.WriteLine(" - Liste des armes : \n");
            ViewWeapons();
        }

        public double AverageDamage()
        {
            double damageTotal = 0;
            double nbWeapons = 0;
            foreach (Weapon weapon in SpaceshipWeapons)
            {
                damageTotal += (weapon.maxDamage + weapon.minDamage) / 2;
                nbWeapons++;
            }
            return damageTotal / nbWeapons;
        }

        public virtual void TakeDamages(double totalDamages)
        {
            double remainingDamages;
            if (currentShield >= totalDamages)
            {
                currentShield -= totalDamages;
                remainingDamages = 0;
            }
            else
            {
                remainingDamages = totalDamages - currentShield;
                currentShield = 0;
                currentStructure -= remainingDamages;
            }
        }
        public virtual void ShootTarget(Spaceship target)
        {
            double totalDamages = 0;
            foreach (Weapon weapon in SpaceshipWeapons)
            {
                int damage = weapon.Shoot();
                totalDamages += damage;
            }
            target.TakeDamages(totalDamages);
        }

        public void ShootTarget(Spaceship target,Weapon weapon)
        {
            double totalDamages = weapon.Shoot();
            target.TakeDamages(totalDamages);
        }


    }
}
