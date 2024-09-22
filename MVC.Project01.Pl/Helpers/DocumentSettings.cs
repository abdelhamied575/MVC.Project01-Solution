namespace MVC.Project01.Pl.Helpers
{
    public static class DocumentSettings
    {

        // 1. Upload

        public static string UploadFile(IFormFile file, string FolderName)
        {
            try
            {

                if (file == null) return null;

                // 1. Get Location Folder Path

                //string FolderPath = $"C:\\Users\\DELL\\Source\\Repos\\MVC.Project01-Solution\\MVC.Project01.Pl\\wwwroot\\Files\\{FolderName}";

                //string FolderPath = Directory.GetCurrentDirectory() + @"wwwroot\files" + FolderName;
                string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", FolderName);

                // 2. Get File Name and Make it Unique

                string FileName = $"{Guid.NewGuid()}{file.FileName}";

                // 3. Get File Path ---> FolderPath + FileName

                string FilePath = Path.Combine(FolderPath, FileName);

                // 4. Save File As Stream : Data Per Time

                using var fileStream = new FileStream(FilePath, FileMode.Create);

                file.CopyTo(fileStream);

                return FileName;


            }
            catch (Exception)
            {

                throw;
            }



        }




        // 2.Delete

        public static void DeleteFile(string FileName,string FolderName)
        {
            // 1. Get FilePath

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", FolderName,FileName);

            if(File.Exists(FilePath))
                File.Delete(FilePath);


        }


    }
}
