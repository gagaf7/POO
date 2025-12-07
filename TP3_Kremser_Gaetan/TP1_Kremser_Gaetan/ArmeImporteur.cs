using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TP3_Kremser_Gaetan
{
    /// <summary>
    /// Classe responsable de l'importation d'armes à partir d'un fichier texte.
    /// Analyse la fréquence des mots et génère des armes basées sur leurs propriétés.
    /// </summary>
    internal class ArmeImporteur
    {
        private Dictionary<string, int> frequenceMots = new Dictionary<string, int>();
        private int minLongueurMot = 3;
        private List<string> listeNoire = new List<string> { "le", "la", "de", "et", "un", "une", "des", "du", "à", "en", "est", "que", "qui", "pour", "par", "sur", "ce", "se", "il", "elle", "on", "ne", "pas", "plus", "moins", "ou", "mais", "dont", "où", "donc", "si", "bien", "tout", "même", "tel", "quel", "très" };
        private Random rand = new Random();

        public ArmeImporteur()
        {
        }

        /// <summary>
        /// Définit la longueur minimale requise pour un mot
        /// </summary>
        public void SetMinLongueurMot(int longueur)
        {
            minLongueurMot = longueur;
        }

        /// <summary>
        /// Définit la liste noire des mots à exclure
        /// </summary>
        public void SetListeNoire(List<string> liste)
        {
            listeNoire = liste;
        }

        /// <summary>
        /// Importe les armes à partir d'un fichier texte
        /// </summary>
        public Dictionary<string, int> ImporterArmes(string filePath)
        {
            frequenceMots.Clear();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Erreur: Le fichier '{filePath}' n'existe pas.");
                return frequenceMots;
            }

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        TraiterLigne(line);
                    }
                }

                Console.WriteLine($"✓ Fichier '{filePath}' importé avec succès ({frequenceMots.Count} mots uniques)");
                return frequenceMots;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'importation: {ex.Message}");
                return frequenceMots;
            }
        }

        /// <summary>
        /// Traite une ligne du fichier pour extraire et compter les mots
        /// </summary>
        private void TraiterLigne(string line)
        {
            // Diviser par caractères de séparation
            string[] mots = line.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?', ';', ':', '-', '(', ')', '"', '\'', '/', '\\' },
                                       StringSplitOptions.RemoveEmptyEntries);

            foreach (string mot in mots)
            {
                string motNormalise = NormaliserMot(mot);

                // Valider le mot avant l'ajout
                if (ValiderMot(motNormalise))
                {
                    if (frequenceMots.ContainsKey(motNormalise))
                    {
                        frequenceMots[motNormalise]++;
                    }
                    else
                    {
                        frequenceMots[motNormalise] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Normalise un mot (minuscules, suppression ponctuation)
        /// </summary>
        private string NormaliserMot(string mot)
        {
            return mot.ToLower().Trim();
        }

        /// <summary>
        /// Valide un mot selon les critères définis
        /// </summary>
        private bool ValiderMot(string mot)
        {
            // Vérifier la longueur minimale
            if (mot.Length < minLongueurMot)
                return false;

            // Vérifier la liste noire
            if (listeNoire.Contains(mot))
                return false;

            // Vérifier que ce n'est pas vide
            if (string.IsNullOrWhiteSpace(mot))
                return false;

            return true;
        }

        /// <summary>
        /// Génère une arme basée sur un mot
        /// </summary>
        public Weapon GenererArme(string mot, int frequence)
        {
            // Type d'arme aléatoire
            Weapon.EWeaponType type = (Weapon.EWeaponType)rand.Next(3);
            
            // Les dégâts sont basés sur la longueur du mot et sa fréquence
            int minDamage = mot.Length * 2;
            int maxDamage = mot.Length * 3 + frequence;
            double reloadTime = 1.0 + (10.0 / frequence);

            return new Weapon(mot, type, minDamage, maxDamage, reloadTime);
        }

        /// <summary>
        /// Affiche les mots avec leur fréquence (triés par fréquence décroissante)
        /// </summary>
        public void AfficherFrequence()
        {
            Console.WriteLine("\n========== Fréquence des mots ==========\n");

            var motsTries = frequenceMots.OrderByDescending(x => x.Value).Take(20);

            foreach (var entree in motsTries)
            {
                Console.WriteLine($"{entree.Key}: {entree.Value}");
            }
        }

        /// <summary>
        /// Obtient le dictionnaire des fréquences
        /// </summary>
        public Dictionary<string, int> ObtenirFrequences()
        {
            return frequenceMots;
        }
    }
}       
