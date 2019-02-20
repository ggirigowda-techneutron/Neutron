#region Copyright Neutron © 2019

//
// NAME:			DataGenerator.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
// DATE:			2/20/2019
// PURPOSE:			Security
//

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classlibrary.Crosscutting.Testing
{
    /// <summary>
    ///     Data generator
    /// </summary>
    public static class DataGenerator
    {
        // Method to generate random address (Address1, City, State, Zip)
        // Be sure to keep the number of items in the list consistent across the 4 lists
        public static List<Tuple<string, string, string, string>> GenerateRandomAddress(int count)
        {
            var address1 = new List<string>
            {
                "One main street",
                "Two main street",
                "Three main street",
                "Four main street",
                "Five main street",
                "Six main street",
                "Seven main street",
                "Eight main street",
                "Nine main street",
                "Ten main street",
                "Eleven main street",
                "Twelve main street"
            };

            var city = new List<string>
            {
                "Dumfries",
                "Woodbridge",
                "Springfield",
                "Alexandria",
                "Arlington",
                "Fairfax",
                "Tysons",
                "Herndon",
                "Reston",
                "Mclean",
                "Stafford",
                "Hay Market"
            };

            var state = new List<string>
            {
                "VA",
                "MD",
                "DC",
                "UT",
                "TX",
                "CA",
                "AZ",
                "NY",
                "MA",
                "NH",
                "VT",
                "OH"
            };

            var zip = new List<string>
            {
                "20001",
                "20002",
                "20003",
                "20004",
                "20005",
                "20006",
                "20007",
                "20008",
                "20009",
                "20010",
                "20011",
                "20012"
            };

            var permutations = new List<Tuple<int, int, int, int>>();

            var random = new Random();
            int a, b, c, d;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, address1.Count);
                b = random.Next(0, address1.Count);
                c = random.Next(0, address1.Count);
                d = random.Next(0, address1.Count);

                var tuple = new Tuple<int, int, int, int>(a, b, c, d);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string, string, string, string>(address1[tuple.Item1], city[tuple.Item2],
                            state[tuple.Item3],
                            zip[tuple.Item4])).ToList();
        }


        /// <summary>
        ///     Generate random client names
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomClientName(int count)
        {
            var names = new List<string>
            {
                "Wushu Inc",
                "Lanton Inc",
                "Rubio Inc",
                "Cluster Inc",
                "Indira Company",
                "Saul Trading",
                "Bernard LLC",
                "Danny & Partners",
                "Dimas Inc",
                "Yuri & Pavalov",
                "Ivan & Drako",
                "Laura Plc"
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random school names
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomSchoolName(int count)
        {
            var names = new List<string>
            {
                "Tapia School",
                "Gutierrez School",
                "Rueda School",
                "Galviz Martial Arts",
                "Yuli Wushu",
                "Rivera School",
                "Mamami Kung-fu",
                "Saucedo Taek-Won-Do",
                "Dominguez MMA",
                "Escobar Martial Arts",
                "Martin School",
                "Crespo Kickboxing"
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random sku names
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomSkuName(int count)
        {
            var names = new List<string>
            {
                "Martial Arts Book",
                "Red Belt",
                "Face Mask",
                "Boxing Gloves",
                "XXL Uniform",
                "Water Bottle"
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random titles
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomTitles(int count)
        {
            var names = new List<string>
            {
                "CEO",
                "CFO",
                "Manager",
                "Sales Executive",
                "Director",
                "Owner",
                "Finance Director",
                "CTO",
                "Developer",
                "Marketing Manager"
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random titles
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomSuffix(int count)
        {
            var names = new List<string>
            {
                "Ph.D",
                "Sr.",
                "Jr.",
                "C.B.E"
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random titles
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomPrefix(int count)
        {
            var names = new List<string>
            {
                "Mr.",
                "Mrs.",
                "Ms",
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate Random Telephone number
        /// </summary>
        /// <returns></returns>
        private static string GetRandomTelNumber()
        {
            Random rand = new Random();
            StringBuilder telNo = new StringBuilder(12);
            int number;
            for (int i = 0; i < 3; i++)
            {
                number = rand.Next(0, 8); // digit between 0 (incl) and 8 (excl)
                telNo = telNo.Append(number.ToString());
            }
            telNo = telNo.Append("-");
            number = rand.Next(0, 743); // number between 0 (incl) and 743 (excl)
            telNo = telNo.Append($"{number:D3}");
            telNo = telNo.Append("-");
            number = rand.Next(0, 10000); // number between 0 (incl) and 10000 (excl)
            telNo = telNo.Append($"{number:D4}");
            return telNo.ToString();
        }

        /// <summary>
        ///     Generate random titles
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomPhone(int count)
        {
            var names = new List<string>();
            for (int i = 0; i <= 9; i++)
            {
                names.Add(GetRandomTelNumber());
            }

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random titles
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomUrl(int count)
        {
            var names = new List<string>
            {
                "http://www.cnn.com",
                "http://www.bbc.com",
                "http://www.google.com",
                "http://www.msnbc.com",
                "http://www.reuters.com",
                "http://www.rt.com",
                "http://www.theguardian.com",
                "http://www.microsoft.com",
                "http://www.apple.com",
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        /// <summary>
        ///     Generate random titles
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Tuple<string>> GenerateRandomDepartment(int count)
        {
            var names = new List<string>
            {
                "HR",
                "Finance",
                "Sales",
                "Marketing",
                "IT",
                "Operations"
            };

            var permutations = new List<Tuple<int>>();

            var random = new Random();
            int a;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, names.Count);

                var tuple = new Tuple<int>(a);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string>(names[tuple.Item1])).ToList();

        }

        // Method to generate random names
        public static List<Tuple<string, string, string>> GenerateRandomName(int count)
        {
            var firstNames = new List<string>
            {
                "Sergio",
                "Daniel",
                "Carolina",
                "David",
                "Reina",
                "Saul",
                "Bernard",
                "Danny",
                "Dimas",
                "Yuri",
                "Ivan",
                "Laura"
            };

            var middleNames = new List<string>
            {
                "Tapia",
                "Gutierrez",
                "Rueda",
                "Galviz",
                "Yuli",
                "Rivera",
                "Mamami",
                "Saucedo",
                "Dominguez",
                "Escobar",
                "Martin",
                "Crespo"
            };

            var lastNames = new List<string>
            {
                "Johnson",
                "Williams",
                "Jones",
                "Brown",
                "David",
                "Miller",
                "Wilson",
                "Anderson",
                "Thomas",
                "Jackson",
                "White",
                "Robinson"
            };

            var permutations = new List<Tuple<int, int, int>>();

            var random = new Random();
            int a, b, c;

            // Generate names.
            while (permutations.Count < count)
            {
                a = random.Next(0, firstNames.Count);
                b = random.Next(0, firstNames.Count);
                c = random.Next(0, firstNames.Count);

                var tuple = new Tuple<int, int, int>(a, b, c);

                if (!permutations.Contains(tuple))
                    permutations.Add(tuple);
            }

            return
                permutations.Select(
                    tuple =>
                        new Tuple<string, string, string>(firstNames[tuple.Item1], middleNames[tuple.Item2],
                            lastNames[tuple.Item3])).ToList();
        }

        /// <summary>
        ///     Generate random string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[rand.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        /// <summary>
        ///     Generate a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword()
        {
            var requiredLength = 8;
            var requiredUniqueChars = 4;
            var requireDigit = true;
            var requireLowercase = true;
            var requireNonAlphanumeric = true;
            var requireUppercase = true;

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (requireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (requireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (requireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (requireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < requiredLength
                                      || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
