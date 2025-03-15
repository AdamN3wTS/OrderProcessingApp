using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrderProcessingApp.Domain.Entities;
using OrderProcessingApp.Domain.Enums;
using OrderProcessingApp.Domain.Services;
using OrderProcessingApp.Repositories;

namespace OrderProcessingApp.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private InMemoryOrderRepository repository;
        private OrderService service;

        [SetUp]
        public void Setup()
        {
            
            repository = new InMemoryOrderRepository();
            service = new OrderService(repository);
        }



        [Test]
        public void AddOrder_ShouldAddOrderToRepository()
        {
            
            var order = new Order("Produkt", 942m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");

            
            service.AddOrder(order);

            
            Assert.AreEqual(1, repository.Orders.Count);
            Assert.AreEqual("Produkt", repository.Orders[0].ProductName);
        }

        [Test]
        public void RemoveOrder_ShouldRemoveOrderFromRepository()
        {
            
            var order = new Order("Produkt", 55m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");
            service.AddOrder(order);

            
            service.RemoveOrder(1);

            
            Assert.AreEqual(0, repository.Orders.Count);
        }

        [Test]
        public void SendToWarehouse_ShouldSetStatusToZwróconoDoKlienta_ForHighPriceCashOnDelivery()
        {
            
            var order = new Order("Produkt", 2501m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");
            service.AddOrder(order);

            
            service.SendToWarehouse(1);

            
            Assert.AreEqual(OrderStatus.ZwróconoDoKlienta, repository.GetOrderById(1).OrderStatus);
        }

        [Test]
        public void SendToWarehouse_ShouldSetStatusToWMagazynie_ForOtherCases()
        {
            
            var order = new Order("Produkt", 2449m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");
            service.AddOrder(order);

            
            service.SendToWarehouse(1);

            
            Assert.AreEqual(OrderStatus.WMagazynie, repository.GetOrderById(1).OrderStatus);
        }

        [Test]
        public void SendToWarehouse_ShouldNotChangeStatus_WhenOrderDoesNotExist()
        {
            
            Assert.DoesNotThrow(() => service.SendToWarehouse(999));
        }

        [Test]
        public void SendToShipping_ShouldSetStatusToZamknięte_ForValidOrder()
        {
            
            var order = new Order("Produkt", 0.40m, PaymentMethod.GotówkaPrzyOdbiorze, CustomerType.Firma, "Adres");
            service.AddOrder(order);
            var addedOrder = repository.GetOrderById(1);
            addedOrder.OrderStatus = OrderStatus.WMagazynie;

            
            service.SendToShipping(1);

            
            Assert.AreEqual(OrderStatus.Zamknięte, repository.GetOrderById(1).OrderStatus);
        }

        [Test]
        public void SendToShipping_ShouldNotChangeStatus_WhenOrderDoesNotExist()
        {
            
            Assert.DoesNotThrow(() => service.SendToShipping(999));
        }
    }
}
