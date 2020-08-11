using FluentValidator;
using System.Collections.Generic;

namespace BaltaStore.Domain.StoreContext.Entities
{
    public class OrderItem : Notifiable
    {

        public OrderItem(Product product, decimal quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = product.Price;

            if (Product.QuantityOnHand < quantity)
            {
                AddNotification("Quantidade", "Produto fora de estoque");
            }

        }


        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
        



    }
}