using System.Reflection;
using AutoglassAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoglassAPI.Repository
{
    public class AutoglassContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Fornecedor> Fornecedores { get; set; } = null!;

        public AutoglassContext(DbContextOptions<AutoglassContext> options) : base(options)
        {
        }

    }
}