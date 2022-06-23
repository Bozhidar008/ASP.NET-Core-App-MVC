using System.IO;

namespace Poodle_e_Learning_Platform.InputModels.Images
{
    public class ImageInputModel
    {
        public int UserId { get; set; }

        public string OriginalType { get; set; }

        public string OriginalName { get; set; }

        public Stream ImageData { get; set; }
    }
}
