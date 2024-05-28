﻿using EcommerceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EcommerceApp.Data
{
    public class MvcContext : DbContext
    {
        public MvcContext(DbContextOptions<MvcContext> options) : base(options)
        { }
        public DbSet<Item> Items { get; set; }

    }
}
