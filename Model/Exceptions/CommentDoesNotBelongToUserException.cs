using System;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Es.Udc.DotNet.PracticaMaD.Model.Exceptions
{
    [Serializable]
    public class CommentDoesNotBelongToUserException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CommentDoesNotBelongToUserException"/> class.
        /// </summary>
        /// <param name="usrId"><c>usrId</c></param>
        /// <param name="idComment"><c>idComment</c></param>
        /// 

        public CommentDoesNotBelongToUserException(long usrId, long idComment)
            : base("Comment " + idComment + " does not belong to user " + usrId)
        {
            this.usrId = usrId;
            this.idComment = idComment;
        }

        public long usrId { get; private set; }

        public long idComment { get; private set; }

        #region Test Code Region. Uncomment for testing.


        #endregion
    }
}