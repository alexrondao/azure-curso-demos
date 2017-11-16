using CadCli.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CadCli.Data
{
    public class CadCliDataContext:DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _conn;

        public CadCliDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CadCliDataContext()
        {
            _conn = @"Server=(localdb)\\MSSQLLocalDB;Database=CadCliDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CadCliDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            if (string.IsNullOrEmpty(_conn))
            {
                optionsBuilder
                    .UseSqlServer(
                        _configuration.GetConnectionString("DefaultConnection"),
                        opt =>
                        {
                            opt.EnableRetryOnFailure();
                        }
                    );
            }
            else
            {
                optionsBuilder.UseSqlServer(_conn);
            }
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

    }
}
