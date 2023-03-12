namespace Japim
{
class Program
    {
        public static void Main(String[] args)
        {
            if (args.Length > 0) Assembler.Assembler.Run(new string[] {args[0]});
           else Assembler.Assembler.Run(new string[]{});
        }
    }
}