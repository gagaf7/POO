using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Kremser_Gaetan
{
    public class Weapon
    {
        public enum EWeaponType
        {
            Direct,
            Explosive,
            Guided
        }

        private string name;
        private int minDamage;
        private int maxDamage;
        private EWeaponType weaponType;

        public Weapon() { }

        public string Name
        {
            get;
        }

        public int MinDamage
        {
            get ;
        }

        public int MaxDamage
        {
            get;
        }

        public EWeaponType WeaponType
        {
            get;
        }


        public Weapon(string Name, int MaxDamage, int MinDamage, EWeaponType Type)
        {
            name = Name;
            maxDamage = MaxDamage;
            minDamage = MinDamage;
            WeaponType = Type;
        }


        public static void WeaponInfo(Weapon weapon)
        {
            Console.WriteLine($" Nom : {weapon.name} \n Dégats max : {weapon.maxDamage} \n Dégats min : {weapon.minDamage} \n Type : {weapon.WeaponType} \n");
        }


    }
}
