namespace Blog.Application.Exceptions
{
    public class UserProblemException : Exception
    {
        public UserProblemException()
        {

        }

        public UserProblemException(string message) : base(message)
        {

        }
    }
}
