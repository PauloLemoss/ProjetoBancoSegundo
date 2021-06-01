using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace A_Lenda
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                // Perguntar qual é a ação que o usuário deseja
                Console.WriteLine("Escolha uma opção:");
                // Mudando a cor de fundo do texto para Azul


                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("_______________________ ALUNOS __________________________________");



                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("1 - Inserir Aluno");
                Console.WriteLine("2 - Deletar Aluno");
                Console.WriteLine("3 - Listar Aluno");
                Console.WriteLine("4 - Consultar Aluno");
                Console.WriteLine("5 - Alterar Aluno");
                Console.WriteLine("6 - Contar Aluno");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("_______________________ DISCIPLINA_______________________");


                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("7 - Inserir Disciplina ");
                Console.WriteLine("8 - Deletar Disciplina");
                Console.WriteLine("9 - Listar Disciplina");
                Console.WriteLine("10 - Alterar Disciplina");
                Console.WriteLine("11 - Contar Disciplina");
                Console.BackgroundColor = ConsoleColor.DarkRed;


                Console.WriteLine("_______________________ Professor __________________________________");

                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("12 - Inserir Professor");
                Console.WriteLine("13 - Deletar Professor");
                Console.WriteLine("14 - Listar Professor");
                Console.WriteLine("15 - Consultar Professor");
                Console.WriteLine("16 - Alterar Professor");
                Console.WriteLine("17 - Contar Professor");

                Console.WriteLine("_________________________Turma_____________________________");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("18 - Inserir Turma");
                Console.WriteLine("19 - Deletar Turma");
                Console.WriteLine("20 - Listar Turma");
                Console.WriteLine("21 - Consultar Turma");
                Console.WriteLine("22 - Alterar Turma");
                Console.WriteLine("23 - Contar Turma");

                Console.WriteLine("__________________________Escola_____________________________");
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("24 - Inserir Escola");
                Console.WriteLine("25 - Deletar Escola");
                Console.WriteLine("26 - Listar Escola");
                Console.WriteLine("27 - Consultar Escola");
                Console.WriteLine("28 - Alterar Escola");
                Console.WriteLine("29 - Contar Escola ");

                // Voltando a cor do texto para a cor padrão
                Console.ResetColor();
                // Instanciar os  repositórios
                RepositorioMySQLAluno repositorioaluno = new RepositorioMySQLAluno();
                RepositorioMySQLDisciplina repositoriodisciplina = new RepositorioMySQLDisciplina();
                RepositorioMySQLEscola repositorioescola = new RepositorioMySQLEscola();
               
                RepositorioMySQLTurma repositorioturma = new RepositorioMySQLTurma();
                try
                {
                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            Console.Clear();

                            // Instanciar o Aluno
                            Aluno aluno = new Aluno();
                            // Solicitar o nome do aluno
                            Console.WriteLine("Informe o seu nome:");
                            // Ober o nome do aluno (digitado no teclado)
                            aluno.NomeAluno = Console.ReadLine();
                            // Solicitar o email do cliente
                            Console.WriteLine("Informe o seu e-mail:");
                            // Ober o email do aluno (digitado no teclado)
                            aluno.EmailAluno = Console.ReadLine();
                            Console.WriteLine("Informe o seu cpf:");
                            // Ober o cpf do cliente (digitado no teclado)
                            aluno.CpfAluno = int.Parse(Console.ReadLine());
                            // Chamando o método "Inserir", passando o objeto da classe "Cliente"
                            // que foi instanciado anteriormente

                            repositorioaluno.Inserir(aluno);
                            break;
                        case '2':
                            Console.Clear();
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            ListarAlunos(repositorioaluno);
                            // Perguntar ao usuário qual é o ID que ele deseja deletar
                            Console.WriteLine("Informe o ID do usuário que você deseja deletar:");
                            // Chamar o método deletar, passando o ID informado pelo usuário
                            repositorioaluno.Apagar(int.Parse(Console.ReadLine()));
                            break;
                        case '3':
                            Console.Clear();
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            ListarAlunos(repositorioaluno);
                            break;
                        case '4':
                            Console.Clear();
                            // Perguntar o nome que deseja consultar
                            Console.WriteLine("Informe o nome que deseja consultar:");
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Aluno AlunoDaVez in repositorioaluno.Listar(Console.ReadLine()))
                            {
                                // Imprimindo as informações de cada aluno na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Aluno ...
                                Console.WriteLine(AlunoDaVez.ToString());
                            }
                            break;
                        case '5':
                            Console.Clear();
                            // Perguntar o nome que deseja consultar
                            Console.WriteLine("Informe o nome que deseja alterar:");
                            // Obtendo a lista de Clientes
                            List<Aluno> listaDeAlunos = repositorioaluno.Listar(Console.ReadLine());
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Aluno alunoDaVez in listaDeAlunos)
                            {
                                // Imprimindo as informações de cada cliente na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Cliente ...
                                Console.WriteLine(alunoDaVez.ToString());
                            }
                            if (listaDeAlunos != null && listaDeAlunos.Count > 0)
                            {
                                // Solicitar o ID de qual dos clientes listados deseja alterar:
                                Console.WriteLine("Informe, pelo ID, qual dos alunos acima você deseja alterar:");
                                // Buscar, na lista de Clientes, pelo Id, o objeto cliente relacionado ...
                                Aluno AlunoAlterar = listaDeAlunos.FindLast(c => c.IdAluno == int.Parse(Console.ReadLine()));
                                // Solicitar o novo nome
                                Console.WriteLine($"Informe o nome nome para {AlunoAlterar.NomeAluno}:");
                                // Alterando a Propriedade Nome do Cliente encontrado ...
                                AlunoAlterar.NomeAluno = Console.ReadLine();
                                // Efetivando a alteração no Banco de Dados
                                repositorioaluno.Alterar(AlunoAlterar);
                            }
                            else
                            {
                                // Caso a lista de Clientes não retorne nenhum registro, exibir mensagem abaixo.
                                Console.WriteLine("Nenhum registro encontrado ...");
                            }
                            break;
                        case '6':
                            Console.Clear();
                            // Informando a quantidade de Clientes registrados, através do método Obter Quantidade ...
                            Console.WriteLine($"Total de Alunos registrados: {repositorioaluno.ObterQuantidadeDeAlunos()}");
                            break;
                        case '7':
                            Console.Clear();
                            // Perguntar o nome que deseja adicionar carro ...
                            Console.WriteLine("Informe o nome do cliente para adcionar um Novo Carro:");
                            // Obtendo a lista de Clientes
                            List<Aluno> listaDeClientesCarros = repositorioaluno.Listar(Console.ReadLine());
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Aluno AlunoDaVez in listaDeClientesCarros)
                            {
                                // Imprimindo as informações de cada cliente na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Cliente ...
                                Console.WriteLine(AlunoDaVez.ToString());
                            }

                            break;
                            Console.Clear();
                        // Perguntar o nome que deseja consultar

                        default:
                            Console.Clear();
                            Console.WriteLine("Opção Indisponível!");
                            break;
                    }
                }
                catch (MySqlException ex)
                {
                    // Mudando a cor do texto para Vermelho (Informações Críticas)
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Exibindo mensagens de erro de banco de dados
                    Console.WriteLine("Ocorreu um erro ao tentar realizar a Operação no Banco de Dados.");
                    Console.WriteLine("Contacte o suporte.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                catch (FormatException ex)
                {
                    // Mudando a cor do texto para Amarelo (Informações Importantes)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // Eixibindo mensagem de erro de Formato
                    Console.WriteLine("Alguma das informações não estava no formato correto.");
                    Console.WriteLine("Tente novamente, inserindo as informações corretas.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    // Mudando a cor do texto para Vermelho (Informações Críticas)
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Exibindo mensagens genéricas ...
                    Console.WriteLine("Ocorreu um erro.");
                    Console.WriteLine("Contacte o suporte.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                Console.WriteLine("Você deseja continuar? s p/ sim ...");
            } while (Console.ReadKey().KeyChar == 's');
        }

        public static void ListarAlunos(RepositorioMySQLAluno repositorio)
        {
            // Listando todos os alunos retornados pela lista de aluno através do método repositorio.Listar()
            foreach (Aluno AlunoDaVez in repositorio.Listar())
            {
                // Imprimindo as informações de cada aluno na tela, utilizando o "ToString" que foi sobrescrito na classe
                // Aluno ...
                Console.WriteLine(AlunoDaVez.ToString());
            }

            do
            {
                try
                {
                    RepositorioMySQLProfessor repositorioprof = new RepositorioMySQLProfessor();
                    Professor professor = new Professor();
                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            Console.Clear();

                            // Instanciar o Professor

                            
                            // Solicitar o nome do aluno
                            Console.WriteLine("Informe o seu nome:");
                            // Ober o nome do aluno (digitado no teclado)
                            professor.NomeProfessor = Console.ReadLine();
                            // Solicitar o email do cliente
                            Console.WriteLine("Informe o seu e-mail:");
                            // Ober o email do aluno (digitado no teclado)
                            professor.EmailProf = Console.ReadLine();
                            Console.WriteLine("Informe o seu cpf:");
                            // Ober o cpf do cliente (digitado no teclado)
                            professor.CpfProf = (Console.ReadLine());
                            professor.IdProfessor = int.Parse(Console.ReadLine());
                            // Chamando o método "Inserir", passando o objeto da classe "Cliente"
                            // que foi instanciado anteriormente

                            repositorioprof.Inserir(professor);
                            break;
                        case '2':
                            Console.Clear();
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            Listarprofessores(repositorioprof);
                            // Perguntar ao usuário qual é o ID que ele deseja deletar
                            Console.WriteLine("Informe o ID do usuário que você deseja deletar:");
                            // Chamar o método deletar, passando o ID informado pelo usuário
                            repositorioprof.Apagar(int.Parse(Console.ReadLine()));
                            break;
                        case '3':
                            Console.Clear();
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            Listarprofessores(repositorioprof);
                            break;
                        case '4':
                            Console.Clear();
                            // Perguntar o nome que deseja consultar
                            Console.WriteLine("Informe o nome que deseja consultar:");
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Professor ProfessorDaVez in repositorioprof.Listar(Console.ReadLine()))
                            {
                                // Imprimindo as informações de cada aluno na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Aluno ...
                                Console.WriteLine(ProfessorDaVez.ToString());
                            }
                            break;
                        case '5':
                            Console.Clear();
                            // Perguntar o nome que deseja consultar
                            Console.WriteLine("Informe o nome que deseja alterar:");
                            // Obtendo a lista de Clientes
                            List<Professor> listaDeProfessores = repositorioprof.Listar(Console.ReadLine());
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Professor ProfessorDaVez in listaDeProfessores)
                            {
                                // Imprimindo as informações de cada cliente na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Cliente ...
                                Console.WriteLine(ProfessorDaVez.ToString());
                            }
                            if (listaDeProfessores != null && listaDeProfessores.Count > 0)
                            {
                                // Solicitar o ID de qual dos clientes listados deseja alterar:
                                Console.WriteLine("Informe, pelo ID, qual dos alunos acima você deseja alterar:");
                                // Buscar, na lista de Clientes, pelo Id, o objeto cliente relacionado ...
                                Professor ProfessorAlterar = listaDeProfessores.FindLast(c => c.IdProfessor == int.Parse(Console.ReadLine()));
                                // Solicitar o novo nome
                                Console.WriteLine($"Informe o nome nome para {ProfessorAlterar.NomeProfessor}:");
                                // Alterando a Propriedade Nome do Cliente encontrado ...
                                ProfessorAlterar.NomeProfessor = Console.ReadLine();
                                // Efetivando a alteração no Banco de Dados
                                repositorioprof.Alterar(ProfessorAlterar);
                            }
                            else
                            {
                                // Caso a lista de Clientes não retorne nenhum registro, exibir mensagem abaixo.
                                Console.WriteLine("Nenhum registro encontrado ...");
                            }
                            break;
                        case '6':
                            Console.Clear();
                            // Informando a quantidade de Clientes registrados, através do método Obter Quantidade ...
                            Console.WriteLine($"Total de professores registrados: {repositorioprof.ObterQuantidadeDeProfessor()}");
                            break;
                        case '7':
                            Console.Clear();
                            // Perguntar o nome que deseja adicionar carro ...
                            Console.WriteLine("Informe o nome do cliente para adcionar um Novo Carro:");
                            // Obtendo a lista de Clientes
                            List<Professor> listaDeClientesCarros = repositorioprof.Listar(Console.ReadLine());
                            // Listando todos os clientes retornados pela lista de cliente através do método repositorio.Listar()
                            foreach (Professor professorDaVez in listaDeClientesCarros)
                            {
                                // Imprimindo as informações de cada cliente na tela, utilizando o "ToString" que foi sobrescrito na classe
                                // Cliente ...
                                Console.WriteLine(professorDaVez.ToString());
                            }

                            break;
                            Console.Clear();
                        // Perguntar o nome que deseja consultar

                        default:
                            Console.Clear();
                            Console.WriteLine("Opção Indisponível!");
                            break;
                    }
                }
                catch (MySqlException ex)
                {
                    // Mudando a cor do texto para Vermelho (Informações Críticas)
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Exibindo mensagens de erro de banco de dados
                    Console.WriteLine("Ocorreu um erro ao tentar realizar a Operação no Banco de Dados.");
                    Console.WriteLine("Contacte o suporte.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                catch (FormatException ex)
                {
                    // Mudando a cor do texto para Amarelo (Informações Importantes)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // Eixibindo mensagem de erro de Formato
                    Console.WriteLine("Alguma das informações não estava no formato correto.");
                    Console.WriteLine("Tente novamente, inserindo as informações corretas.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    // Mudando a cor do texto para Vermelho (Informações Críticas)
                    Console.ForegroundColor = ConsoleColor.Red;
                    // Exibindo mensagens genéricas ...
                    Console.WriteLine("Ocorreu um erro.");
                    Console.WriteLine("Contacte o suporte.");
                    // Voltando a cor do texto para a cor padrão
                    Console.ResetColor();
                }
                Console.WriteLine("Você deseja continuar? s p/ sim ...");
            } while (Console.ReadKey().KeyChar == 's');
        }
    }        
}
