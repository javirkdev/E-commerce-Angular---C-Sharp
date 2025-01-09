using Microsoft.EntityFrameworkCore;
using TuringClothes.Database;
using TuringClothes.Repository;

namespace TuringClothes.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public MyBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
                    var myDatabase = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<MyDatabase>();
                    var productRepository = _serviceProvider.CreateScope().ServiceProvider.GetService<ProductRepository>();
                    var temporaryOrderRepository = _serviceProvider.CreateScope().ServiceProvider.GetService<TemporaryOrderRepository>();
 
                    var expiredOrders = myDatabase.TemporaryOrders.Where(o => o.ExpirationTime < DateTime.UtcNow).ToList();

                    if (expiredOrders.Any())
                    {
                        Console.WriteLine("Borrando órdenes temporales expiradas...");
                        foreach (var expiredOrder in expiredOrders)
                        {
                            var expiredDetails = await temporaryOrderRepository.GetTemporaryOrder(expiredOrder.Id);
                            foreach (var item in expiredDetails.Details)
                            {
                                Product product = await productRepository.GetProductById(item.ProductID);
                                product.Stock += item.Amount;
                                myDatabase.Products.Update(product);
                            }
                        }

                        myDatabase.RemoveRange(expiredOrders);
                      
                        await myDatabase.SaveChangesAsync(stoppingToken);
                    }

                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Error al borrar órdenes temporales expiradas.");
                }
                
            }


        }
    }
}
