using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    public class Weapon
    {
        public enum EWeaponType
        {
            Direct,
            Explosive,
            Guided
        }

        public string name { get; set; }
        public int minDamage { get;  set; }
        public int maxDamage { get; set; }
        public EWeaponType weaponType { get; set; }
        public double reloadTime { get; set; }
        public double timeBeforeReload { get; set; }
        public double selfDamage { get; set; } = 0;

        public Weapon() 
        {
            timeBeforeReload = reloadTime;
        }

        public Weapon(string Name, EWeaponType Type, int MinDamage, int MaxDamage, double ReloadTime)
        {
            name = Name;
            weaponType = Type;
            minDamage = MinDamage;
            maxDamage = MaxDamage;
            reloadTime = ReloadTime;
            timeBeforeReload = reloadTime;
        }

        public Weapon(string Name, EWeaponType Type, int MinDamage, int MaxDamage, double ReloadTime, double SelfDamage)
        {
            name = Name;
            weaponType = Type;
            minDamage = MinDamage;
            maxDamage = MaxDamage;
            reloadTime = ReloadTime;
            selfDamage = SelfDamage;
            timeBeforeReload = reloadTime;
        }

        public int weaponDamage()
        {
            int damaged;
            if (timeBeforeReload != 0)
            {
                damaged = 0;
            }
            else
            {
                Random rand = new Random();
                damaged = rand.Next(minDamage, maxDamage);
            }
            return damaged;
        }

        public static void WeaponInfo(Weapon weapon)
        {
            Console.WriteLine($" Nom : {weapon.name} \n Dégats max : {weapon.maxDamage} \n Dégats min : {weapon.minDamage} \n Type : {weapon.weaponType} \n");
        }

        public int Shoot()
        {
            int damage;
            Random rand = new Random();
            switch (weaponType){
                case EWeaponType.Direct:
                    if(rand.Next(1, 10) == 5)
                    {
                        damage = 0;
                        timeBeforeReload = reloadTime;
                        return damage;
                    }
                    else
                    {
                        damage = weaponDamage();
                        timeBeforeReload = reloadTime;
                        return damage;
                    }

                case EWeaponType.Explosive:
                    if (rand.Next(1, 4) == 3)
                    {
                        damage= 0;
                        timeBeforeReload = reloadTime;
                        return damage;  
                    }
                    else
                    {
                        damage = weaponDamage() * 2;
                        timeBeforeReload = reloadTime * 2;
                        return damage;
                    }

                case EWeaponType.Guided:
                    damage = minDamage;
                    timeBeforeReload = reloadTime;
                    return damage;
            }
            return -1;

        }



    }
}
