namespace Shared.Dto
{
    public record TypeResultDto
    {
        //When I use This Rrecord? 
        // I depend base on Value Type not Reference Type
        // objects are immutable
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
