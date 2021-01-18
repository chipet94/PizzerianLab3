using PizzerianLab3.Data.Entities;

namespace PizzerianLab3.Singletons
{
    public class CartSingleton
    {
        public Order Order;

        public CartSingleton()
        {
            Order = new Order();
        }
    }
}