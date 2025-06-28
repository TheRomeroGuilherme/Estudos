using GerenciadorDeTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.API.Data
{
    public class TarefasContext : DbContext
    {
        public TarefasContext(DbContextOptions<TarefasContext> options) : base(options)
        {
        }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}