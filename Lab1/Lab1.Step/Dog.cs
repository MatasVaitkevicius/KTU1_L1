using System;

namespace Lab1.Step
{
    class Dog
    {
        private const int VaccinationDuration = 1;

        public string Name { get; set; }
        public int ChipId { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public DateTime VaccinationDate { get; set; }
        public bool Aggressive { get; set; }

        public bool IsVaccinationExpired()
        {
            return VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) < 0;
        }
    }
}
