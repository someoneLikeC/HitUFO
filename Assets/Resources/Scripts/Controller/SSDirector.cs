public class SSDirector : System.Object
{
    private static SSDirector instance;
    public FirstController currentController { get; set; }
    public static SSDirector GetInstance()
    {
        if (instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }
}
