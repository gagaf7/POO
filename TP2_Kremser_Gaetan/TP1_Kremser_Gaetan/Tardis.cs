using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class Tardis : Spaceship, IAbility
    {
        public Tardis(double structure, double shield) : base(structure, shield)
        {
            name = "Tardis";
            maxStructure = 1;
            maxShield = 0;
            currentStructure = 1;
            currentShield = 0;
            SpaceshipWeapons.Capacity = 0;
        }
        public Tardis() : base()
        {
            name = "Tardis";
            maxStructure = 1;
            maxShield = 0;
            currentStructure = 1;
            currentShield = 0;
            SpaceshipWeapons.Capacity = 0;
        }
        public void UseAbility(List<Spaceship> spaceships)
        {
            if (spaceships.Count < 2) return;
            
            Random rand = new Random();
            int index1 = rand.Next(0, spaceships.Count);
            int index2 = rand.Next(0, spaceships.Count);
            
            if (index1 != index2)
            {
                Spaceship temp = spaceships[index1];
                spaceships[index1] = spaceships[index2];
                spaceships[index2] = temp;
            }
        }
    }
}
