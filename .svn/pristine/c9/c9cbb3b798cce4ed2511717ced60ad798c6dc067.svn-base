using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UCMS.Model
{
    public class ContentTypeField
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DataType DataType { get; set; }
        public int Length { get; set; }
        public string LookupType { get; set; }
        public string LookupField { get; set; }
        public List<string> Items { get; set; }
        public string ContentTypeId { get; set; }
    }
}
