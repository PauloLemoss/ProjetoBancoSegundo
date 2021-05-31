using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;


namespace A_Lenda
{
    public class RepositorioMySQLProfessor
    {
        public void Inserir(Professor professor)
        {   //conectando ao banco de dados um método inserir...


            MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            try
            {


                conexao.Open();

                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente

                MySqlCommand comando = new MySqlCommand($"INSERT INTO professor (NomeProfessor,CpfProf,EmailProf) VALUES (@NomeProfessor,@CpfProf,@EmailProf)", conexao);
                // Executando (Efetivando) o comando criando anteriormente
                comando.Parameters.AddWithValue("@NomeProfessor", professor.NomeProfessor);
                comando.Parameters.AddWithValue("@CpfProf", professor.CpfProf);
                comando.Parameters.AddWithValue("@EmailProf", professor.EmailProf);



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
        public void Alterar(Professor professor)
        {
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand(string.Format($"UPDATE professor SET NomeProfessor = @NomeProfessor, CpfProf=@CpfProf , EmailProf =@EmailProf  WHERE id_professor = @Id"), conexao);
                // Adicionando parâmetro SQL para o Id
                comando.Parameters.AddWithValue("@Id", professor.IdProfessor);
                // Adicionando parâmetro SQL para o Nome da disciplina
                comando.Parameters.AddWithValue("@NomeProfessor", professor.NomeProfessor);
                comando.Parameters.AddWithValue("@CpfProf", professor.CpfProf);
                comando.Parameters.AddWithValue("@EmailProf", professor.EmailProf);

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
                MySqlCommand comando = new MySqlCommand("DELETE FROM professor WHERE id_professor = @id", conexao);
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
        public List<Professor> Listar(string nome = null)
        {
            // Criando a lista de clientes vazia
            List<Professor> professores = new List<Professor>();
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
                    // Comando retornando todos os clientes ser "WHERE"
                    comando = new MySqlCommand("SELECT * FROM disciplina", conexao);
                }
                // Comando SQL em caso do parâmetro "Nome" não ser NULL
                else
                {
                    // Comando retornando todos os alunos com "WHERE", cujo
                    // Nome é igual ao informado pelo parâmetro
                    comando = new MySqlCommand("SELECT * FROM professor WHERE NomeProfessor LIKE @Nome", conexao);
                    // Adicionando parâmetro SQL para o Nome
                    comando.Parameters.AddWithValue("@NomeDisciplina", string.Format($"%{nome}%"));
                }
                // Executando (Efetivando) o comando criando anteriormente e salvando os dados no Data Reader
                MySqlDataReader reader = comando.ExecuteReader();
                // Em posse do Data Reader, vou ler os dados, sempre do primeiro até o último e "pra frente"
                while (reader.Read())
                {
                    // Instanciando um objeto chamado "aluno" da classe "Aluno"
                    Professor professor = new Professor();
                    // Buscando a informação ID do Banco de dados e salvando no atributo correspondente
                    professor.IdProfessor = reader.GetString("id_disciplina");
                    // Buscando a informação Nome do Banco de dados e salvando no atributo correspondente
                    professor.NomeProfessor = reader.GetString("NomeProfessor");
                    professor.CpfProf = reader.GetString("CpfProf");
                    professor.EmailProf = reader.GetString("EmailProf");
                    // Buscando a informação Email do Banco de dados e salvando no atributo correspondente

                    professores.Add(professor);
                }
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
            // Retornando a lista de alunos (agora totalmente preenchida)
            return professores;
        }
        public long ObterQuantidadeDeProfessor()
        {
            long quantidade = 0;
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM professor", conexao);
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
