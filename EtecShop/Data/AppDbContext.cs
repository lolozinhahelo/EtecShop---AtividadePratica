using EtecShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EtecShop.Data;
public class AppDbContext : IdentityDbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{ 
}
public DbSet<Avaliacao> Avaliacoes { get; set; }
public DbSet<Categoria> Categorias { get; set; }
public DbSet<Produto> Produtos { get; set; }
protected override void OnModelCreating(ModelBuilder builder)
{
base.OnModelCreating(builder);
#region Populando os dados da Gestão de Usuários
List<IdentityRole> roles = new()
{
new IdentityRole()

{
Id = Guid.NewGuid().ToString(),
Name = "Administrador",
NormalizedName = "ADMINISTRADOR"
},
new IdentityRole()
{
Id = Guid.NewGuid().ToString(),
Name = "Funcionário",
NormalizedName = "FUNCIONÁRIO"
},
new IdentityRole()
{
Id = Guid.NewGuid().ToString(),
Name = "Cliente",
NormalizedName = "CLIENTE"
}
};
builder.Entity<IdentityRole>().HasData(roles);
IdentityUser user = new()
{
Id = Guid.NewGuid().ToString(),
Email = "admin@etecshop.com",
NormalizedEmail = "ADMIN@ETECSHOP.COM",
UserName = "Admin",
NormalizedUserName = "ADMIN",
LockoutEnabled = true,
EmailConfirmed = true,
};
PasswordHasher<IdentityUser> pass = new();
user.PasswordHash = pass.HashPassword(user, "@Etec123");
builder.Entity<IdentityUser>().HasData(user);
List<IdentityUserRole<string>> userRoles = new()
{
new IdentityUserRole<string>() {
UserId = user.Id,
RoleId = roles[0].Id
},
new IdentityUserRole<string>() {
UserId = user.Id,
RoleId = roles[1].Id
},
new IdentityUserRole<string>() {
UserId = user.Id,
RoleId = roles[2].Id
}};
builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
#endregion
}
}