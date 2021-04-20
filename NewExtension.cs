using System;

namespace ExtendClass
{
    class NewExtension
    {
        string path = "Path to the place where the data is stored.";
        string pathListExtension = "Path to the place where te data is stored";
        void sendToStore(string newPath)
        {
            path = newPath;
        }
        
        string getFromStore()
        {
            return path;
        }

        string getExtentionList(){
             return pathListExtension;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
