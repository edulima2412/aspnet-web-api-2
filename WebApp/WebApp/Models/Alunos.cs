using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Alunos
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public string registro { get; set; }
        public string dataCadastro { get; set; }

        public List<Alunos> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/db.json");
            var json = File.ReadAllText(caminhoArquivo);
            var alunos = JsonConvert.DeserializeObject<List<Alunos>>(json);

            return alunos;
        }

        public bool ReescreverArquivo(List<Alunos> alunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/db.json");

            var json = JsonConvert.SerializeObject(alunos, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public List<Alunos> Salvar(Alunos aluno)
        {
            var listaAlunos = this.ListarAlunos();

            // Seleciona o ultimo id do aluno da lista
            var maxId = listaAlunos.Max(a => a.id);

            aluno.id = maxId + 1;

            var dataAtual = DateTime.Now.ToString("yyyy-MM-dd");
            aluno.dataCadastro = dataAtual;

            listaAlunos.Add(aluno);

            ReescreverArquivo(listaAlunos);

            return listaAlunos;
        }

        public Alunos Atualizar(int id, Alunos aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(p => p.id == id);

            if(itemIndex > 0)
            {
                aluno.id = id;
                aluno.dataCadastro = listaAlunos[itemIndex].dataCadastro;
                listaAlunos[itemIndex] = aluno;
            } else
            {
                return null;
            }

            ReescreverArquivo(listaAlunos);

            return aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(p => p.id == id);

            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaAlunos);
            return true;
        }
    }
}