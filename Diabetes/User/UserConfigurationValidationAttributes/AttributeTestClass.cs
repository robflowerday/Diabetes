namespace Diabetes.User.UserConfigurationValidationAttributes
{
    public class AttributeTestClass
    {
        // Valid
        [NonNegative]
        public int TestInt { get; set; }
        
        [NonNegative]
        public double TestDouble { get; set; }
        
        [NonNegative]
        public float TestFloat { get; set; }
        
        [NonNegative]
        public decimal TestDecimal { get; set; }
        
        [NonNegative]
        public long TestLong { get; set; }
        
        [NonNegative]
        public short TestShort { get; set; }
        
        [NonNegative]
        public byte TestByte { get; set; }
        
        // Invalid
        [NonNegative]
        public string TestString{ get; set; }
    }
}