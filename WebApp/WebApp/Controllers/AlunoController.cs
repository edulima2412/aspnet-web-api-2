using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*","*","*")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<Alunos> Get()
        {
            Alunos aluno = new Alunos();
            return aluno.ListarAlunos();
        }

        // GET: api/Aluno/5
        public Alunos Get(int id)
        {
            Alunos aluno = new Alunos();
            return aluno.ListarAlunos().Where(x => x.id == id).FirstOrDefault();
        }

        // POST: api/Aluno
        public List<Alunos> Post([FromBody]Alunos aluno)
        {
            Alunos _aluno = new Alunos();

            _aluno.Salvar(aluno);

            return _aluno.ListarAlunos();

        }

        // PUT: api/Aluno/5
        public Alunos Put(int id, [FromBody] Alunos aluno)
        {
            Alunos _aluno = new Alunos();
            _aluno.Atualizar(id, aluno);
            return aluno;
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Alunos _aluno = new Alunos();

            _aluno.Deletar(id);
        }
    }
}
