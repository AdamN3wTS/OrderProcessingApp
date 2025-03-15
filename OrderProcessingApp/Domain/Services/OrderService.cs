using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessingApp.Domain.Entities;
using OrderProcessingApp.Domain.Enums;
using OrderProcessingApp.Repositories;
using DotNetEnv;

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
        public void ViewOrderDetails(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order == null)
            {
                Console.WriteLine($"Zamówienie ID {orderId} nie istnieje.");
                return;
            }

            Console.WriteLine($"ID: {order.Id}");
            Console.WriteLine($"Nazwa Produktu: {order.ProductName}");
            Console.WriteLine($"Cena Produktu: {order.Price} PLN");
            Console.WriteLine($"Typ Klienta: {order.CustomerType}");
            Console.WriteLine($"Adres Dostawy: {order.AdresDostawy}");
            Console.WriteLine($"Metoda Płatności: {order.PaymentMethod}");
            Console.WriteLine($"Status Zamówienia: {order.OrderStatus}");
            Console.WriteLine($"Data złożenia Zamówienia: {order.OrderDate}");
        }
        public void ViewNewOrders()
        {
            var all = _repository.GetAllOrders();
            var newOrders = all.Where(o => o.OrderStatus == OrderStatus.Nowe);
            if (newOrders.Count() == 0)
            {
                Console.WriteLine("Brak nowych zamówień.");
                return;
            }
            foreach (var o in newOrders)
            {
                Console.WriteLine($"ID: {o.Id}, {o.ProductName}, {o.OrderStatus}, Cena: {o.Price} PLN");
            }
        }

        public void ViewMagazinOrders()
        {
            var all = _repository.GetAllOrders();
            var magazynOrders = all.Where(o => o.OrderStatus == OrderStatus.WMagazynie);
            if (magazynOrders.Count() == 0)
            {
                Console.WriteLine("Brak zamówień w Magazynie.");
                return;
            }
            foreach (var o in magazynOrders)
            {
                Console.WriteLine($"ID: {o.Id}, {o.ProductName}, {o.OrderStatus}, Cena: {o.Price} PLN");
            }
        }

    }
}
