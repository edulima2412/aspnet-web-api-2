using App.Domain;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        [HttpGet]
        [Route("Recuperar")]
        [Authorize]
        public IHttpActionResult Recuperar()
        {
            try
            {
                AlunoModel aluno = new AlunoModel();
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Recuperar/{id?}")]
        public IHttpActionResult Recuperar(int id)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                var resultado = aluno.ListarAlunos(id);

                if(!resultado.Any())
                {
                    return NotFound();
                } else
                {
                    return Ok(aluno.ListarAlunos(id));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route(@"RecuperarPorData/{data:regex([0-9]{4}\-[0-9]{2}\-[0-9]{2})}")]
        public IHttpActionResult RecuperarPorData(string data)
        {
            try
            {
                AlunoModel aluno = new AlunoModel();

                var alunos = aluno.ListarAlunos().Where(x => x.data == data);

                if(!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Adicionar([FromBody] AlunoDTO aluno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                AlunoModel _aluno = new AlunoModel();

                _aluno.Inserir(aluno);

                return Ok(_aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Atualizar(int id, [FromBody] AlunoDTO aluno)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();

                var alunoExiste = _aluno.ListarAlunos().FirstOrDefault(a => a.id == id);

                if(alunoExiste == null)
                {
                    return NotFound();
                } else
                {
                    aluno.id = id;
                    _aluno.Atualizar(aluno);

                    return Ok(_aluno.ListarAlunos(id));
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Deletar(int id)
        {
            try
            {
                AlunoModel _aluno = new AlunoModel();

                var aluno = _aluno.ListarAlunos().FirstOrDefault(a => a.id == id);

                if(aluno == null)
                {
                    return NotFound();
                } else
                {
                    _aluno.Deletar(id);
                    return Ok("Deletado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
