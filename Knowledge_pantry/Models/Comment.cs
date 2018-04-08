using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge_pantry.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatorName { get; set; }
        public int ParentCommentId { get; set; }
        public int Like { get; set; }
        public DateTime CreationTime { get; set; }
        public Summary Summary { get; set; }

        public Comment() { }

        public Comment(string creatorName, int parentCommentId, Summary summary, string text)
        {
            CreationTime = DateTime.Now;
            Like = 0;
            CreatorName = creatorName;
            ParentCommentId = parentCommentId;
            Summary = summary;
            Text = text;
        }
    }
}
