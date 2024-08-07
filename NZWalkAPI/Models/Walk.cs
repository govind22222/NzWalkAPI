﻿namespace NZWalkAPI.Models
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Added DifficultyId and RegionId for Relationship with Region and Difficulty Table.
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation Property
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
