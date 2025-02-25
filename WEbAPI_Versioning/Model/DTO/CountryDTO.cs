namespace WEbAPI_Versioning.Model.DTO
{
    public class CountryDTOV1
    {
        public int Id { get; set; }    
        public string? Name { get; set; }

    }

    public class CountryDTOV2
    {

        public int Id { get; set; }
        public string? CountryName { get; set; }
    }
}
