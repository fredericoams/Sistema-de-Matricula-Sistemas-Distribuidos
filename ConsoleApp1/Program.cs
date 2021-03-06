﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Matricula;
using System.ServiceModel;
using Sistema_de_Matricula.Models;
using Sistema_de_Matricula.Servicos;
using System.ServiceModel.Description;
using Sistema_de_Matricula.Data;

namespace ServiceLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var hosts = IniciaHost();
            try
            {
                Console.WriteLine("Iniciando...");
                hosts.ForEach(h =>h.Open());
                ServicesInfo(hosts);
                DadosTeste();
                Console.ReadLine();
                hosts.ForEach(h => h.Close());
            }
            catch (Exception e)
            {
                hosts.ForEach(h => h.Abort());
                Console.WriteLine(e);
                Console.ReadLine();

            }
        }


        public static void ServicesInfo(List<ServiceHost> servicos)
        {
            servicos.ForEach(s => Console.WriteLine("{0} Online",s.Description.ServiceType));
        }

        public static List<ServiceHost> IniciaHost()
        {
            var hosts = new List<ServiceHost>();
            ServiceHost hostAlunos = new ServiceHost(typeof(AlunoService), new Uri("http://localhost:3030/alunos"));
            hostAlunos.AddServiceEndpoint(typeof(IAlunoService), new BasicHttpBinding(), "http://localhost:3030/alunos");
            ServiceHost hostProfessor = new ServiceHost(typeof(ProfessorService), new Uri("http://localhost:3030/professores"));
            hostProfessor.AddServiceEndpoint(typeof(IProfessorService), new BasicHttpBinding(), "http://localhost:3030/professores");
            ServiceHost hostTurma = new ServiceHost(typeof(TurmaService), new Uri("http://localhost:3030/turmas"));
            hostTurma.AddServiceEndpoint(typeof(ITurmaService), new BasicHttpBinding(), "http://localhost:3030/turmas");
            ServiceHost hostMateria = new ServiceHost(typeof(MateriaService), new Uri("http://localhost:3030/materias"));
            hostMateria.AddServiceEndpoint(typeof(IMateriaService), new BasicHttpBinding(), "http://localhost:3030/materias");
            ServiceHost hostCurso = new ServiceHost(typeof(CursoService), new Uri("http://localhost:3030/cursos"));
            hostCurso.AddServiceEndpoint(typeof(ICursoService), new BasicHttpBinding(), "http://localhost:3030/cursos");
            ServiceHost hostAlunosTurma = new ServiceHost(typeof(AlunoTurmaService), new Uri("http://localhost:3030/alunosturma"));
            hostAlunosTurma.AddServiceEndpoint(typeof(IAlunoTurmaService), new BasicHttpBinding(), "http://localhost:3030/alunosturma");

            hosts.Add(hostAlunos);
            hosts.Add(hostProfessor);
            hosts.Add(hostTurma);
            hosts.Add(hostMateria);
            hosts.Add(hostCurso);
            hosts.Add(hostAlunosTurma);

            hosts.ForEach(h => h.Description.Behaviors.
                                    Add(new ServiceMetadataBehavior() { HttpGetEnabled = true }));
            return hosts;
        }

        public static void DadosTeste() {

            CursoDAO dao = new CursoDAO();
            dao.Add(new Curso() { ID=  0, Nome=  "Engenharia da Computação" });
            dao.Add(new Curso() { ID = 1, Nome = "Engenharia Civil" });
            dao.Add(new Curso() { ID = 2, Nome = "Engenharia de Produção" });
            dao.Add(new Curso() { ID = 3, Nome = "Publicidade" });
            dao.Add(new Curso() { ID = 4, Nome = "Jornalismo" });
        }




    }
}
