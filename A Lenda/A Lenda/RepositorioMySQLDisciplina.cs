using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace A_Lenda
{
    public class RepositorioMySQLDisciplina
    {
        public void Inserir(Disciplina disciplina)
        {   //conectando ao banco de dados um método inserir...


            MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            try
            {


                conexao.Open();

                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente

                MySqlCommand comando = new MySqlCommand($"INSERT INTO discplina (NomeDisciplina) VALUES (@NomeDisciplina)", conexao);
                // Executando (Efetivando) o comando criando anteriormente
                comando.Parameters.AddWithValue("@NomeDisciplina", disciplina.NomeDisciplina);


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
        public void Alterar(Disciplina disciplina)
        {
            MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            {

                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand(string.Format($"UPDATE disciplina SET NomeDisciplina = @NomeDisciplina WHERE id_disciplina = @Id"), conexao);
                // Adicionando parâmetro SQL para o Id
                comando.Parameters.AddWithValue("@Id", disciplina.IdDisciplina);
                // Adicionando parâmetro SQL para o Nome da disciplina
                comando.Parameters.AddWithValue("@NomeDisciplina", disciplina.NomeDisciplina);


                comando.ExecuteNonQuery();
            }
            try { 
            }            //tratando possíveis erros
            catch (Exception ex)
            {
                // Escalando o erro para a classe que chama este método
                throw ex;
            }
            // Conseguindo ou não realizar as opções, executar o Finally (Fechando a conexão)
            finally
            {

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
                MySqlCommand comando = new MySqlCommand("DELETE FROM disciplina WHERE id_disciplina = @id", conexao);
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
        public List<Disciplina> Listar(string nome = null)
        {
            MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            // Criando a lista de clientes vazia
            List<Disciplina> disciplinas = new List<Disciplina>();
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {

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
                    comando = new MySqlCommand("SELECT * FROM disciplina WHERE NomeDisciplina LIKE @Nome", conexao);
                    // Adicionando parâmetro SQL para o Nome
                    comando.Parameters.AddWithValue("@NomeDisciplina", string.Format($"%{nome}%"));
                }
                // Executando (Efetivando) o comando criando anteriormente e salvando os dados no Data Reader
                MySqlDataReader reader = comando.ExecuteReader();
                // Em posse do Data Reader, vou ler os dados, sempre do primeiro até o último e "pra frente"
                while (reader.Read())
                {
                    // Instanciando um objeto chamado "aluno" da classe "Aluno"
                    Disciplina disciplina = new Disciplina();
                    // Buscando a informação ID do Banco de dados e salvando no atributo correspondente
                    disciplina.IdDisciplina = reader.GetString("id_disciplina");
                    // Buscando a informação Nome do Banco de dados e salvando no atributo correspondente
                    disciplina.NomeDisciplina = reader.GetString("NomeDisciplina");
                    // Buscando a informação Email do Banco de dados e salvando no atributo correspondente

                    disciplinas.Add(disciplina);
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

                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }
            // Retornando a lista de alunos (agora totalmente preenchida)
            return disciplinas;
        }
        public long ObterQuantidadeDisciplinas()
        {
            long quantidade = 0;
            MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {

                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM disciplina", conexao);
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
                // Fechando a conexão com o banco de dados (Tem que fechar sempre);
                conexao.Close();
            }
            // Retornando a quantidade de aluno
            return quantidade;

        }
    }
}
