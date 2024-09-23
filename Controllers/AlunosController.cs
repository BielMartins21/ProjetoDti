using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDti.Models;

namespace ProjetoDti.Controllers
{
    public class AlunosController : Controller
    {
        private readonly CustomDbContext _context;

        public AlunosController(CustomDbContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            List<Aluno> alunos = await _context.Alunos.ToListAsync();
            foreach (Aluno aluno in alunos)
            {
                aluno.MediaNotas = CalcularMediaNotas(aluno);
            }
            return View(alunos);
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aluno aluno = await _context.Alunos.FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.MediaNotas = CalcularMediaNotas(aluno);
            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Nota1,Nota2,Nota3,Nota4,Nota5,Frequencia")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.MediaNotas = CalcularMediaNotas(aluno);
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aluno aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Nota1,Nota2,Nota3,Nota4,Nota5,Frequencia")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    aluno.MediaNotas = CalcularMediaNotas(aluno);
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aluno aluno = await _context.Alunos.FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Aluno aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AlunoExists(int id)
        {
            return await _context.Alunos.AnyAsync(e => e.Id == id);
        }

        // Método para calcular a média das notas
        private double CalcularMediaNotas(Aluno aluno)
        {
            var notas = new List<double> { aluno.Nota1, aluno.Nota2, aluno.Nota3, aluno.Nota4, aluno.Nota5 };
            return notas.Count(n => n >= 0) > 0 ? notas.Average() : 0; // Evita divisão por zero e considera notas não negativas
        }
    }
}
