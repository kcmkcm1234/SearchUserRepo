namespace IDOBusTech.NETTech.Test.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class RequestModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
    }
}