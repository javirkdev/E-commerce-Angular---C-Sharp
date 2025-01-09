using Microsoft.ML.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuringClothes.Database
{
    public class ModelInput
    {

        [LoadColumn (0)]
        [ColumnName ("text")]

        public string Text { get; set; }

        [LoadColumn (1)]
        [ColumnName ("label")]
        public float Label { get; set; }
    }
}
