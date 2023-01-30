using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CatalogService
{
    public class CommentBlock
    {
        public List<Comment> Comments { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentBlock(List<Comment> comments, bool existMoreComments)
        {
            this.Comments = comments;
            this.ExistMoreComments = existMoreComments;
        }
    }
}
