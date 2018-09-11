using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1.Step
{
    class Program
    {
        private static void Main(string[] args)
        {
            {                
                var dogs = ReadDogData();
                SaveReportToFile(dogs);

                Console.WriteLine("Kurio amžiaus agresyvius šunis skaičiuoti?");
                var age = int.Parse(Console.ReadLine());
                Console.WriteLine("Agresyvių šunų kiekis: " + CountAggressive(dogs, age));
                Console.WriteLine();

                Console.WriteLine("Kurios veislės šunis filtruoti?");
                var breedToFilter = Console.ReadLine();
                var filteredByBreed = FilterByBreed(dogs, breedToFilter);
                filteredByBreed.ForEach(d => Console.WriteLine(d.Name));
                Console.WriteLine();

                Console.WriteLine("Kurios veislės seniausius šunis surasti?");
                breedToFilter = Console.ReadLine();
                var oldestDogs = FindOldestDogs(dogs, breedToFilter);
                PrintDogsToConsole(oldestDogs);
                Console.WriteLine();

                
                Console.WriteLine("Skirtingos Veislės:");
                GetBreeds(dogs).ForEach(Console.WriteLine);


                Console.WriteLine("Vakcinos galiojimas baigėsi:");
                dogs.ForEach(d =>
                {
                    if (d.IsVaccinationExpired())
                    {
                        Console.WriteLine($"Vardas: {d.Name}, Šeimininkas: {d.Owner} Telefonas: {d.Phone} ");
                    }
                });

                Console.Read();
            }
        }

        private static void SaveReportToFile(List<Dog> dogs)
        {
            using (var writer = new StreamWriter(@"L1SavedData.csv"))
            {
                writer.WriteLine("Vardas;MikroId;Svoris;Amžius;Savininkas;Tel. Nr.;Vakcinacija;Agresyvumas");
                dogs.ForEach(d =>
                    writer.WriteLine($"{d.Name};{d.ChipId};{d.Weight};{d.Age};{d.Owner};{d.Phone};{d.VaccinationDate};{d.Aggressive}"));

            }
        }

        private static void PrintDogsToConsole(List<Dog> dogs)
        {
            dogs.ForEach(d => Console.WriteLine($"Vardas: {d.Name}\nMikroschemos ID: {d.ChipId}\nSvoris: {d.Weight}" +
                                                $"\nAmžius: {d.Age}\nSavininka: {d.Owner}\nTelefonas: {d.Phone}\nVakcinacijos data: {d.VaccinationDate}\nAgrsyvus: {d.Aggressive}\n"));
        }

        private static List<Dog> ReadDogData()
        {
            var dogs = new List<Dog>();

            using (var reader = new StreamReader(@"L1Data.csv"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var values = line.Split(';');
                    var name = values[0];
                    var chipId = int.Parse(values[1]);
                    var weight = Convert.ToDouble(values[2]);
                    var age = int.Parse(values[3]);
                    var breed = values[4];
                    var owner = values[5];
                    var phone = values[6];
                    var vaccinationDate = DateTime.Parse(values[7]);
                    var aggressive = bool.Parse(values[8]);

                    dogs.Add(new Dog
                    {

                        Name = name,
                        ChipId = chipId,
                        Weight = weight,
                        Age = age,
                        Breed = breed,
                        Owner = owner,
                        Phone = phone,
                        VaccinationDate = vaccinationDate,
                        Aggressive = aggressive
                    });

                    line = reader.ReadLine();
                }
            }
            return dogs;
        }

        private static List<string> GetBreeds(IEnumerable<Dog> dogs)
        {
            return dogs.Select(d => d.Breed).Distinct().ToList();
        }

        private static List<Dog> FilterByBreed(List<Dog> dogs, string breed)
        {
            return dogs.FindAll(d => d.Breed.Equals(breed)).ToList();
        }

        private static int CountAggressive(List<Dog> dogs, int age)
        {
            return dogs.Count(d => (d.Aggressive.Equals(true)) && (d.Age == age));
        }

        private static List<Dog> FindOldestDogs(List<Dog> dogs, string breed)
        {
            var filteredDogs = dogs.Where(d => d.Breed == breed).ToList();
            var maxAge = filteredDogs.Max(d => d.Age);
            return filteredDogs.Where(d => d.Age == maxAge).ToList();
        }

    }
}
