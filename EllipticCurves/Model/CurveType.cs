namespace EllipticCurves.Model
{
    public class CurveType
    {
        public string Type { get; set; }

        public CurveType(string type)
        {
            Type = type;
        }
        
        public static string Prime => "P";
        public static string Supersingular => "2S";
        public static string Nonsupersingular => "2N";
    }
}