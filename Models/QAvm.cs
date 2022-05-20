using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace graduation_project.Models
{
    public class QAvm
    {
        public Question question  { get; set; }
        public List<Answer> answers { get; set; }
        public Answer answer { get; set; }
    }
}