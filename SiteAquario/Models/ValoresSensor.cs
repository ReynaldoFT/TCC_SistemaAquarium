using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteAquario.Models
{

    [Table("ValoresSensor")]
    public class ValoresSensor
    {
        [Column("ValoresSensorID")]
        [Display(Name = "ValoresSensorID")]        
        public int ValoresSensorID { get; set; }

        [Column("Ph")]
        [Required(ErrorMessage = "Digite o PH")]
        [Display(Name = "Ph")]
        public double Ph { get; set; }

        [Column("Temperatura")]
        [Required(ErrorMessage = "Digite a temperatura")]
        [Display(Name = "Temperatura")]
        public double Temperatura { get; set; }

        [Column("DataCadastro")]
        [Display(Name = "Data Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }



    }
}
