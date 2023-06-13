using Japim.Assets;
namespace Japim.interpreter{
    
    ///<summary>
    /// This class perform the division of the text into specific segments
    ///</summary>
    static class Decomposer{
        public static string[] Decompose(string chain){
            
            string metadata = "";
            string root = "";

            if(-1 < chain.IndexOf(Token.METADATA))
            {
                //for rule [Metadata] always is before [root]
                if( chain.IndexOf(Token.METADATA) < chain.IndexOf(Token.ROOT))
                {
                    string[] segmentedChain = chain.Split(Token.ROOT);

                    metadata =  segmentedChain[0].Replace(Token.METADATA, "");
                    root = segmentedChain[1];
                }
                //in case [Metadata] was declared after [root], so your data is not record
                else
                {
                    string[] segmentedChain = chain.Split(Token.METADATA);
                    root = segmentedChain[0].Replace(Token.ROOT, "");
                }
            }
            else root = chain.Split(Token.ROOT)[0];             

            return new string[]{metadata, root};
        }
    }
}