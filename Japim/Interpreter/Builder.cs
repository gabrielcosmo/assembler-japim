using Japim.interpreter;
using Japim.Assets;

namespace Japim.Builder
{
    class Builder
    {
        public static void Run(String[] task)
        {
            if (task.Length > 0)
            {
                List<string> content = new List<string>();
                using var file = new StreamReader(task[0]);
                string? line;

                while ((line = file.ReadLine()) != null) content.Add(line);

                file.Close();
                Build(content);
            }
            else
            {
                Console.WriteLine("Expected a build file!");
            }
        }
        public static void Build(List<string> content)
        {
            Translate translate = Translate.instance;
            Dictionary<string, ASSET> project = translate.TokenService(content);
            Dictionary<string, string> conectors = new Dictionary<string, string>();

            foreach(var a in content)
            
            foreach(var item in project.Keys)
            {
                ASSET type = project[item];

                if (type == ASSET.FILE)
                {
                   File.Create(item);
                }
                else if (type == ASSET.DIRECTORY)
                {
                   Directory.CreateDirectory(item);
                }
            }
        }
    }
}