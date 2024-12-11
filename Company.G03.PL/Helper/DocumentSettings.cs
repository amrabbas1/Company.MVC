namespace Company.G03.PL.Helper
{
    public class DocumentSettings
    {
        //Upload
        public static string Upload(IFormFile file, string folderName)
        {
            //1. get folder location
            //string folderPath = $"D:\\Route\\07_MVC\\06\\Company.Solution\\Company.G03.PL\\wwwroot\\Files\\{folderName}";

            //string folderPath = Directory.CreateDirectory() + $"\\wwwroot\\Files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\{folderName}");

            //2. get file name and sure make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";//Guid always unique

            //3. get file path : folderPath + fileName
            string filePath = Path.Combine(folderPath, fileName);

            //4. File Stream : data per sec(server byt3aml m3 zeroes and ones fa lazm a7wl7ha file stream)
            using var FileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(FileStream);
            return fileName;
        }

        //Delete
        public static void Delete(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Files\\{folderName}", fileName);

            if(File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
