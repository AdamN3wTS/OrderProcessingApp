using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessingApp.Domain.Enums;

namespace OrderProcessingApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public CustomerType CustomerType { get; set; }
        public string AdresDostawy { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            OrderStatus = OrderStatus.Nowe;
        }

        public Order(string productName,
                     decimal price,
                     PaymentMethod paymentMethod,
                     CustomerType customerType,
                     string adresDostawy)
        {
            ProductName = productName;
            Price = price;
            OrderDate = DateTime.Now;
            PaymentMethod = paymentMethod;
            CustomerType = customerType;
            if (string.IsNullOrEmpty(adresDostawy))
            {
                OrderStatus = OrderStatus.Błąd;
            }
            else
            {
                AdresDostawy = adresDostawy;
                OrderStatus = OrderStatus.Nowe;
            }
        }
    }
}
