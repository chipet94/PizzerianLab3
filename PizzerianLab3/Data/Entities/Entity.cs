using System;

namespace PizzerianLab3.Data.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
}