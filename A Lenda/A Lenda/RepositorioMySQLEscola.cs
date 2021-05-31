using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace A_Lenda
{
    public class RepositorioMySQLEscola
    {
        public void Inserir(Escola escola)
        {   //conectando ao banco de dados um método inserir...


            MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
            try
            {


                conexao.Open();

                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente

                MySqlCommand comando = new MySqlCommand($"INSERT INTO escola (NomeEscola,EnderecoEscola,BairroEscola,NumeroEscola) VALUES (@NomeEscola,@EnderecoEscola,@BairroEscola,@NumeroEscola)", conexao);
                // Executando (Efetivando) o comando criando anteriormente
                comando.Parameters.AddWithValue("@NomeEscola", escola.NomeEscola);
                comando.Parameters.AddWithValue("@EnderecoEscola", escola.EnderecoEscola);
                comando.Parameters.AddWithValue("@BairroEscola", escola.BairroEscola);
                comando.Parameters.AddWithValue("@NumeroEscola", escola.NumeroEscola);



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
        public void Alterar(Escola escola)
        {
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand(string.Format($"UPDATE escola SET NomeEscola = @NomeEscola, EnderecoEscola=@EnderecoEscola , BairroEscola =@BairroEscola,NumeroEscola=@NumeroEscola  WHERE id_escola = @Id"), conexao);
                // Adicionando parâmetro SQL para o Id
                comando.Parameters.AddWithValue("@Id", escola.IdEscola);
                // Adicionando parâmetro SQL para o Nome da disciplina
                comando.Parameters.AddWithValue("@NomeEscola", escola.NomeEscola);
                comando.Parameters.AddWithValue("@EnderecoEscola", escola.EnderecoEscola);
                comando.Parameters.AddWithValue("@BairroEscola", escola.NumeroEscola);
                comando.Parameters.AddWithValue("@NumeroEscola", escola.BairroEscola);

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
                MySqlCommand comando = new MySqlCommand("DELETE FROM escola WHERE id_escola = @id", conexao);
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
        public List<Escola> Listar(string nome = null)
        {
            // Criando a lista de clientes vazia
            List<Escola> escolas = new List<Escola>();
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
                    comando = new MySqlCommand("SELECT * FROM escola", conexao);
                }
                // Comando SQL em caso do parâmetro "Nome" não ser NULL
                else
                {
                    // Comando retornando todos os alunos com "WHERE", cujo
                    // Nome é igual ao informado pelo parâmetro
                    comando = new MySqlCommand("SELECT * FROM escola WHERE NomeEscola LIKE @Nome", conexao);
                    // Adicionando parâmetro SQL para o Nome
                    comando.Parameters.AddWithValue("@NomeEscola", string.Format($"%{nome}%"));
                }
                // Executando (Efetivando) o comando criando anteriormente e salvando os dados no Data Reader
                MySqlDataReader reader = comando.ExecuteReader();
                // Em posse do Data Reader, vou ler os dados, sempre do primeiro até o último e "pra frente"
                while (reader.Read())
                {
                    // Instanciando um objeto chamado "aluno" da classe "Aluno"
                    Escola escola = new Escola();
                    // Buscando a informação ID do Banco de dados e salvando no atributo correspondente
                    escola.IdEscola = reader.GetString("id_disciplina");
                    // Buscando a informação Nome do Banco de dados e salvando no atributo correspondente
                    escola.NomeEscola = reader.GetString("NomeEscola");
                    escola.EnderecoEscola = reader.GetString("EnderecoEscola");
                    escola.NumeroEscola = reader.GetInt32("BairroEscola");
                    escola.BairroEscola = reader.GetString("NumeroEscola");
                    // Buscando a informação Email do Banco de dados e salvando no atributo correspondente

                    escolas.Add(escola);
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
            return escolas;
        }
        public long ObterQuantidadeDeEscolas()
        {
            long quantidade = 0;
            // Bloco do Try: Tentando abrir conexão e realizar comando no Banco de Dados
            try
            {
                MySqlConnection conexao = new MySqlConnection(@"Server=localhost;Port=3306;DataBase=bdprovapaulo;Uid=root;Pwd=kabuterimon12;");
                // Abrindo a conexão com o banco de dados;
                conexao.Open();
                // Instanciando um "Command" com o código SQL e a Conexão estabelecida anteriormente
                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM escola", conexao);
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
