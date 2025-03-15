using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessingApp.Domain.Entities;
using OrderProcessingApp.Domain.Enums;
using OrderProcessingApp.Domain.Services;

namespace OrderProcessingApp.ConsoleUI
{
    public static class ConsoleMenuActions
    {
        private static bool CorrectProductName(string productName)
        {
            return !string.IsNullOrWhiteSpace(productName);
        }

        private static bool CorrectPrice(string price)
        {

            if (double.TryParse(price, out _))
            {
                if (Convert.ToDouble(price) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // 1. Dodawanie Produktów
        public static void DodajZamówienie(OrderService orderService)
        {
            Order newOrder = new Order();

            Console.WriteLine("Podaj nazwę produktu:");
            string nazwa = Console.ReadLine();
            while (!CorrectProductName(nazwa))
            {
                Console.WriteLine("Nazwa produktu nie może być pusta. Spróbuj ponownie:");
                nazwa = Console.ReadLine();
            }
            newOrder.ProductName = nazwa;

            Console.WriteLine("Podaj cenę produktu:");
            string cena = Console.ReadLine();
            while (!CorrectPrice(cena))
            {
                Console.WriteLine("Cena produktu musi być liczbą. Spróbuj ponownie:");
                cena = Console.ReadLine();
            }
            newOrder.Price = Convert.ToDecimal(cena);

            Console.WriteLine("Podaj typ klienta:");
            Console.WriteLine("1. Firma");
            Console.WriteLine("2. Osoba Fizyczna");

            string typKlienta = Console.ReadLine();
            switch (typKlienta)
            {
                case "1":
                    newOrder.CustomerType = CustomerType.Firma;
                    break;
                case "2":
                    newOrder.CustomerType = CustomerType.OsobaFizyczna;
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór");
                    break;
            }

            Console.WriteLine("Podaj metodę płatności:");
            Console.WriteLine("1. Gotówka przy odbiorze");
            Console.WriteLine("2. Karta");
            string paymentMethod = Console.ReadLine();
            switch (paymentMethod)
            {
                case "1":
                    newOrder.PaymentMethod = PaymentMethod.GotówkaPrzyOdbiorze;
                    break;
                case "2":
                    newOrder.PaymentMethod = PaymentMethod.Karta;
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór");
                    break;
            }

            Console.WriteLine("Podaj adres dostawy (miejscowość, ulica, nr domu).");
            Console.Write("Miejscowość: ");
            string miejscowosc = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(miejscowosc))
            {
                newOrder.AdresDostawy = null;
                newOrder.OrderStatus = OrderStatus.Błąd;
            }
            else
            {
                Console.Write("Ulica: ");
                string ulica = Console.ReadLine();

                Console.Write("Numer domu: ");
                string numerDomu = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(numerDomu))
                {
                    newOrder.AdresDostawy = null;
                    newOrder.OrderStatus = OrderStatus.Błąd;
                }
                else
                {
                    newOrder.AdresDostawy = $"{miejscowosc}, ul. {ulica} {numerDomu}";
                }
            }

            newOrder.OrderDate = DateTime.Now;


            orderService.AddOrder(newOrder);

            Console.WriteLine($"Dodano zamówienie. ID: {newOrder.Id}, Status: {newOrder.OrderStatus}");
        }
        // 2. Przekazanie Do Magazynu
        public static void PrzekazanieDoMagazynu(OrderService orderService)
        { 
        Console.WriteLine("\nZamówienia w statusie Nowe:");
            orderService.ViewNewOrders(); 

            Console.WriteLine("Wybierz ID zamówienia, które chcesz przekazać do Magazynu:");
            string idstring2 = Console.ReadLine();

            if (int.TryParse(idstring2, out int newid))
            {
               
                orderService.SendToWarehouse(newid);
            }
            else
            {
                Console.WriteLine("Niepoprawne ID.");
            }
        }
        // 3. Przekazanie Do Wysyłki
        public static void PrzekazanieDoWysyłki(OrderService orderService)
        {
            Console.WriteLine("\nZamówienia w Magazynie:");
            orderService.ViewMagazinOrders();

            Console.WriteLine("Wybierz ID zamówienia, które chcesz przekazać do Wysyłki:");
            string idstring3 = Console.ReadLine();

            if (int.TryParse(idstring3, out int magid))
            {
                orderService.SendToShipping(magid);
            }
            else
            {
                Console.WriteLine("Niepoprawne ID.");
            }
        }

        // 4. Wyświetlenie Zamówień

        public static void WyswietlZamowienia(OrderService orderService)
        {
            orderService.ViewAllOrders();
        }

        // 5. Usuwanie Zamówień po ID
        public static void UsunZamowienie(OrderService orderService)
        {
            Console.WriteLine("\nWszystkie zamówienia:");
            orderService.ViewAllOrders();

            Console.WriteLine("Wybierz ID zamówienia, które chcesz usunąć:");
            string idstring = Console.ReadLine();

            if (int.TryParse(idstring, out int id))
            {
                orderService.RemoveOrder(id);
            }
            else
            {
                Console.WriteLine("Niepoprawny numer zamówienia.");
            }
        }

        // 6. Wyświetlenie Szczegółów Zamówienia po ID
        public static void WyswietlSzczegolyZamowienia(OrderService orderService)
        {
            Console.WriteLine("\nWybierz ID zamówienia, o którym chcesz zobaczyć informacje:");
            string idstring = Console.ReadLine();

            if (int.TryParse(idstring, out int id))
            {

                orderService.ViewOrderDetails(id);
            }
            else
            {
                Console.WriteLine("Niepoprawny numer zamówienia.");
            }
        }
    }
}
