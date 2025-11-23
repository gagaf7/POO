using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class SpaceInvaders
    {
        private List<Player> lstPlayer;
        public SpaceInvaders()
        {
            Init();

        }
        private void Init()
        {
            List<Weapon> Weapons2 = new List<Weapon>();
            foreach (Weapon weapon in Armory.GetInstance().GetWeaponLst())
            {
                Weapons2.Add(weapon);
            }
            Weapon FulguroPoing = new Weapon("Fulguro Poing", Weapon.EWeaponType.Direct, 100, 1000, 10);
            Armory.GetInstance().AddWeapon(FulguroPoing);
            Spaceship ship1 = new B_Wings();
            Spaceship ship2 = new Tardis();
            Spaceship ship3 = new Rocinante();
            Spaceship ship4 = new Dart();
            Spaceship ship5 = new F_18();
            Spaceship ship6 = new ViperMKII();

            ship1.AddWeapon(Armory.GetInstance().GetWeaponByName("AK-47"));
            ship2.AddMultipleWeapons(Weapons2);
            ship3.AddWeapon(Armory.GetInstance().GetWeaponByName("Roquette launcher"));
            ship3.AddWeapon(Armory.GetInstance().GetWeaponByName("Missile launcher"));

            List<Spaceship> lstEnnemiSpaceship = new List<Spaceship>();

            lstEnnemiSpaceship.Add(ship1);
            lstEnnemiSpaceship.Add(ship2);
            lstEnnemiSpaceship.Add(ship3);
            lstEnnemiSpaceship.Add(ship4);
            lstEnnemiSpaceship.Add(ship5);
            lstEnnemiSpaceship.Add(ship6);





            lstPlayer = new List<Player>();
            Player p1 = new Player("gaetan", "kremser", "gagaf7",ship3);
            lstPlayer.Add(p1);
            Player p2 = new Player("jean", "dupont", "jdupont",ship1);
            lstPlayer.Add(p2);
            Player p3 = new Player("Theo", "mas", "nirox",ship2);
            lstPlayer.Add(p3);

        }

        static void Main(string[] args)
        {
            SpaceInvaders unSpaceInvaders = new SpaceInvaders();
            unSpaceInvaders.Init();
            foreach (Player player in unSpaceInvaders.lstPlayer)
            {
                Console.WriteLine(player.ToString());
            }
            Console.WriteLine("Armurerie : \n");
            Armory.GetInstance().ArmoryView();

            Console.WriteLine("Détails des vaisseaux des joueurs : \n");
            foreach (Player player in unSpaceInvaders.lstPlayer)
            {
                Console.WriteLine($"Joueur : {player.getAlias()} \n");
                player.playerSpaceShip.ViewShip();
            }
            Console.WriteLine();
            Console.ReadKey();

        }
    }
}


