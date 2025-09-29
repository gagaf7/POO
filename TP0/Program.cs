using System;
using System.Linq;
using System.Threading;

namespace TP0_Kremser_Gaetan
{
    class Program
    {
        static string ModifTailleCaractere(string nom, string prenom) {
            String nomPrenom = string.Empty;

            nomPrenom = nom.ToUpper() + " " + prenom.ToLower();

            return nomPrenom;
        }

        static float CalculIMC(int tailleCm, float poidsKg)
        {
            float tailleMetres = tailleCm / 100f;

            float imc = poidsKg / (tailleMetres * tailleMetres);

            return imc;
        }

        static void SavoirImc(float imc)
        {
            String message = string.Empty;

            if (imc < 16.5f)
            {
                message = "Attention à l’anorexie !";
            }
            else if ((imc > 18.5f) && (imc <= 16.5f))
            {
                message = "Maigreur";
            }
            else if ((imc > 25f) && (imc <= 18.5f))
            {
                message = "Corpulence normale";
            }
            else if ((imc > 30f) && (imc <= 25f))
            {
                message = "Surpoids";
            }
            else if ((imc > 35f) && (imc <= 30f))
            {
                message = "Obésité modérée";
            }
            else if ((imc > 40f) && (imc <= 35f))
            {
                message = "Obésité sévère";
            }
            else
            {
                message = "Obésité morbide";
            }

            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            String textBienvenue = "Bienvenue sur mon programme, jeune étranger imberbe";
            String nom = string.Empty;
            String prenom = String.Empty;
            int age = 0;
            int taille = 0;
            int poids = 0;
            int nbCheveux = 0;
            bool saisieValide = false;
            bool nomValide = true;
            bool prenomValide = true;
            int validationUtilisateur = 0;
            bool recommencer = true;
            bool saisieCorrecte = false;

            Console.WriteLine(textBienvenue);
            Console.ReadKey();

            while (recommencer)
            {

                do
                {
                    Console.WriteLine("Donne-moi ton nom, vil chenapan : ");
                    nom = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nom) || nom.Any(char.IsDigit))
                    {
                        Console.WriteLine("Erreur : le nom ne peut pas contenir de chiffres et ne doit pas être vide.");
                    }
                    else
                    {
                        nomValide = false;
                    }

                } while (nomValide);

                do
                {
                    Console.WriteLine("Et quel est ton prénom, petit galopin : ");
                    prenom = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(prenom) || prenom.Any(char.IsDigit))
                    {
                        Console.WriteLine("Erreur : le prénom ne peut pas contenir de chiffres et ne doit pas être vide.");
                    }
                    else
                    {
                        prenomValide = false;
                    }

                } while (prenomValide);

                do
                {
                    Console.WriteLine("Et quel est ton âge, petit galopin : ");
                    string saisie = Console.ReadLine();

                    if (!int.TryParse(saisie, out age))
                    {
                        Console.WriteLine("Erreur : l’âge doit être un nombre strictement positif.");
                    }

                } while (age <= 0);

                do
                {
                    Console.WriteLine("Et quelle est ta taille (en cm), petit galopin : ");
                    string saisie = Console.ReadLine();

                    if (!int.TryParse(saisie, out taille))
                    {
                        Console.WriteLine("Erreur : la taille doit être un nombre strictement positif.");
                    }

                } while (taille <= 0);

                do
                {
                    Console.WriteLine("Et quel est ton poids (en kilos), petit galopin : ");
                    string saisie = Console.ReadLine();

                    if (!int.TryParse(saisie, out poids))
                    {
                        Console.WriteLine("Erreur : le poids doit être un nombre strictement positif.");
                    }

                } while (poids <= 0);

                String salutation = string.Format("Bonjour {0} {1}!", nom, prenom);
                Console.WriteLine(salutation);
                Console.ReadKey();
                if (age < 18)
                {
                    Console.WriteLine("Tu n'as pas 18 ans donc tu peux pas profiter des bienfait de l'alcool.");
                }
                else
                {
                    Console.WriteLine("Tu fais partie des goats");
                }
                Console.ReadKey();

                Console.WriteLine("Ton IMC est de " + Convert.ToString(CalculIMC(taille, poids)));
                Console.ReadKey();
                SavoirImc(CalculIMC(taille, poids));
                Console.ReadKey();
                do
                {
                    Console.WriteLine("Combien de cheveux avez-vous sur la tête : ");
                    string cheveuxUtilisateur = Console.ReadLine();

                    if (int.TryParse(cheveuxUtilisateur, out nbCheveux))
                    {
                        if (nbCheveux >= 100000 && nbCheveux <= 150000)
                        {
                            saisieValide = true;
                        }
                        else
                        {
                            Console.WriteLine("Erreur : le nombre doit être compris entre 100000 et 150000.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erreur : vous devez entrer un nombre valide.");
                    }

                } while (!saisieValide);

                Console.WriteLine("Bravo, vous avez indiqué le bon nombre de cheveux !");
                Console.ReadKey();
                // À la fin → menu de choix
                Console.WriteLine("Veuillez sélectionner une option (1 à 4) : ");
                Console.WriteLine("1 - Quitter le programme");
                Console.WriteLine("2 - Recommencer le programme");
                Console.WriteLine("3 - Compter jusqu’à 10");
                Console.WriteLine("4 - Téléphoner à Tata Jacqueline");

                do
                {
                    string saisie = Console.ReadLine();
                    if (int.TryParse(saisie, out validationUtilisateur) && validationUtilisateur >= 1 && validationUtilisateur <= 4)
                    {
                        saisieCorrecte = true;
                    }
                    else
                    {
                        Console.WriteLine("Erreur : vous devez entrer un nombre entre 1 et 4.");
                    }
                } while (!saisieCorrecte);

                switch (validationUtilisateur)
                {
                    case 1: // Quitter
                        Console.WriteLine("Au revoir, à bientôt !");
                        Thread.Sleep(3000);
                        return;

                    case 2: // Recommencer
                        Console.WriteLine("Redémarrage du programme...");
                        Thread.Sleep(1000);
                        recommencer = true;
                        break;

                    case 3: // Compter jusqu’à 10
                        Console.WriteLine("Je vais compter jusqu’à 10 pour tes beaux yeux");
                        for (int i = 1; i <= 10; i++)
                        {
                            Console.WriteLine(i);
                            Thread.Sleep(1000); 
                        }
                        Console.WriteLine("Fin du comptage. Au revoir chérie!");
                        Thread.Sleep(3000);
                        return;

                    case 4: // Téléphoner à Tata Jacqueline
                        Console.WriteLine("Vous appelez Tata Jacqueline...");
                        Thread.Sleep(1500);
                        Console.WriteLine("...Bip... Bip... Bip...");
                        Thread.Sleep(2000);
                        Console.WriteLine("« Bonjour, vous êtes bien sur la messagerie de Tata Jacqueline. "
                                        + "Je ne peux pas vous répondre pour l’instant car je suis probablement "
                                        + "en train de jouer au bingo du jeudi soir :) . Laissez un message après le bip ! »");
                        Thread.Sleep(2000);
                        Console.WriteLine("Biiiiiip !");
                        Thread.Sleep(2000);
                        Console.WriteLine("Fin de l’appel. Au revoir !");
                        Thread.Sleep(3000);
                        return;
                }
            }
        }

    }

}
   
