using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace A_Lenda
{
    public class RepositorioMySQLTurma
    {
        public void Inserir(Turma turma)
        {   //conectando ao banco de dados um método inserir...
            //Conexao C = new Conexao();

            MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            try
            {


                conexao.Open();

                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente

                MySqlCommand comando = new MySqlCommand($"INSERT INTO turma (NomeTurma,id_professor,id_aluno,id_disciplina) VALUES (@NomeTurma,@id_professor,@id_aluno,@id_disciplina)", conexao);
                // Executando (Efetivando) o comando criando anteriormente
                comando.Parameters.AddWithValue("@NomeTurma", turma.NomeTurma);
                comando.Parameters.AddWithValue("@id_professor", turma.IdProfessor);
                comando.Parameters.AddWithValue("@id_aluno", turma.IdAluno);
                comando.Parameters.AddWithValue("@id_disciplina", turma.IdDisciplina);




                comando.ExecuteNonQuery();


            }
            catch
            {
                System.Console.WriteLine("Não foi possível inserir os dados...");
            }
            finally
            {
                // Fechando a conexão com o banco de dados ();;
                conexao.Close();
            }




        }
        public void Alterar(Turma turma)
        {
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand(string.Format($"UPDATE turma SET NomeTurma = @NomeTurma, id_professor=@id_professor , id_aluno=@id_aluno,id_disciplina=@id_disciplina  WHERE id_turma = @Id"), conexao);
                // Adicionando parâmetro SQL para o Id
                comando.Parameters.AddWithValue("@Id", turma.Id);
                // Adicionando parâmetro SQL para o Nome da disciplina
                comando.Parameters.AddWithValue("@NomeTurma", turma.NomeTurma);
                comando.Parameters.AddWithValue("@id_professor", turma.IdProfessor);
                comando.Parameters.AddWithValue("@id_aluno", turma.IdAluno);
                comando.Parameters.AddWithValue("@id_disciplina", turma.IdDisciplina);


                comando.ExecuteNonQuery();
            }
            //tratando possíveis erros
            catch (Exception ex)
            {
                // Escalando o erro para a classe que chama este método
                throw ex;
            }
            // Conseguindo ou não realizar as opções, executar o Finally (Fechando a conexão)
            finally
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }


        }
        public void Apagar(int id)
        {
            MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");

            try
            {
                //Excluindo o registro
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("DELETE FROM turma WHERE id_turma = @id", conexao);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch
            {
                System.Console.WriteLine("Não foi possível deletar");
            }
            finally
            {
                conexao.Close();
            }
        }
        //Método listar, cujo parâmetro nome é "opcional"
        public List<Turma> Listar(string nome = null)
        {
            // lista turma
            List<Turma> turmas = new List<Turma>();
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = null;
                // Comando SQL em caso do parâmetro "Nome" ser NULL
                if (nome == null)
                {
                    // Comando retornando todas as trumas com um inner join ser "WHERE"
                    comando = new MySqlCommand("SELECT turma.id_turma,turma.NomeTurma,professor.NomeProfessor,aluno.NomeAluno,disciplina.NomeDisciplina from turma INNER JOIN professor ON professor.id_professor = turma.id_professor INNER JOIN aluno ON aluno.id_aluno = turma.id_turma INNER JOIN disciplina ON disciplina.id_disciplina = turma.id_disciplina", conexao);
                }
                // Comando SQL em caso do parâmetro "Nome" não ser NULL
                else
                {
                    // Comando retornando todos os alunos com "WHERE", cujo
                    // Nome é igual ao informado pelo parâmetro
                    comando = new MySqlCommand("SELECT turma.id_turma,turma.NomeTurma,professor.NomeProfessor,aluno.NomeAluno,disciplina.NomeDisciplina from turma INNER JOIN professor ON professor.id_professor = turma.id_professor INNER JOIN aluno ON aluno.id_aluno = turma.id_turma INNER JOIN disciplina ON disciplina.id_disciplina = turma.id_disciplina", conexao);
                }
                // Adicionando parâmetro SQL para o Nome
                comando.Parameters.AddWithValue("@NomeTurma", string.Format($"%{nome}%"));
            }
            catch
            {

            }
            // Executando (Efetivando) o comando criando anteriormente e salvando os dados no Data Reader
            MySqlDataReader reader = comando.ExecuteReader();
            // Em posse do Data Reader, vou ler os dados, sempre do primeiro até o último e "pra frente"
            while (reader.Read())
            {
                // Instanciando um objeto chamado "turma" da classe "Turma"
                Turma turma = new Turma();
                // Buscando a informação ID do Banco de dados e salvando no atributo correspondente
                turma.Id = reader.GetInt32("id_turma");
                turma.NomeTurma = reader.GetString("NomeTurma");
                turma.IdProfessor = reader.GetInt32("id_professor");
                // Buscando a informação Nome do Banco de dados e salvando no atributo correspondente
                turma.IdAluno = reader.GetInt32("id_aluno");
                turma.IdDisciplina = reader.GetInt32("id_disciplina");

                // Buscando a informação Email do Banco de dados e salvando no atributo correspondente

                turmas.Add(turma);
            }
            try
            {

            }
 

            catch(Exception ex)
            {
                // Escalando o erro para a classe que chama este método
                throw ex;
            }
            // Conseguindo ou não realizar as opções, executar o Finally (Fechando a conexão)
            finally
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }
            // Retornando a lista de alunos (agora totalmente preenchida)
            return turmas;
        }
        public long ObterQuantidadeDeTurma()
        {
            long quantidade = 0;
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM turma", conexao);
                // Executando (Efetivando) o comando criando anteriormente e salvando os dados inteiro "quantidade"
                quantidade = (long)comando.ExecuteScalar();
            }
            // Tratando possíveis erros ...
            catch (Exception ex)
            {
                // Escalando o erro para a classe que chama este método
                throw ex;
            }
            // Conseguindo ou não realizar as opções, executar o Finally (Fechando a conexão)
            finally
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }
            // Retornando a quantidade de aluno
            return quantidade;

        }
    }
}


