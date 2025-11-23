using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class F_18 : Spaceship, IAbility
    {
        
        public F_18(double structure, double shield) : base(structure, shield)
        {
            name = "F-18";
            structure = 15;
            shield = 0;
        }
        public F_18() : base()
        {
            name = "F-18";
            maxStructure = 15;
            maxShield = 0;
        }
        public void UseAbility(List<Spaceship> spaceships)
        {
            if (spaceships.Contains(this))
            {
                int index = spaceships.IndexOf(this);
                if(spaceships.IndexOf(this) == index - 1)
                {
                    spaceships[index - 1].setCurrentStructure(spaceships[index - 1].getCurrentStructure() - 10);
                }
                if (spaceships.IndexOf(this) == index + 1)
                {
                    spaceships[index + 1].setCurrentStructure(spaceships[index + 1].getCurrentStructure() - 10);
                }
            }
        }
    }
}
