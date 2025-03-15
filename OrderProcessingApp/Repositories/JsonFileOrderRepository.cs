using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrderProcessingApp.Domain.Entities;
namespace OrderProcessingApp.Repositories
{
    public class JsonFileOrderRepository
    {
        
        public void SaveToFile(string filePath,InMemoryOrderRepository repository)
        {
            var json = JsonConvert.SerializeObject(repository.Orders);
            File.WriteAllText(filePath, json);
        }
        public void LoadFromFile(string filePath, InMemoryOrderRepository repository)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var orders = JsonConvert.DeserializeObject<List<Order>>(json);
                foreach (var order in orders)
                {
                    repository.AddOrder(order);
                }
            }
        }
    }
}
