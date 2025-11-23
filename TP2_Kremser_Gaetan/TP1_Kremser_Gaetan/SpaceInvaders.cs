using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_Kremser_Gaetan
{
    internal class SpaceInvaders
    {
        private Player player;
        private List<Spaceship> enemies;
        private Random rand = new Random();

        public SpaceInvaders()
        {
            Init();
        }

        private void Init()
        {
            // Create enemy spaceships
            enemies = new List<Spaceship>
            {
                new B_Wings(),
                new Tardis(),
                new Rocinante(),
                new Dart(),
                new F_18()
            };

            // Add weapons to some enemies
            enemies[0].AddWeapon(Armory.GetInstance().GetWeaponByName("AK-47"));
            enemies[2].AddWeapon(Armory.GetInstance().GetWeaponByName("Roquette launcher"));
            enemies[2].AddWeapon(Armory.GetInstance().GetWeaponByName("Missile launcher"));

            // Create player
            player = new Player("gaetan", "kremser", "gagaf7", new ViperMKII());
        }

        private void PlayRound(int roundNumber)
        {
            Console.WriteLine($"\n========== TOUR {roundNumber} ==========\n");

            // Use special abilities
            foreach (Spaceship ship in enemies)
            {
                if (ship is IAbility ability && ship.getCurrentStructure() > 0)
                {
                    ability.UseAbility(enemies);
                }
            }

            // Regenerate shields
            foreach (Spaceship ship in enemies)
            {
                if (ship.getCurrentStructure() > 0)
                {
                    ship.setCurrentShield(Math.Min(ship.getCurrentShield() + 2, ship.getMaxShield()));
                }
            }
            if (player.playerSpaceShip.getCurrentStructure() > 0)
            {
                player.playerSpaceShip.setCurrentShield(Math.Min(player.playerSpaceShip.getCurrentShield() + 2, player.playerSpaceShip.getMaxShield()));
            }

            // Enemies attack player
            Spaceship playerShip = player.playerSpaceShip;
            foreach (Spaceship enemy in enemies)
            {
                if (enemy.getCurrentStructure() > 0)
                {
                    Console.WriteLine($"\n[{enemy.GetType().Name}] attaque");
                    
                    // F-18 explodes on contact with player dealing 10 damage
                    if (enemy is F_18)
                    {
                        Console.WriteLine("  [F-18] EXPLOSE au contact et inflige 10 dégâts!");
                        playerShip.TakeDamages(10);
                        enemy.setCurrentStructure(0);
                    }
                    else
                    {
                        enemy.ShootTarget(playerShip);
                    }
                    
                    Console.WriteLine($"  Joueur - Bouclier: {playerShip.getCurrentShield():F1} | Structure: {playerShip.getCurrentStructure():F1}");

                    if (playerShip.getCurrentStructure() <= 0)
                    {
                        return;
                    }
                }
            }

            // Player attacks random enemy with weighted probability
            // Specification: [1/n, 2/n, 3/n, ...] chance for each position
            List<Spaceship> activeEnemies = enemies.Where(s => s.getCurrentStructure() > 0).ToList();
            if (activeEnemies.Count == 0)
            {
                return;
            }

            // Weighted random: sum of arithmetic sequence 1+2+3+...+n = n*(n+1)/2
            int n = activeEnemies.Count;
            double totalWeight = n * (n + 1) / 2.0;
            double randomValue = rand.NextDouble() * totalWeight;
            double cumulative = 0;
            int targetIndex = 0;
            
            for (int i = 0; i < n; i++)
            {
                cumulative += (i + 1);
                if (randomValue <= cumulative)
                {
                    targetIndex = i;
                    break;
                }
            }

            Spaceship target = activeEnemies[targetIndex];
            Console.WriteLine($"\nJoueur tire sur [{target.GetType().Name}]");
            playerShip.ShootTarget(target);
            Console.WriteLine($"  {target.GetType().Name} - Bouclier: {target.getCurrentShield():F1} | Structure: {target.getCurrentStructure():F1}");

            if (target.getCurrentStructure() <= 0)
            {
                Console.WriteLine($"[{target.GetType().Name}] détruit!");
            }
        }

        static void Main(string[] args)
        {
            SpaceInvaders game = new SpaceInvaders();

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("     SPACE INVADERS - DÉBUT DE PARTIE");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.WriteLine(game.player.ToString());
            Console.WriteLine("\nVaisseau du joueur:");
            game.player.playerSpaceShip.ViewShip();

            Console.WriteLine("\nAppuyez sur une touche pour commencer...\n");
            Console.ReadKey();

            // Game loop
            int round = 1;
            Spaceship playerShip = game.player.playerSpaceShip;
            while (playerShip.getCurrentStructure() > 0 && game.enemies.Any(s => s.getCurrentStructure() > 0))
            {
                game.PlayRound(round);
                round++;

                if (playerShip.getCurrentStructure() <= 0)
                {
                    Console.WriteLine("\n═══════════════════════════════════════");
                    Console.WriteLine("               DÉFAITE");
                    Console.WriteLine("═══════════════════════════════════════");
                    break;
                }

                if (!game.enemies.Any(s => s.getCurrentStructure() > 0))
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
    }
}


