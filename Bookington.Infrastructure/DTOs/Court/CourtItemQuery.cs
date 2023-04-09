using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;


namespace Bookington.Infrastructure.DTOs.Court
{
    public class CourtItemQuery : PaginatedQuery
    {
        //[EnumDataType(typeof(CourtTypeEnum))]
        //[JsonConverter(typeof(StringEnumConverter))]
        //public CourtTypeEnum? CourtType { get; set; }
        //[EnumDataType(typeof(CourtPlayerEnum))]
        //[JsonConverter(typeof(StringEnumConverter))]
        //public CourtPlayerEnum? CourtPLayertype { get; set; }
        public string? SearchText { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? OpenAt { get; set; }
        public string? CloseAt { get; set; }
        public string? DateOpen { get; set; }
    }
}
