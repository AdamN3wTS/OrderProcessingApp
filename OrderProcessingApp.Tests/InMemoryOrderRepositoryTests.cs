using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrderProcessingApp.Domain.Entities;
using OrderProcessingApp.Domain.Enums;
using OrderProcessingApp.Repositories;

namespace OrderProcessingApp.Tests
{
    [TestFixture]
    public class InMemoryOrderRepositoryTests
    {
        private InMemoryOrderRepository repository = new InMemoryOrderRepository();

        
        
        [Test]
        public void AddOrder_ShouldAddOrderAndAssignId()
        {
            
            var order = new Order("Produkt", 24m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.OsobaFizyczna, "Adres");

            
            repository.AddOrder(order);

            
            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(1, repository.Orders.Count);
        }

        [Test]
        public void RemoveOrderById_ShouldRemoveOrderAndReassignIds()
        {
            
            var order1 = new Order("Produkt", 10m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");
            var order2 = new Order("Produkt2", 11m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres2");
            repository.AddOrder(order1);
            repository.AddOrder(order2);

            
            repository.RemoveOrderById(1);

            
            Assert.AreEqual(1, repository.Orders.Count);
            
            Assert.AreEqual(1, repository.Orders[0].Id);
            Assert.AreEqual("Produkt2", repository.Orders[0].ProductName);
        }

        [Test]
        public void GetOrderById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            var order = repository.GetOrderById(1);
            Assert.IsNull(order);
        }

        [Test]
        public void GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            
            var order = new Order("Produkt", 1320m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");
            repository.AddOrder(order);

            
            var fetchedOrder = repository.GetOrderById(1);

            
            Assert.IsNotNull(fetchedOrder);
            Assert.AreEqual("Produkt", fetchedOrder.ProductName);
        }
    }
}
