using Japim.interpreter;
using Japim.Assets;

namespace Japim.Assembler
{
    class Assembler
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
                Build(content.ToArray());
            }
            else
            {
                Console.WriteLine("Expected a build file!");
            }
        }
        public static void Build(string[] content)
        {
            Transcriber Transcriber = Transcriber.instance;
            Dictionary<string, ASSET> project = Transcriber.TokenService(content);
            Dictionary<string, string> conectors = new Dictionary<string, string>();
            
            foreach(var item in project.Keys)
            {
                ASSET type = project[item];

                if (type == ASSET.FILE)
                {
                   if (!File.Exists(item)) File.Create(item);
                }
                else if (type == ASSET.DIRECTORY)
                {
                   if (!Directory.Exists(item)) Directory.CreateDirectory(item);
                }
            }
        }
    }
}