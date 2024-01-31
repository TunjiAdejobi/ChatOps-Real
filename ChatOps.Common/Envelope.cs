namespace ChatOps.Common
{
    public class Envelope<T>
    {
        public T Result { get; }

        public string ErrorMessage { get; }

        public List<string> ErrorMessages { get; }

        public bool HasError { get; }

        public DateTime TimeGenerated { get; }

        protected internal Envelope(T result, string errorMessage, bool hasError)
        {
            Result = result;
            ErrorMessage = errorMessage ?? string.Empty; // Initialize ErrorMessage to an empty string if null
            HasError = hasError;
            TimeGenerated = DateTime.UtcNow;
            ErrorMessages = new List<string>(); // Initialize the list to avoid null
        }

        protected internal Envelope(T result, List<string> errorMessages, bool hasError)
        {
            Result = result;
            ErrorMessages = errorMessages;
            HasError = hasError;
            TimeGenerated = DateTime.UtcNow;
            ErrorMessage = string.Empty; // Initialize ErrorMessage to an empty string
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(List<string> errorMessages, bool hasError)
            : base(null, errorMessages, hasError)
        {
        }

        protected Envelope(string errorMessage, bool hasError)
            : base(null, errorMessage, hasError)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            string? nullRes = null;

            return new Envelope<T>(result, nullRes, false);
        }

        public static Envelope Ok()
        {
            string? nullRes = null;

            return new Envelope(nullRes, false);
        }

        public static Envelope Error(string errorMessage)
        {
            return new Envelope(errorMessage, true);
        }

        public static Envelope ErrorList(List<string> errorMessages)
        {
            return new Envelope(errorMessages, true);
        }
    }
}