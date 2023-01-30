using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.Exceptions
{
    public class InputValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="InputValidationException"/> class.
        /// </summary>
        /// <param message="message"><c>proId</c></param>

        public InputValidationException(string message)
           : base(message)
        {
            this.message = message;
        }

        public string message { get; private set; }

        #region Test Code Region. Uncomment for testing.

        #endregion
    }
}
