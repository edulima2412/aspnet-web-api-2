using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App.Repository
{
    public class AlunoDAO
    {
        // Duas possibilidades de pegar a conexão
        //string stringConexao = ConfigurationManager.AppSettings["stringConexao"];
        public string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        public IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);
            conexao.Open();
        }

        public List<AlunoDTO> ListarAlunosDB(int? id)
        {
            try
            {
                var listaAlunos = new List<AlunoDTO>();

                // Criação do comando
                IDbCommand selectCmd = conexao.CreateCommand();

                if (id == null)
                {
                    // Selecção do comando
                    selectCmd.CommandText = "SELECT * FROM Alunos";
                }
                else
                {
                    // Pode utilizar os parametros sem instanciar o DataParameter
                    selectCmd.CommandText = $"SELECT * FROM Alunos WHERE id = {id}";
                }

                // Execução do comando
                IDataReader resultado = selectCmd.ExecuteReader();
                while (resultado.Read())
                {
                    var date = Convert.ToDateTime(resultado["data"].ToString());

                    var aluno = new AlunoDTO
                    {
                        id = Convert.ToInt32(resultado["id"]),
                        nome = resultado["nome"].ToString(),
                        sobrenome = resultado["sobrenome"].ToString(),
                        telefone = resultado["telefone"].ToString(),
                        registro = resultado["registro"].ToString(),
                        data = date.ToString("yyyy-MM-dd")
                    };

                    listaAlunos.Add(aluno);
                }

                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Alunos (nome,sobrenome,telefone,registro,data) VALUES (@nome,@sobrenome,@telefone,@registro,@data)";

                // Referenciando os parametros
                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                insertCmd.Parameters.Add(paramNome);

                IDbDataParameter paramSobronome = new SqlParameter("sobrenome", aluno.sobrenome);
                insertCmd.Parameters.Add(paramSobronome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                insertCmd.Parameters.Add(paramTelefone);

                IDbDataParameter paramRegistro = new SqlParameter("registro", aluno.registro);
                insertCmd.Parameters.Add(paramRegistro);

                // Recebe data atual
                aluno.data = DateTime.Now.ToString();

                IDbDataParameter paramData = new SqlParameter("data", aluno.data);
                insertCmd.Parameters.Add(paramData);

                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void AtualizarAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "UPDATE Alunos SET nome = @nome, sobrenome = @sobrenome, telefone = @telefone, registro = @registro WHERE id = @id";

                // Referenciando os parametros
                IDbDataParameter paramId = new SqlParameter("id", aluno.id);
                updateCmd.Parameters.Add(paramId);

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                updateCmd.Parameters.Add(paramNome);

                IDbDataParameter paramSobronome = new SqlParameter("sobrenome", aluno.sobrenome);
                updateCmd.Parameters.Add(paramSobronome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                updateCmd.Parameters.Add(paramTelefone);

                IDbDataParameter paramRegistro = new SqlParameter("registro", aluno.registro);
                updateCmd.Parameters.Add(paramRegistro);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM Alunos WHERE id = @id";

                // Referenciando os parametros
                IDbDataParameter paramId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}