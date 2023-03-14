namespace FilmApp.Data.Entities;

public class Movie : EntityBase
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Universe { get; set; }
    public decimal BoxOffice { get; set; }

    public override string ToString() => $"{base.ToString()} | Title: {Title} | Y: {Year} | Universe: {Universe} | BoxOffice: {BoxOffice:C}";
}
