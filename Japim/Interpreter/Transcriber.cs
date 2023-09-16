
using Japim.Assets;
namespace Japim.interpreter
{
    ///<summary>
    ///Transcribe the raw text of file in a token, that will the base to make the project.
    ///</summary>
    class Transcriber
    {
        public Dictionary<string, ASSET> Structure { protected set; get; }
        public Dictionary<string, string> Metadata { protected set; get; }
        public static Transcriber instance = new Transcriber();

        private static List<string> openDirs = new List<string>();

        private Transcriber()
        {
            Structure = new Dictionary<string, ASSET>();
            Metadata = new Dictionary<string, string>();
        }

        public Dictionary<string, ASSET> TokenService(string[] content)
        {   
            string chain = "";
            string[] stream = {};

            foreach (string item in content) chain += item;
            
            //string[] body = chain.Split(Token.ROOT);
            string body = (Decomposer.Decompose(chain))[1];
            stream = Spliter(body, "/");
            
            try
            {
                Corrector.Corrector.Read(stream);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return instance.Tokenizer(stream);
        }

        private Dictionary<string, ASSET> Tokenizer(string[] material)
        {           
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
                count ++;
            }  
            return Structure; //return info in format of [App, DIRECTORY] and [App\main.cs, FILE]
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
