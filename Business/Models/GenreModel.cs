using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Business.Models
{
    public class GenreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Movie Count")]
        public int MovieCountOutput { get; set; }

        [DisplayName("Movies")]
        //[Required(ErrorMessage = "{0} must be selected!")] // if users must be selected, uncomment this line
        public List<int> MovieIdsInput { get; set; }

        [DisplayName("Movies")]
        public string MovieNamesOutput { get; set; }

    }
}
