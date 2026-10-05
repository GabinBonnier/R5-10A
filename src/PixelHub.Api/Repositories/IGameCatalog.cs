using PixelHub.Api.Models;

namespace PixelHub.Api.Repositories;

public interface IGameCatalog
{
    Task<List<Game>> GetAllAsync();
    Task<List<Game>> GetByGenreAsync(string genre);
    Task<List<Game>> GetTopRatedAsync(int count);
    Task<List<GenreStats>> GetStatsByGenreAsync();
}

public record GenreStats(string Genre, double NoteMoyenne, int Nombre);