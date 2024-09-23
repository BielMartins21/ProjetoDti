using Microsoft.AspNetCore.Mvc;
using ProjetoDti.Models;

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
        var alunos = await _context.Alunos.ToListAsync();
        foreach (var aluno in alunos)
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

        var aluno = await _context.Alunos.FirstOrDefaultAsync(m => m.Id == id);
        if (aluno == null)
        {
            return NotFound();
        }

        aluno.MediaNotas = CalcularMediaNotas(aluno);
        return View(aluno);
    }

    // Método para calcular a média das notas
    private double CalcularMediaNotas(Aluno aluno)
    {
        var notas = new List<double> { (double)aluno.Nota1, (double)aluno.Nota2, (double)aluno.Nota3, (double)aluno.Nota4, (double)aluno.Nota5 };
        return notas.Count(n => n > 0) > 0 ? notas.Average() : 0;
    }

    // Continue com os outros métodos: Create, Edit, Delete...
}
