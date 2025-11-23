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
            structure = 1;
            shield = 0;
            SpaceshipWeapons.Capacity = 0;
        }
        public Tardis() : base()
        {
            name = "Tardis";
            maxStructure = 1;
            maxShield = 0;
            SpaceshipWeapons.Capacity = 0;
        }
        public void UseAbility(List<Spaceship> spaceships)
        {
            Random randIndexSelection = new Random();
            Random randIndexPlacement = new Random();
            int indexSelection = randIndexSelection.Next(0, spaceships.Count);
            int indexPlacement = randIndexPlacement.Next(0, spaceships.Count);
            Spaceship selectedShip = spaceships[indexSelection];
            Spaceship placementShip = spaceships[indexPlacement];
            spaceships.RemoveAt(indexSelection);
            spaceships.RemoveAt(indexPlacement);
            spaceships.Insert(indexSelection, placementShip);
            spaceships.Insert(indexPlacement, selectedShip);
        }
    }
}
