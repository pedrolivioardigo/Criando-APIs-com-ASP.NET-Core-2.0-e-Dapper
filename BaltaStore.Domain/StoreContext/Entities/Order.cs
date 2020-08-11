using BaltaStore.Domain.StoreContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaltaStore.Domain.StoreContext.Entities
{
    public class Order
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;

        public Order(Customer customer)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();

        }
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime CreateDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public IReadOnlyCollection<Delivery> Deliveries => _deliveries.ToArray();


        public void AddItem(OrderItem item)
        {
            // Valida item
            //Acidiona ao pedido
            _items.Add(item);
        }


        public void Place() //Criar um pedido
        {
            //gera o numero do pedido
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
            //validar
        }

        public void Pay()// pagar um pedido
        {
            Status = EOrderStatus.Paid;            
        }


        public void Ship() // enviar um pedido
        {
            //a cada 5 produtor  uma entrega
            var deliveries = new List<Delivery>();
            deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
            var count = 1;

            //quebra as entregas de 5 em 5
            foreach (var item in _items)
            {
                if(count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
                count++;
            }

            // envia as entregas
            deliveries.ForEach(x => x.Ship());

            //adiciona as entregas ao pedido
            deliveries.ForEach(x => _deliveries.Add(x));

        }

        //cancelar pedido
        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
            _deliveries.ToList().ForEach(X => X.Cancel());
        }







    }
}