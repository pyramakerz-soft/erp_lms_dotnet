namespace LMS_CMS_PL.Services
{
    public class FileImageValidationService
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly string[] _allowedMimeTypes = { "image/jpeg", "image/png" };

        public string? ValidateImageFile(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName)?.ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                return "Only image files (jpg, jpeg, png) are allowed.";
            }

            if (!_allowedMimeTypes.Contains(file.ContentType))
            {
                return "Invalid image type. Allowed types are: jpg, jpeg, png.";
            }

            return null;
        }
    }
}
