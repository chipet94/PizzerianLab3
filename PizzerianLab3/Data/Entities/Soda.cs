using System;

namespace PizzerianLab3.Data.Entities
{
    public class Soda
    {
        public Guid Id { get; set; }
        public int MenuNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}