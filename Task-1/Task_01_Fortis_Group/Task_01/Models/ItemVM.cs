using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Task_01.Models
{
    public class ItemVM
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Item Name is Required"), DisplayName("Item Name")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters exceeded")]

        public string ItemName { get; set; } = null!;
        [Required(ErrorMessage = "Category is Required"), DisplayName("Category")]
        public int CategoryId { get; set; }

        public bool Status { get; set; }

    }
}
