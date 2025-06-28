using GerenciadorDeTarefas.API.Data;
using GerenciadorDeTarefas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly TarefasContext _context;

        // O DbContext é injetado pelo sistema de injeção de dependência do .NET
        public TarefasController(TarefasContext context)
        {
            _context = context;
        }

        // READ: Obter todas as tarefas (GET /api/tarefas)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetAll()
        {
            var tarefas = await _context.Tarefas.ToListAsync();
            return Ok(tarefas);
        }

        // READ: Obter uma tarefa pelo ID (GET /api/tarefas/1)
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetById(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        // CREATE: Criar uma nova tarefa (POST /api/tarefas)
        [HttpPost]
        public async Task<ActionResult<Tarefa>> Create(Tarefa novaTarefa)
        {
            // O Id será gerado automaticamente pelo banco de dados
            _context.Tarefas.Add(novaTarefa);
            await _context.SaveChangesAsync(); // Salva as mudanças no banco

            return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
        }

        // UPDATE: Atualizar uma tarefa existente (PUT /api/tarefas/1)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tarefa tarefaAtualizada)
        {
            if (id != tarefaAtualizada.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarefaAtualizada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tarefas.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: Deletar uma tarefa (DELETE /api/tarefas/1)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}