using WebApp.Core;

namespace WebApp.Data
{
    public class InMemPathData : IPathData
    {
        BasePath BasePath;

        public InMemPathData()
        {
            BasePath = new BasePath(){
                Path = @"C:\Users\gjatin\Desktop\DirectoryProject\corpus"
            };
        }

        public BasePath GetBasePath()
        {
            return BasePath;
        }
    }
}
