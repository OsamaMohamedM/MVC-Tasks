using Microsoft.AspNetCore.Mvc;

namespace Model_Binding_Sources__FileUploading.Models
{
    public class ReportSearchViewModel
    {
        [BindProperty(Name = "q", SupportsGet = true)]
        public string SearchQuery { get; set; }

        [FromQuery(Name = "cat")]
        public string Category { get; set; }
    }
}