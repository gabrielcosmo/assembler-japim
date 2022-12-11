
using Japim.Assets;
namespace Japim.interpreter
{
    class Translate
    {
        public Dictionary<string, ASSET> Structure { protected set; get; }
        public Dictionary<string, string> Metadata { protected set; get; }
        public static Translate instance = new Translate();

        private static List<string> openDirs = new List<string>();

        private Translate()
        {
            Structure = new Dictionary<string, ASSET>();
            Metadata = new Dictionary<string, string>();
        }

        public Dictionary<string, ASSET> TokenService(List<string> content)
        {   
            string cadeia = "";
            foreach (string item in content) cadeia += item;
           
            return instance.Tokenizer(cadeia);
        }

        private Dictionary<string, ASSET> Tokenizer(string content)
        {
            string[] body = content.Split(Token.ROOT);
            string root = body[1];
            string[] material = Spliter(root, "/");
           
            int count = 0;
            string? name;

            foreach(var item in material)
            {
                try { name =  material[count + 1]; }
                catch { break; } //case out range, stop             
               
                if (item.Equals(Token.DIRECTORY_STMT))
                {
                    if (openDirs.Count > 0)
                    {
                        name = Path.Combine(openDirs[openDirs.Count - 1], name);
                        if (!Structure.ContainsKey(name)) Structure.Add(name, ASSET.DIRECTORY);
                    }
                    else
                    {
                        name = Path.Combine(name);
                        if (!Structure.ContainsKey(name)) Structure.Add(name, ASSET.DIRECTORY);
                    }
                    openDirs.Add(name);
                }

                //  [ (before open) ...

                else if (item.Equals(Token.ARCHIVE)) Logger(name, ASSET.FILE);

                else if (item.Equals(Token.DIRECTORY_CLOSE)) openDirs.RemoveAt(openDirs.Count - 1); // ... (end of directory) ]
                 Console.WriteLine(item);
                count ++;
            }

            return Structure;
        }

        private static void Logger(string name, ASSET species)
        {
            if (openDirs.Count > 0)
            {
                string path = Path.Combine(openDirs[openDirs.Count - 1], name);
                if (!instance.Structure.ContainsKey(path)) instance.Structure.Add(path, species);
            }

            else if (!instance.Structure.ContainsKey(name)) instance.Structure.Add(name, species);      
        }

        private static string[] Spliter(string content, string point)
        {
            return content.Replace(" ", "")
                        .Replace(Token.ARCHIVE, $"{point}{Token.ARCHIVE}{point}")
                        .Replace(Token.DIRECTORY_STMT, $"{point}{Token.DIRECTORY_STMT}{point}")
                        .Replace(Token.DIRECTORY_OPEN, $"{point}{Token.DIRECTORY_OPEN}{point}")
                        .Replace(Token.DIRECTORY_CLOSE, $"{point}{Token.DIRECTORY_CLOSE}{point}")
                        .Split(point);          
        }
    }
}
