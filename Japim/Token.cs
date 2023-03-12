using System;

namespace Japim.Assets
{
    static class Token
    {
        public const string ROOT = "[root]";
        public const string ARCHIVE = "+";
        public const string DIRECTORY_STMT = ">";
        public const string DIRECTORY_OPEN = "[";
        public const string DIRECTORY_CLOSE = "]";
        public const string ASSIGNER = ":";
    }
    
    enum ASSET
    {
        FILE,
        DIRECTORY
    }
}