using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_Kremser_Gaetan
{
    internal class Player
    {
        private string firstName;
        private string lastName;
        private string alias;
        public string name { get; private set; }
        public readonly Spaceship playerSpaceShip;

        public Player(string firstName, string lastName, string alias, Spaceship spaceship)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.alias = alias;
            playerSpaceShip = spaceship;

            lastName = formatNameNom(lastName);
            firstName = formatNamePrenom(firstName);

            name = $"{firstName} {lastName}";
        }

        public string getFirstName() { return firstName; }
        public string getLastName() { return lastName; }
        public string getAlias() { return alias; }

        private static string formatNamePrenom(string prenom)
        {
            return $"{char.ToUpper(prenom[0])}{prenom.Substring(1).ToLower()}";
        }
        private static string formatNameNom(string nom)
        {
            return $"{char.ToUpper(nom[0])}{nom.Substring(1).ToLower()}";
        }

        public override string ToString()
        {
            return $"{alias} ({name})";
        }

        public override bool Equals(object obj)
        {
            Player p = obj as Player;
            if (p == null)
                return false;
            return p.alias == this.alias;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
