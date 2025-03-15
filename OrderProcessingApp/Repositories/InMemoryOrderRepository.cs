using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessingApp.Domain.Entities;

namespace OrderProcessingApp.Repositories
{
    public class InMemoryOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();
        public IReadOnlyList<Order> Orders => _orders;

        public void AddOrder(Order order)
        {

            order.Id = _orders.Count + 1;
            _orders.Add(order);
        }

        public void RemoveOrderById(int id)
        {
            var order = GetOrderById(id);
            if (order != null)
            {
                _orders.Remove(order);


                for (int i = 0; i < _orders.Count; i++)
                {
                    _orders[i].Id = i + 1;
                }
            }
        }

        public Order GetOrderById(int id)
        {
            if (Orders.ElementAtOrDefault(id - 1) == null)
            {
                return null;
            }
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }
    }
}
