using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessingApp.Domain.Entities;
using OrderProcessingApp.Domain.Enums;
using OrderProcessingApp.Repositories;

namespace OrderProcessingApp.Domain.Services
{
    public class OrderService
    {
        private readonly InMemoryOrderRepository _repository;

        public OrderService(InMemoryOrderRepository repository)
        {
            _repository = repository;
        }

        public void AddOrder(Order order)
        {

            _repository.AddOrder(order);
        }

        public void RemoveOrder(int orderId)
        {

            _repository.RemoveOrderById(orderId);
        }

        public void SendToWarehouse(int orderId)
        {

            var order = _repository.GetOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine($"Zamówienie o ID {orderId} nie istnieje.");
                return;
            }
            if (order.OrderStatus != OrderStatus.Nowe)
            {
                Console.WriteLine($"Zamówienie o ID {orderId} nie jest w statusie Nowe.");
                return;
            }

            if (order.Price >= 2500 && order.PaymentMethod == PaymentMethod.GotówkaPrzyOdbiorze)
            {
                Task.Delay(5000);

                order.OrderStatus = OrderStatus.ZwróconoDoKlienta;
                Console.WriteLine($"ALERT: Zamówienie ID:{orderId} zostało zwrócone do klienta.");
            }
            else
            {
                Task.Delay(5000);

                order.OrderStatus = OrderStatus.WMagazynie;
                Console.WriteLine($"ALERT: Zamówienie ID:{orderId} przekazane do Magazynu.");
            }
        }
        public void SendToShipping(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine($"Zamówienie o ID {orderId} nie istnieje.");
                return;
            }
            if (order.OrderStatus != OrderStatus.WMagazynie)
            {
                Console.WriteLine($"Zamówienie o ID {orderId} nie jest w statusie W Magazynie.");
                return;
            }

            Task.Delay(5000);
            order.OrderStatus = OrderStatus.WWysyłce;
            Console.WriteLine($"ALERT: Zamówienie ID: {orderId} przekazane do Wysyłki");

            Task.Delay(5000);
            order.OrderStatus = OrderStatus.Zamknięte;
            Console.WriteLine($"ALERT: Zamówienie ID: {orderId} dostarczone");
        }


        public void ViewAllOrders()
        {
            var all = _repository.GetAllOrders();
            foreach (var o in all)
            {
                Console.WriteLine($"ID: {o.Id}, {o.ProductName}, {o.OrderStatus}, Cena: {o.Price} PLN");
            }
        }
        
    }
}
