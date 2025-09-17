namespace Blog.Application.Exceptions
{
    public class UserNotCreatedException : Exception
    {
        public UserNotCreatedException()
        {

        }

        public UserNotCreatedException(string message) : base(message)
        {

        }
    }
}
