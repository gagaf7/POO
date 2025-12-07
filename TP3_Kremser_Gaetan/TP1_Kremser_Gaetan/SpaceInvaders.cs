using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3_Kremser_Gaetan;

namespace TP3_Kremser_Gaetan
{
    /// <summary>
    /// Classe principale du jeu Space Invaders
    /// Gère le menu, les joueurs, les vaisseaux et la partie
    /// </summary>
    internal class SpaceInvaders
    {
        private List<Player> players = new List<Player>();
        private Player currentPlayer = null;
        private List<Spaceship> enemies = new List<Spaceship>();
        private Random rand = new Random();

        public SpaceInvaders()
        {
            Init();
        }

        /// <summary>
        /// Initialise le jeu avec les ennemis par défaut
        /// </summary>
        private void Init()
        {
            enemies = new List<Spaceship>
            {
                new B_Wings(),
                new Tardis(),
                new Rocinante(),
                new Dart(),
                new F_18()
            };

            enemies[0].AddWeapon(Armory.GetInstance().GetWeaponByName("AK-47"));
            enemies[2].AddWeapon(Armory.GetInstance().GetWeaponByName("Roquette launcher"));
            enemies[2].AddWeapon(Armory.GetInstance().GetWeaponByName("Missile launcher"));
        }

        /// <summary>
        /// Menu principal du jeu
        /// </summary>
        private void MenuPrincipal()
        {
            bool quitter = false;
            while (!quitter)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════════");
                Console.WriteLine("      SPACE INVADERS - MENU PRINCIPAL");
                Console.WriteLine("═══════════════════════════════════════\n");

                if (currentPlayer != null)
                {
                    Console.WriteLine($"Joueur actuel: {currentPlayer.getAlias()}\n");
                }

                Console.WriteLine("1. Gestion des joueurs");
                Console.WriteLine("2. Gestion du vaisseau");
                Console.WriteLine("3. Gestion de l'armurerie");
                Console.WriteLine("4. Lancer une partie");
                Console.WriteLine("5. Quitter\n");

                Console.Write("Choisissez une option: ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        MenuJoueurs();
                        break;
                    case "2":
                        if (currentPlayer != null)
                            MenuVaisseau();
                        else
                            Console.WriteLine("Erreur: Sélectionnez d'abord un joueur");
                        Console.ReadKey();
                        break;
                    case "3":
                        MenuArmurerie();
                        break;
                    case "4":
                        if (currentPlayer != null)
                            LancerPartie();
                        else
                            Console.WriteLine("Erreur: Sélectionnez d'abord un joueur");
                        Console.ReadKey();
                        break;
                    case "5":
                        quitter = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Menu de gestion des joueurs
        /// </summary>
        private void MenuJoueurs()
        {
            bool retour = false;
            while (!retour)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════════");
                Console.WriteLine("      GESTION DES JOUEURS");
                Console.WriteLine("═══════════════════════════════════════\n");

                Console.WriteLine("1. Créer un nouveau joueur");
                Console.WriteLine("2. Sélectionner un joueur");
                Console.WriteLine("3. Supprimer un joueur");
                Console.WriteLine("4. Lister les joueurs");
                Console.WriteLine("5. Retour\n");

                Console.Write("Choisissez une option: ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        CreerJoueur();
                        break;
                    case "2":
                        SelectionnersJoueur();
                        break;
                    case "3":
                        SupprimerJoueur();
                        break;
                    case "4":
                        ListerJoueurs();
                        break;
                    case "5":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Crée un nouveau joueur
        /// </summary>
        private void CreerJoueur()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      CRÉER UN JOUEUR");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();

            Console.Write("Nom: ");
            string nom = Console.ReadLine();

            Console.Write("Alias: ");
            string alias = Console.ReadLine();

            Console.WriteLine("\nChoisissez un vaisseau:");
            Console.WriteLine("1. ViperMKII");
            Console.WriteLine("2. B-Wings");
            Console.WriteLine("3. Rocinante");
            Console.Write("Choix: ");
            string choixVaisseau = Console.ReadLine();

            Spaceship vaisseau = choixVaisseau switch
            {
                "1" => new ViperMKII(),
                "2" => new B_Wings(),
                "3" => new Rocinante(),
                _ => new ViperMKII()
            };

            Player newPlayer = new Player(prenom, nom, alias, vaisseau);
            players.Add(newPlayer);
            Console.WriteLine($"\n✓ Joueur '{alias}' créé avec succès");
            Console.ReadKey();
        }

        /// <summary>
        /// Sélectionne le joueur courant
        /// </summary>
        private void SelectionnersJoueur()
        {
            Console.Clear();
            if (players.Count == 0)
            {
                Console.WriteLine("Erreur: Aucun joueur disponible");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      SÉLECTIONNER UN JOUEUR");
            Console.WriteLine("═══════════════════════════════════════\n");

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].getAlias()} ({players[i].ToString()})");
            }

            Console.Write("\nChoisissez un joueur: ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= players.Count)
            {
                currentPlayer = players[choix - 1];
                Console.WriteLine($"\n✓ Joueur '{currentPlayer.getAlias()}' sélectionné");
            }
            else
            {
                Console.WriteLine("Choix invalide");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Supprime un joueur
        /// </summary>
        private void SupprimerJoueur()
        {
            Console.Clear();
            if (players.Count == 0)
            {
                Console.WriteLine("Erreur: Aucun joueur disponible");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      SUPPRIMER UN JOUEUR");
            Console.WriteLine("═══════════════════════════════════════\n");

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].getAlias()}");
            }

            Console.Write("\nChoisissez un joueur à supprimer: ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= players.Count)
            {
                Player removed = players[choix - 1];
                players.RemoveAt(choix - 1);
                if (currentPlayer == removed)
                    currentPlayer = null;
                Console.WriteLine($"\n✓ Joueur '{removed.getAlias()}' supprimé");
            }
            else
            {
                Console.WriteLine("Choix invalide");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Liste tous les joueurs
        /// </summary>
        private void ListerJoueurs()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      LISTE DES JOUEURS");
            Console.WriteLine("═══════════════════════════════════════\n");

            if (players.Count == 0)
            {
                Console.WriteLine("Aucun joueur créé");
            }
            else
            {
                foreach (Player p in players)
                {
                    Console.WriteLine($"- {p.ToString()}");
                }
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Menu de gestion du vaisseau
        /// </summary>
        private void MenuVaisseau()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      GESTION DU VAISSEAU");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.WriteLine($"Vaisseau: {currentPlayer.playerSpaceShip.GetType().Name}");
            Console.WriteLine($"Structure: {currentPlayer.playerSpaceShip.getCurrentStructure()}/{currentPlayer.playerSpaceShip.getMaxStructure()}");
            Console.WriteLine($"Bouclier: {currentPlayer.playerSpaceShip.getCurrentShield()}/{currentPlayer.playerSpaceShip.getMaxShield()}\n");

            Console.WriteLine("Armes actuelles:");
            currentPlayer.playerSpaceShip.ViewWeapons();

            Console.WriteLine("\n1. Ajouter une arme");
            Console.WriteLine("2. Retirer une arme");
            Console.WriteLine("3. Retour");
            Console.Write("\nChoisissez une option: ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AjouterArmeAuVaisseau();
                    break;
                case "2":
                    RetirerArmeAuVaisseau();
                    break;
            }
        }

        /// <summary>
        /// Ajoute une arme au vaisseau du joueur
        /// </summary>
        private void AjouterArmeAuVaisseau()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      AJOUTER UNE ARME");
            Console.WriteLine("═══════════════════════════════════════\n");

            List<Weapon> weapons = Armory.GetInstance().GetWeaponLst();
            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weapons[i].name}");
            }

            Console.Write("\nChoisissez une arme: ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= weapons.Count)
            {
                currentPlayer.playerSpaceShip.AddWeapon(weapons[choix - 1]);
                Console.WriteLine($"✓ Arme '{weapons[choix - 1].name}' ajoutée");
            }
            else
            {
                Console.WriteLine("Choix invalide");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Retire une arme du vaisseau du joueur
        /// </summary>
        private void RetirerArmeAuVaisseau()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      RETIRER UNE ARME");
            Console.WriteLine("═══════════════════════════════════════\n");

            List<Weapon> weapons = currentPlayer.playerSpaceShip.SpaceshipWeapons;
            if (weapons.Count == 0)
            {
                Console.WriteLine("Aucune arme à retirer");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weapons[i].name}");
            }

            Console.Write("\nChoisissez une arme à retirer: ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= weapons.Count)
            {
                currentPlayer.playerSpaceShip.RemoveWeapons(weapons[choix - 1]);
                Console.WriteLine($"✓ Arme '{weapons[choix - 1].name}' retirée");
            }
            else
            {
                Console.WriteLine("Choix invalide");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Menu de gestion de l'armurerie
        /// </summary>
        private void MenuArmurerie()
        {
            bool retour = false;
            while (!retour)
            {
                Console.Clear();
                Console.WriteLine("═══════════════════════════════════════");
                Console.WriteLine("      GESTION DE L'ARMURERIE");
                Console.WriteLine("═══════════════════════════════════════\n");

                Console.WriteLine("1. Afficher l'armurerie");
                Console.WriteLine("2. Ajouter une arme");
                Console.WriteLine("3. Modifier une arme");
                Console.WriteLine("4. Supprimer une arme");
                Console.WriteLine("5. Importer depuis un fichier");
                Console.WriteLine("6. Top 5 - Dommages moyens");
                Console.WriteLine("7. Top 5 - Dommages minimums");
                Console.WriteLine("8. Retour\n");

                Console.Write("Choisissez une option: ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Armory.GetInstance().ArmoryView();
                        break;
                    case "2":
                        AjouterArmeArmurerie();
                        break;
                    case "3":
                        ModifierArmeArmurerie();
                        break;
                    case "4":
                        SupprimerArmeArmurerie();
                        break;
                    case "5":
                        ImporterArmes();
                        break;
                    case "6":
                        Armory.GetInstance().AfficherTop5AverageDamage();
                        break;
                    case "7":
                        Armory.GetInstance().AfficherTop5MinDamage();
                        break;
                    case "8":
                        retour = true;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }

                if (choix != "8")
                    Console.ReadKey();
            }
        }

        /// <summary>
        /// Ajoute une arme manuelle à l'armurerie
        /// </summary>
        private void AjouterArmeArmurerie()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      AJOUTER UNE ARME");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.Write("Nom de l'arme: ");
            string nom = Console.ReadLine();

            Console.WriteLine("Type:");
            Console.WriteLine("1. Direct");
            Console.WriteLine("2. Explosive");
            Console.WriteLine("3. Guided");
            Console.Write("Choix: ");
            Weapon.EWeaponType type = Console.ReadLine() switch
            {
                "1" => Weapon.EWeaponType.Direct,
                "2" => Weapon.EWeaponType.Explosive,
                "3" => Weapon.EWeaponType.Guided,
                _ => Weapon.EWeaponType.Direct
            };

            Console.Write("Dommages minimum: ");
            int minDamage = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Dommages maximum: ");
            int maxDamage = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Temps de rechargement: ");
            double reloadTime = double.Parse(Console.ReadLine() ?? "0");

            Weapon weapon = new Weapon(nom, type, minDamage, maxDamage, reloadTime);
            Armory.GetInstance().AddWeapon(weapon);
        }

        /// <summary>
        /// Modifie une arme existante
        /// </summary>
        private void ModifierArmeArmurerie()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      MODIFIER UNE ARME");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.Write("Nom de l'arme à modifier: ");
            string nom = Console.ReadLine();

            try
            {
                Weapon weapon = Armory.GetInstance().GetWeaponByName(nom);

                Console.Write("Nouveaux dommages minimum: ");
                int minDamage = int.Parse(Console.ReadLine() ?? weapon.minDamage.ToString());

                Console.Write("Nouveaux dommages maximum: ");
                int maxDamage = int.Parse(Console.ReadLine() ?? weapon.maxDamage.ToString());

                Console.Write("Nouveau temps de rechargement: ");
                double reloadTime = double.Parse(Console.ReadLine() ?? weapon.reloadTime.ToString());

                Armory.GetInstance().ModifyWeapon(nom, minDamage, maxDamage, reloadTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        /// <summary>
        /// Supprime une arme de l'armurerie
        /// </summary>
        private void SupprimerArmeArmurerie()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      SUPPRIMER UNE ARME");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.Write("Nom de l'arme à supprimer: ");
            string nom = Console.ReadLine();

            try
            {
                Weapon weapon = Armory.GetInstance().GetWeaponByName(nom);
                Armory.GetInstance().RemoveWeapon(weapon);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
        }

        /// <summary>
        /// Importe les armes depuis un fichier texte
        /// </summary>
        private void ImporterArmes()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("      IMPORTER DES ARMES");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.Write("Chemin du fichier: ");
            string filePath = Console.ReadLine();

            ArmeImporteur importeur = new ArmeImporteur();
            importeur.SetMinLongueurMot(3);

            Dictionary<string, int> frequences = importeur.ImporterArmes(filePath);

            Console.WriteLine($"\n✓ {frequences.Count} mots uniques trouvés");
            Console.WriteLine("Génération des armes...\n");

            int armesCrees = 0;
            foreach (var entree in frequences.OrderByDescending(x => x.Value).Take(10))
            {
                Weapon weapon = importeur.GenererArme(entree.Key, entree.Value);
                Armory.GetInstance().AddWeapon(weapon);
                Console.WriteLine($"  ✓ Arme créée: {weapon.name} ({weapon.weaponType})");
                armesCrees++;
            }

            Console.WriteLine($"\n✓ {armesCrees} armes créées avec succès");
        }

        /// <summary>
        /// Lance une partie complète
        /// </summary>
        private void LancerPartie()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("     SPACE INVADERS - DÉBUT DE PARTIE");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.WriteLine($"Joueur: {currentPlayer.getAlias()}");
            Console.WriteLine($"Vaisseau: {currentPlayer.playerSpaceShip.GetType().Name}\n");

            Console.WriteLine("Appuyez sur une touche pour commencer...\n");
            Console.ReadKey();

            int round = 1;
            Spaceship playerShip = currentPlayer.playerSpaceShip;

            while (playerShip.getCurrentStructure() > 0 && enemies.Any(s => s.getCurrentStructure() > 0))
            {
                PlayRound(round);
                round++;

                if (playerShip.getCurrentStructure() <= 0)
                {
                    Console.WriteLine("\n═══════════════════════════════════════");
                    Console.WriteLine("               DÉFAITE");
                    Console.WriteLine("═══════════════════════════════════════");
                    break;
                }

                if (!enemies.Any(s => s.getCurrentStructure() > 0))
                {
                    Console.WriteLine("\n═══════════════════════════════════════");
                    Console.WriteLine("               VICTOIRE");
                    Console.WriteLine("═══════════════════════════════════════");
                    break;
                }

                Console.WriteLine("\nContinuer... (appuyez sur une touche)");
                Console.ReadKey();
            }

            Console.WriteLine("\nFin de la partie. Appuyez sur une touche...");
            Console.ReadKey();
        }

        /// <summary>
        /// Joue un tour complet du jeu
        /// </summary>
        private void PlayRound(int roundNumber)
        {
            Console.WriteLine($"\n========== TOUR {roundNumber} ==========\n");

            Spaceship playerShip = currentPlayer.playerSpaceShip;

            // Utilisez les capacités spéciales
            foreach (Spaceship ship in new List<Spaceship>(enemies))
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
            if (playerShip.getCurrentStructure() > 0)
            {
                playerShip.setCurrentShield(Math.Min(playerShip.getCurrentShield() + 2, playerShip.getMaxShield()));
            }

            // Enemies attack player
            foreach (Spaceship enemy in enemies)
            {
                if (enemy.getCurrentStructure() > 0)
                {
                    Console.WriteLine($"\n[{enemy.GetType().Name}] attaque");

                    if (enemy is F_18)
                    {
                        Console.WriteLine("  [F-18] EXPLOSE et inflige 10 dégâts!");
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

            // Player attacks random enemy
            List<Spaceship> activeEnemies = enemies.Where(s => s.getCurrentStructure() > 0).ToList();
            if (activeEnemies.Count == 0)
            {
                return;
            }

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

        /// <summary>
        /// Point d'entrée principal avec support des arguments de ligne de commande
        /// </summary>
            static void Main(string[] args)
        {
            SpaceInvaders game = new SpaceInvaders();

            // Si un fichier est fourni en argument, l'importer automatiquement
            if (args.Length > 0)
            {
                ArmeImporteur importeur = new ArmeImporteur();
                Dictionary<string, int> frequences = importeur.ImporterArmes(args[0]);
                Console.WriteLine("Affichage des fréquences...");
                importeur.AfficherFrequence();
                Console.WriteLine("\nAppuyez sur une touche pour accéder au menu...");
                Console.ReadKey();
            }

            game.MenuPrincipal();
        }
    }
}


