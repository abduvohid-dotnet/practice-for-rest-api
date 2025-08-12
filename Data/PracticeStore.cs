using PracticeForRestApi.Models.Dto;

namespace PracticeForRestApi.Data
{
    public static class PracticeStore
    {
        public static List<PracticeDTO> practiceList = new List<PracticeDTO>{
            new PracticeDTO { Id = 1, Name = "Pool View", Occupancy=4, Sqft=100 },
            new PracticeDTO {Id = 2, Name = "Beach View", Occupancy=3, Sqft=300}
            };
    }
}