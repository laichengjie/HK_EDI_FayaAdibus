using System.Web.Mvc;
using FluentValidation.Attributes;
using HK_EDI.Web.Mvc;



namespace HK_EDI.Web.Models.Tasks
{
  
    public partial class ScheduleTaskModel : BaseNopEntityModel
    {
        
        [AllowHtml]
        public string Name { get; set; }

        
        public int Seconds { get; set; }

        
        public bool Enabled { get; set; }

        
        public bool StopOnError { get; set; }

        
        public string LastStartUtc { get; set; }

        
        public string LastEndUtc { get; set; }

        
        public string LastSuccessUtc { get; set; }

        public string Type { get; set; }


    }
}