using System;

namespace PizzerianLab3.DTOs.Order
{
    public class UpdateOrderStatusDTO
    {
        public Guid OrderId { get; set; }
        public bool Completed { get; set; }
    }
}