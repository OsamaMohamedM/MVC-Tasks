using System.ComponentModel.DataAnnotations;

namespace Model_Binding_Sources__FileUploading.Models
{
    public class ReportUploadViewModel
    {
        [Required]
        [StringLength(50)]
        public string ReportTitle { get; set; }

        [Required]
        public IFormFile ReportFile { get; set; }
    }
}