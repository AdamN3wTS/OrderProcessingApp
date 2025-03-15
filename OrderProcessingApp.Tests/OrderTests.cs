using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrderProcessingApp.Domain.Entities;
using OrderProcessingApp.Domain.Enums;

namespace OrderProcessingApp.Tests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void DefaultConstructor_ShouldSetOrderDateToNowAndStatusToNew()
        {
            
            var order = new Order();

            
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Nowe),
                "Konstruktor powinien ustawiać status 'Nowe'");

            
            var now = DateTime.Now;
            Assert.That((now - order.OrderDate).TotalSeconds, Is.LessThan(2),
                "OrderDate powinno być ustawione na DateTime.Now");
        }

        [Test]
        public void ParamConstructor_WithValidData_ShouldInitializeCorrectly()
        {
            
            string productName = "Produkt";
            decimal price = 1500m;
            PaymentMethod paymentMethod = PaymentMethod.GotówkaPrzyOdbiorze;
            CustomerType customerType = CustomerType.Firma;
            string adresDostawy = "Adres";

            
            var order = new Order(productName, price, paymentMethod, customerType, adresDostawy);

            
            Assert.That(order.ProductName, Is.EqualTo(productName));
            Assert.That(order.Price, Is.EqualTo(price));
            Assert.That(order.PaymentMethod, Is.EqualTo(paymentMethod));
            Assert.That(order.CustomerType, Is.EqualTo(customerType));
            Assert.That(order.AdresDostawy, Is.EqualTo(adresDostawy));
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Nowe),
                "Dla poprawnego adresu dostawy status powinien być 'Nowe'.");

            
            var now = DateTime.Now;
            Assert.That((now - order.OrderDate).TotalSeconds, Is.LessThan(2));
        }

        [Test]
        public void ParamConstructor_WithEmptyAdresDostawy_ShouldSetOrderStatusToBłąd()
        {
            
            string emptyAddress = "";

            
            var order = new Order("Produkt", 123m, PaymentMethod.Karta, CustomerType.OsobaFizyczna, emptyAddress);

            
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Błąd),
                "Jeśli adres pusty => status powinien 'Błąd'.");
        }

        [Test]
        public void ParamConstructor_WithNullAdresDostawy_ShouldSetOrderStatusToBłąd()
        {
            
            string nullAddress = null;

            
            var order = new Order("Produkt", 123m, PaymentMethod.Karta, CustomerType.OsobaFizyczna, nullAddress);

            
            Assert.That(order.OrderStatus, Is.EqualTo(OrderStatus.Błąd),
                "Jeśli adres null => status powinien 'Błąd'.");
        
        }
    }
}
