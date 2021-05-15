using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class BluDataContext : DbContext
    {
        public BluDataContext(DbContextOptions options) : base(options)
        {
        }
        public BluDataContext() : base ()
        {
        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost\MSSQLSERVER01;Integrated Security=true;Initial Catalog=BluData");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>().HasMany(e => e.Telefones);
            
            modelBuilder.Entity<Fornecedor>().Navigation(b => b.Telefones).
            UsePropertyAccessMode(PropertyAccessMode.Property);

            base.OnModelCreating(modelBuilder);
        }
    }
}
