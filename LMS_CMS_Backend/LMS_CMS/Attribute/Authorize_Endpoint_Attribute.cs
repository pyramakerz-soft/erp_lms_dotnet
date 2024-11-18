namespace LMS_CMS_PL.Attribute
{
    [AttributeUsage(
       AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor, // Include constructors
       AllowMultiple = false,
       Inherited = true
   )]
    public class Authorize_Endpoint_Attribute : System.Attribute
    {
        public string[] AllowedTypes { get; }
        public int AllowEdit { get; }
        public int AllowDelete { get; }
        public string[] Pages { get; }

        public Authorize_Endpoint_Attribute(string[] allowedTypes, int allowEdit = 0, int allowDelete = 0, string[] pages = null)
        {
            AllowedTypes = allowedTypes ?? Array.Empty<string>();
            AllowEdit = allowEdit;
            AllowDelete = allowDelete;
            Pages = pages ?? Array.Empty<string>();
        }
    }
}
