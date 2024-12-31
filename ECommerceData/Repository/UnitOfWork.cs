﻿using ECommerce.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category {  get; private set; }
        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderItemRepository OrderItem { get; private set; }

        public IShippingAddressRepository ShippingAddress { get; private set; }


        public ICartItemRepository CartItem { get; private set; }

        public IOrderItemStatusRepository OrderItemStatus { get; private set; }

        public ILikesRepository Likes { get; private set; }

        public ICompanySettingsRepository CompanySettings { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Cart = new CartRepository(_db);
            CartItem = new CartItemRepository(_db);
            Order = new OrderRepository(_db);
            OrderItem = new OrderItemRepository(_db);
            ShippingAddress = new ShippingAddressRepositoy(_db);
            OrderItemStatus = new OrderItemStatusRepository(_db);
            Likes = new LikesRepository(_db);
            CompanySettings = new CompanySettingsRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
