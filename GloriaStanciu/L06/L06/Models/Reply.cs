using System;
using System.Collections.Generic;
using System.Text;

namespace L06.Models
{
    public class Question
    {
        public string Title { get; }
        public string Body { get; }
        public string[] Tags { get; }

        public Question(string title, string body, string[] tags)
        {
            Title = title;
            Body = body;
            Tags = tags;
        }
    }

    public class Reply
    {
        public string QuestionId { get; }
        public string Answer { get; }
        public string AuthorId { get; }

        public Reply(string questionId, string answer, string authorId)
        {
            QuestionId = questionId;
            Answer = answer;
            AuthorId = authorId;
        }
    }
}
