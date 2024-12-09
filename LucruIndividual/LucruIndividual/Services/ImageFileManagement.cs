namespace LucruIndividual.Services
{
    public class ImageFileManagement
    {
        public static bool deleteImage(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            else
            {
                Console.WriteLine("File not found.");
                return false;
            }
        }
    }
}
