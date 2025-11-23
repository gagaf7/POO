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
        private List<Spaceship> lstEnnemiSpaceship;
        private Random rand = new Random();

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
            Spaceship shipP1 = new Rocinante();
            Spaceship shipP2 = new B_Wings();
            Spaceship shipP3 = new ViperMKII();

            ship1.AddWeapon(Armory.GetInstance().GetWeaponByName("AK-47"));
            ship2.AddMultipleWeapons(Weapons2);
            ship3.AddWeapon(Armory.GetInstance().GetWeaponByName("Roquette launcher"));
            ship3.AddWeapon(Armory.GetInstance().GetWeaponByName("Missile launcher"));

            lstEnnemiSpaceship = new List<Spaceship>();
            lstEnnemiSpaceship.Add(ship1);
            lstEnnemiSpaceship.Add(ship2);
            lstEnnemiSpaceship.Add(ship3);
            lstEnnemiSpaceship.Add(ship4);
            lstEnnemiSpaceship.Add(ship5);
            lstEnnemiSpaceship.Add(ship6);

            lstPlayer = new List<Player>();
            Player p1 = new Player("gaetan", "kremser", "gagaf7", shipP3);
            lstPlayer.Add(p1);
            Player p2 = new Player("jean", "dupont", "jdupont", shipP1);
            lstPlayer.Add(p2);
            Player p3 = new Player("Theo", "mas", "nirox", shipP2);
            lstPlayer.Add(p3);
        }

        private void PlayRound(int roundNumber)
        {
            try
            {
                Console.WriteLine($"\n========== TOUR {roundNumber} ==========\n");

                // Début de tour : utiliser les aptitudes spéciales
                foreach (Spaceship ship in lstEnnemiSpaceship)
                {
                    if (ship is IAbility ability && ship.getCurrentStructure() > 0)
                    {
                        try
                        {
                            ability.UseAbility(lstEnnemiSpaceship);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erreur aptitude {ship.GetType().Name}: {ex.Message}");
                        }
                    }
                }

                // Régénération des boucliers
                foreach (Spaceship ship in lstEnnemiSpaceship)
                {
                    if (ship.getCurrentStructure() > 0)
                    {
                        double newShield = Math.Min(ship.getCurrentShield() + 2, ship.getMaxShield());
                        ship.setCurrentShield(newShield);
                    }
                }
                foreach (Player player in lstPlayer)
                {
                    if (player.playerSpaceShip.getCurrentStructure() > 0)
                    {
                        double newShield = Math.Min(player.playerSpaceShip.getCurrentShield() + 2, player.playerSpaceShip.getMaxShield());
                        player.playerSpaceShip.setCurrentShield(newShield);
                    }
                }

                // Tous les ennemis tirent sur le joueur principal
                Spaceship playerShip = lstPlayer[0].playerSpaceShip;
                foreach (Spaceship enemy in lstEnnemiSpaceship)
                {
                    if (enemy.getCurrentStructure() > 0 && enemy != playerShip)
                    {
                        Console.WriteLine($"\n[{enemy.GetType().Name}] attaque");
                        try
                        {
                            enemy.ShootTarget(playerShip);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erreur tir: {ex.Message}");
                        }
                        Console.WriteLine($"  Joueur - Bouclier: {playerShip.getCurrentShield():F1} | Structure: {playerShip.getCurrentStructure():F1}");

                        // Vérifier si c'est un F-18 au contact
                        if (enemy is F_18)
                        {
                            int enemyIndex = lstEnnemiSpaceship.IndexOf(enemy);
                            int playerIndex = lstEnnemiSpaceship.IndexOf(playerShip);
                            if (enemyIndex == playerIndex - 1 || enemyIndex == playerIndex + 1)
                            {
                                Console.WriteLine("  [F-18] EXPLOSE et inflige 10 dégâts!");
                                playerShip.TakeDamages(10);
                            }
                        }

                        playerShip.isDestroyed();
                        if (playerShip.getCurrentStructure() <= 0)
                        {
                            return;
                        }
                    }
                }

                // Le joueur tire sur un ennemi aléatoire
                List<Spaceship> ennemisActifs = lstEnnemiSpaceship.Where(s => s.getCurrentStructure() > 0 && s != playerShip).ToList();
                if (ennemisActifs.Count == 0)
                {
                    return;
                }

                double randomValue = rand.NextDouble();
                int targetIndex = 0;
                double cumul = 0;
                for (int i = 0; i < ennemisActifs.Count; i++)
                {
                    cumul += (double)(i + 1) / ennemisActifs.Count;
                    if (randomValue <= cumul)
                    {
                        targetIndex = i;
                        break;
                    }
                }

                Spaceship target = ennemisActifs[targetIndex];
                Console.WriteLine($"\nJoueur tire sur [{target.GetType().Name}]");
                try
                {
                    playerShip.ShootTarget(target);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur tir joueur: {ex.Message}");
                }
                Console.WriteLine($"  {target.GetType().Name} - Bouclier: {target.getCurrentShield():F1} | Structure: {target.getCurrentStructure():F1}");

                target.isDestroyed();
                if (target.getCurrentStructure() <= 0)
                {
                    Console.WriteLine($"[{target.GetType().Name}] détruit!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans PlayRound: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                SpaceInvaders unSpaceInvaders = new SpaceInvaders();

                Console.WriteLine("═══════════════════════════════════════");
                Console.WriteLine("     SPACE INVADERS - DÉBUT DE PARTIE");
                Console.WriteLine("═══════════════════════════════════════\n");

                foreach (Player player in unSpaceInvaders.lstPlayer)
                {
                    Console.WriteLine(player.ToString());
                }
                Console.WriteLine("\nArmurerie:\n");
                Armory.GetInstance().ArmoryView();

                Console.WriteLine("\nDétails des vaisseaux:\n");
                foreach (Player player in unSpaceInvaders.lstPlayer)
                {
                    Console.WriteLine($"Joueur: {player.getAlias()}");
                    player.playerSpaceShip.ViewShip();
                }

                Console.WriteLine("\nAppuyez sur une touche pour commencer...\n");
                Console.ReadKey();

                // Boucle de jeu
                int round = 1;
                Spaceship playerShip = unSpaceInvaders.lstPlayer[0].playerSpaceShip;
                while (playerShip.getCurrentStructure() > 0 && unSpaceInvaders.lstEnnemiSpaceship.Any(s => s.getCurrentStructure() > 0 && s != playerShip))
                {
                    unSpaceInvaders.PlayRound(round);
                    round++;

                    if (playerShip.getCurrentStructure() <= 0)
                    {
                        Console.WriteLine("\n═══════════════════════════════════════");
                        Console.WriteLine("               DÉFAITE");
                        Console.WriteLine("═══════════════════════════════════════");
                        break;
                    }

                    if (!unSpaceInvaders.lstEnnemiSpaceship.Any(s => s.getCurrentStructure() > 0 && s != playerShip))
                    {
                        Console.WriteLine("\n═══════════════════════════════════════");
                        Console.WriteLine("               VICTOIRE");
                        Console.WriteLine("═══════════════════════════════════════");
                        break;
                    }

                    Console.WriteLine("\nContinuer... (appuyez sur une touche)");
                    Console.ReadKey();
                }

                Console.WriteLine("\nFin du programme. Appuyez sur une touche...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur fatale: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}


