using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Kremser_Gaetan
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
            Weapon FulguroPoing = new Weapon("Fulguro Poing", 1000, 3, Weapon.EWeaponType.Direct);
            Armory.GetInstance().AddWeapon(FulguroPoing);
            Spaceship ship1 = new Spaceship(100, 50);
            Spaceship ship2 = new Spaceship(120, 60);
            Spaceship ship3 = new Spaceship(150, 75);

            ship1.AddWeapon(Armory.GetInstance().GetWeaponByName("AK-47"));
            ship2.AddMultipleWeapons(Weapons2);
            ship3.AddWeapon(Armory.GetInstance().GetWeaponByName("Roquette launcher"));
            ship3.AddWeapon(Armory.GetInstance().GetWeaponByName("Missile launcher"));




            
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


