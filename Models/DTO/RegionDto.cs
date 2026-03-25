namespace NZWalks.API.Models.DTO
{
    public class RegionDto
    {
        // This will have the information that we will want to expose to our client. 
        // It will be a subset of the properties in the Domain Model

        // Currently we are exposing everything to our client. 
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } // nullable due to ?
    }
}
