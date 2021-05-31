using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Lenda
{
    public class Turma
    {
        public int Id { get; set; }
        public string NomeTurma { get; set; }
        public int IdProfessor { get; set; }
        public int IdAluno { get; set; }
        public int IdDisciplina { get; set; }
        public Turma() { }

        public Turma(int id, string nomeTurma, int idProfessor, int idAluno, int idDisciplina)
        {
            Id = id;
            NomeTurma = nomeTurma;
            IdProfessor = idProfessor;
            IdAluno = idAluno;
            IdDisciplina = idDisciplina;
        }

        public Turma(string nomeTurma, int idProfessor, int idAluno, int idDisciplina)
        {
            NomeTurma = nomeTurma;
            IdProfessor = idProfessor;
            IdAluno = idAluno;
            IdDisciplina = idDisciplina;
        }
        public override string ToString()
        {
            return string.Format($"{Id} {NomeTurma} {IdProfessor} {IdAluno} { IdDisciplina}");
        }
    }
}

