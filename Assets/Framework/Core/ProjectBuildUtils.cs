
public class ProjectBuildUtils  {

    //通过命令行调用时可以使用此方法返回传入的参数
    public static string GetConsoleArgsByName(string name)
    {
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            if (arg.StartsWith(name))
            {
                return arg.Split(':')[1];
            }
        }
        return string.Empty;
    }
}
