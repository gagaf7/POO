using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class Rocinante : Spaceship
    {
        public Rocinante(double structure, double shield) : base(structure, shield)
        {
            name = "Rocinante";
            structure = 3;
            shield = 5;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Torpille"));
        }
        public Rocinante() : base()
        {
            name = "Rocinante";
            maxStructure = 3;
            maxShield = 5;
            SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Torpille"));
        }

        public override void TakeDamages(double totalDamages)
        {
            Random rand = new Random();
            double remainingDamages;
            if(rand.Next(1,2) == 1)
            {
                totalDamages = 0;
                remainingDamages = 0;
            }
            else
            {

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
        }
    }
}
