﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public string Description { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }
        
        public virtual List<QuizStudent> QuizStudents { get; set;}

    }
}
