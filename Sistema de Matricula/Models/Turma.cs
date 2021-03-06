﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Sistema_de_Matricula.Models
{   [DataContract]
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Nome { get; set; }

        [IgnoreDataMember]
        public virtual Materia Materia { get; set; } ////NavigationPropiety coleção de alunos one-to-many

        [DataMember]
        public int CursoID { get; set; }

        [IgnoreDataMember]
        public virtual Professor Professor { get; set; } //NavigationPropiety  professor

        [DataMember]
        public int ProfessorID { get; set; } ////NavigationPropiety professor da turma one-to-many


       // todo many to many tem que criar tabela nova
        [IgnoreDataMember]
        public IList<AlunosTurma> AlunosTurmas { get; set; } //NavigationPropiety coleção de alunos many-to-many
    }
}
