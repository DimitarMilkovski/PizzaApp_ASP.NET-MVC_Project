using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PizzaAppRefactored.Domain.Enums;
using PizzaAppRefactored.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppRefactored.DataAccess
{
    
    public class PizzaAppDbContext : DbContext
    {
        
        public PizzaAppDbContext(DbContextOptions options) : base(options) { }  

        
        public DbSet<Pizza> Pizzas { get; set; }    
        public DbSet<Order> Orders { get; set; }    
        public DbSet<User> Users { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //define relations

            modelBuilder.Entity<Pizza>()
                .HasMany(x => x.PizzaOrders) 
                .WithOne(x => x.Pizza) 
                .HasForeignKey(x => x.PizzaId);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.PizzaOrders)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
           

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders) 
                .WithOne(x => x.User) 
                .HasForeignKey(x => x.UserId); 

            modelBuilder.Entity<Pizza>()
                .HasData(new Pizza
                {

                    Id = 1,
                    Name = "Capricciosa",
                    IsOnPromotion = true
                },
                new Pizza
                {
                    Id = 2,
                    Name = "Pepperoni",
                    IsOnPromotion = false 
                }
                );

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    FirstName = "Tijana",
                    LastName = "Stojanovska",
                    Address = "Address1",
                },
                 new User
                 {
                     Id = 2,
                     FirstName = "Aleksandar",
                     LastName = "Ivanovski",
                     Address = "Address2",
                 }
                );

            modelBuilder.Entity<Order>()
                .HasData(
                 new Order
                 {
                     Id = 1,
                     PizzaStore = "Jakomo",
                     IsDelivered = false,
                     PaymentMethod = PaymentMethodEnum.Card,
                     UserId = 1
                 },

                 new Order
                 {
                     Id = 2,
                     PizzaStore = "Jakomo",
                     IsDelivered = false,
                     PaymentMethod = PaymentMethodEnum.Cash,
                     UserId = 2
                 }

                );

            modelBuilder.Entity<PizzaOrder>()
                .HasData(
                new PizzaOrder
                {
                    Id = 1,
                    PizzaId = 1,
                    Price = 350,
                    Quantity = 1,
                    OrderId = 2,
                    PizzaSize = PizzaSizeEnum.Standard
                },
                new PizzaOrder
                {
                    Id = 2,
                    PizzaId = 2,
                    Price = 300,
                    Quantity = 1,
                    OrderId = 1,
                    PizzaSize = PizzaSizeEnum.Standard
                }
                );

        }
    }
}
