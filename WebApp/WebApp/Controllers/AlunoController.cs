using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/alunos")]
    public class AlunoController : ApiController
    {
        [HttpGet]
        [Route("Recuperar")]
        public IHttpActionResult Recuperar()
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Recuperar/{id}")]
        public IHttpActionResult Recuperar(int id)
        {
            try
            {
                Aluno aluno = new Aluno();
                return Ok(aluno.ListarAlunos().Where(x => x.id == id).FirstOrDefault());
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
                Aluno aluno = new Aluno();

                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.data == data);

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
        [Route("Adicionar")]
        public IHttpActionResult Adicionar([FromBody] Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();

                _aluno.Salvar(aluno);

                return Ok(_aluno.ListarAlunos());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("Atualizar/{id}")]
        public IHttpActionResult Atualizar(int id, [FromBody] Aluno aluno)
        {
            try
            {
                Aluno _aluno = new Aluno();
                _aluno.Atualizar(id, aluno);
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IHttpActionResult Deletar(int id)
        {
            try
            {
                Aluno _aluno = new Aluno();
                return Ok(_aluno.Deletar(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
