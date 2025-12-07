using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_Kremser_Gaetan
{
    /// <summary>
    /// Singleton gérant l'armurerie centrale des armes du jeu
    /// </summary>
    public sealed class Armory
    {
        private static Armory? armoryInstance;
        private List<Weapon> weapons = new List<Weapon>();

        private Armory()
        {
            Init();
        }

        /// <summary>
        /// Initialise l'armurerie avec les armes par défaut
        /// </summary>
        private void Init()
        {
            weapons = new List<Weapon>();
            weapons.Add(new Weapon("AK-47", Weapon.EWeaponType.Direct, 25, 35, 5));
            weapons.Add(new Weapon("Roquette launcher", Weapon.EWeaponType.Explosive, 20, 30, 4));
            weapons.Add(new Weapon("Missile launcher", Weapon.EWeaponType.Guided, 30, 50, 4));
            weapons.Add(new Weapon("Laser", Weapon.EWeaponType.Direct, 2, 3, 2));
            weapons.Add(new Weapon("Hammer", Weapon.EWeaponType.Explosive, 1, 8, 1.5));
            weapons.Add(new Weapon("Torpille", Weapon.EWeaponType.Guided, 3, 3, 2));
            weapons.Add(new Weapon("Mitrailleuse", Weapon.EWeaponType.Direct, 6, 8, 1));
            weapons.Add(new Weapon("EMG", Weapon.EWeaponType.Explosive, 1, 7, 1.5));
            weapons.Add(new Weapon("Missile", Weapon.EWeaponType.Guided, 4, 100, 4));
            weapons.Add(new Weapon("Pistolet", Weapon.EWeaponType.Direct, 10, 15, 3));
            weapons.Add(new Weapon("Tête nucléaire", Weapon.EWeaponType.Explosive, 9999, 9999, 0, 9999));
        }

        /// <summary>
        /// Obtient l'instance unique de l'armurerie (Singleton)
        /// </summary>
        public static Armory GetInstance()
        {
            if (armoryInstance == null)
            {
                armoryInstance = new Armory();
            }
            return armoryInstance;
        }       

        /// <summary>
        /// Affiche toutes les armes de l'armurerie
        /// </summary>
        public void ArmoryView()
        {
            Console.WriteLine("\n========== ARMURERIE ==========\n");
            foreach (Weapon weapon in weapons)
            {
                Weapon.WeaponInfo(weapon);
            }
        }

        /// <summary>
        /// Retourne la liste complète des armes
        /// </summary>
        public List<Weapon> GetWeaponLst()
        {
            return weapons;
        }

        /// <summary>
        /// Ajoute une arme à l'armurerie
        /// </summary>
        public void AddWeapon(Weapon weapon)
        {
            if (weapon != null && !weapons.Contains(weapon))
            {
                weapons.Add(weapon);
                Console.WriteLine($"✓ Arme '{weapon.name}' ajoutée à l'armurerie");
            }
        }

        /// <summary>
        /// Supprime une arme de l'armurerie
        /// </summary>
        public void RemoveWeapon(Weapon weapon)
        {
            if (weapons.Remove(weapon))
            {
                Console.WriteLine($"✓ Arme '{weapon.name}' supprimée de l'armurerie");
            }
        }

        /// <summary>
        /// Modifie une arme existante
        /// </summary>
        public void ModifyWeapon(string weaponName, int minDamage, int maxDamage, double reloadTime)
        {
            Weapon weapon = GetWeaponByName(weaponName);
            if (weapon != null)
            {
                weapon.minDamage = minDamage;
                weapon.maxDamage = maxDamage;
                weapon.reloadTime = reloadTime;
                Console.WriteLine($"✓ Arme '{weaponName}' modifiée");
            }
        }

        /// <summary>
        /// Obtient une arme par son nom
        /// </summary>
        public Weapon GetWeaponByName(string weaponName)
        {
            foreach (Weapon weapon in weapons)
            {
                if (weaponName.Equals(weapon.name, StringComparison.OrdinalIgnoreCase))
                {
                    return weapon;
                }
            }
            throw new ArgumentException($"Arme '{weaponName}' non trouvée dans l'armurerie");
        }

        /// <summary>
        /// Retourne les 5 armes avec les plus gros dommages moyens (Lambda)
        /// </summary>
        public List<Weapon> GetTop5AverageDamage()
        {
            return weapons
                .OrderByDescending(w => (w.maxDamage + w.minDamage) / 2.0)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Retourne les 5 armes avec les dommages minimums les plus hauts (Lambda)
        /// </summary>
        public List<Weapon> GetTop5MinDamage()
        {
            return weapons
                .OrderByDescending(w => w.minDamage)
                .Take(5)
                .ToList();
        }

        /// <summary>
        /// Affiche les 5 meilleures armes par dommages moyens
        /// </summary>
        public void AfficherTop5AverageDamage()
        {
            Console.WriteLine("\n========== Top 5 Armes - Dommages Moyens ==========\n");
            int rang = 1;
            foreach (Weapon weapon in GetTop5AverageDamage())
            {
                double avgDamage = (weapon.maxDamage + weapon.minDamage) / 2.0;
                Console.WriteLine($"{rang}. {weapon.name}: {avgDamage:F2} dégâts moyens");
                rang++;
            }
        }

        /// <summary>
        /// Affiche les 5 meilleures armes par dommages minimums
        /// </summary>
        public void AfficherTop5MinDamage()
        {
            Console.WriteLine("\n========== Top 5 Armes - Dommages Minimums ==========\n");
            int rang = 1;
            foreach (Weapon weapon in GetTop5MinDamage())
            {
                Console.WriteLine($"{rang}. {weapon.name}: {weapon.minDamage} dégâts minimum");
                rang++;
            }
        }

        /// <summary>
        /// Vide l'armurerie (sauf les armes par défaut)
        /// </summary>
        public void ClearCustomWeapons()
        {
            weapons.Clear();
            Init();
        }
    }
}
